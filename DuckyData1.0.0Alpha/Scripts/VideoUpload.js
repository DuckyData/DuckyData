duckyData.controller('videoUploadCtrl', function ($scope, $timeout, $window, $cookies, $q, GAPIFactory, duckyDataFileUploader, toastr) {
    $scope.videoFileUploader = duckyDataFileUploader.uploaderVideo;
    $scope.videoFileUploader.failedQueue = [];
    $scope.videoFileUploader.onAfterAddingFile = function (fileItem) {
        $timeout(function () {
            autosize(document.querySelectorAll('textarea'));
        }, 200)
        fileItem.metadataTitle = fileItem._file.name
    };
    $scope.videoUploadData = {
        user: {
            imgURL: $cookies.get('userYoutubeImage'),
            channel: $cookies.get('userYoutubeChannel'),
        },
        currentUploadingVideo:null
    }

    $scope.videoUploadUICtrl = {
        disableBtn: false
    }

    $scope.videoInputTagClick = function () {
        $("#video-file-select-tag").click();
    }
    $scope.goToMyChannel = function () {
        var url = 'https://www.youtube.com/channel/' + $cookies.get('userYoutubeChannel');
        $window.open(url, '_blank');
    }

    var uploadVideo;
    var UploadVideo = function () {
        this.tags = ['youtube-cors-upload'];
        this.categoryId = 22;
        this.videoId = '';
        this.uploadStartTime = 0;
    };

    UploadVideo.prototype.ready = function (accessToken) {
        var deferred = $q.defer();
        this.accessToken = accessToken;
        this.gapi = gapi;
        this.authenticated = true;
        this.gapi.client.request({
            path: '/youtube/v3/channels',
            params: {
                part: 'snippet',
                mine: true
            },
            callback: function (response) {
                if (response.error) {
                    deferred.resolve({status:403});
                } else {
                    $cookies.put('userYoutubeChannel', response.items[0].id);
                    $cookies.put('userYoutubeImage', response.items[0].snippet.thumbnails.default.url);
                    $scope.videoUploadUICtrl.showUserLogo = true;
                    deferred.resolve({ status: 200 });
                }
            }.bind(this)
        });
        return deferred.promise;
    };

    $scope.uploadAllVideos = function() {
        // get connestion
        $scope.videoUploadUICtrl.disableBtn = true;
        if (uploadVideo == null) {
            uploadVideo = new UploadVideo();
        }
        GAPIFactory.signIn().then(function (result) {
            if (result.status == 200) {
                uploadVideo.ready(result.token).then(function () {
                    UploadVideo.prototype.uploadFile(0)
                });
            } else {
                $scope.videoUploadUICtrl.disableBtn = false;
            }
        })
        
    }

    UploadVideo.prototype.uploadFile = function (index) {
        $scope.videoUploadData.currentUploadingVideo = file;
        if (index + 1 <= $scope.videoFileUploader.queue.length) {
            var file = $scope.videoFileUploader.queue[index];
            var metadata = {
                snippet: {
                    title: file.metadataTitle ? file.metadataTitle : file._file.name,
                    description: file.metadataDesc,
                    tags: this.tags,
                    categoryId: this.categoryId
                },
                status: {
                    privacyStatus: file.privacyStatus ? file.privacyStatus : 'unlisted'
                }
            };
            var uploader = new MediaUploader({
                baseUrl: 'https://www.googleapis.com/upload/youtube/v3/videos',
                file: file._file,
                token: $cookies.get('gapiToken'),
                metadata: metadata,
                params: {
                    part: Object.keys(metadata).join(',')
                },
                onError: function (data) {
                    var message = data;
                    try {
                        var errorResponse = JSON.parse(data);
                        message = errorResponse.error.message;
                    } finally {
                        toastr.error(message);
                    }
                    file.uploaded = false;
                    file.pct = 0;
                    $scope.videoFileUploader.failedQueue.push(file);
                    UploadVideo.prototype.uploadFile(index + 1);
                }.bind(this),
                onProgress: function (data) {
                    var currentTime = Date.now();
                    var bytesUploaded = data.loaded;
                    var totalBytes = data.total;
                    file.pct = Math.round((data.loaded / data.total) * 100);
                    $scope.$apply(file.pct);
                }.bind(this),
                onComplete: function (data) {
                    file.uploaded = true;
                    UploadVideo.prototype.uploadFile(index + 1);
                }.bind(this)
            });

            this.uploadStartTime = Date.now();
            uploader.upload();
        } else {
            clearUploadQueue();
            if ($scope.videoFileUploader.failedQueue.lenght) {
                reloadFailedQueue();
            } else {
                toastr.success('All files uploaded');
            }
            $scope.$apply(function () {
                $scope.videoUploadUICtrl.disableBtn = false;
            })
        }
    };
    function clearUploadQueue() {
        $scope.$apply(function () {
            $scope.videoFileUploader.queue = [];
        })
    }

    function reloadFailedQueue() {
        $scope.$apply(function () {
            angular.forEach($scope.videoFileUploader.failedQueue, function (file) {
                $scope.videoFileUploader.queue.push(file);
            });
            $timeout(function () {
                $scope.videoFileUploader.failedQueue = [];
            }, 500);
        })
    }

    $scope.removeVideoFromList = function (index) {
        if ($scope.videoUploadUICtrl.disableBtn) {
            toastr.error('Cannot remove file from list', 'Upload in process');
        } else {
            $scope.videoFileUploader.queue.splice(index, 1)
        }
    }
});