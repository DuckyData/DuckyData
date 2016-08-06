duckyData.controller('homeCtrl', function ($scope, $http, $window, FileUploader, toastr) {
    $scope.singAudioUploader = new FileUploader();

    $scope.homePageUICtrl = {
        disableIfentifyButton: false
    }

    $scope.singAudioUploader.filters.push({
        name: 'singleAudioFilter',
        fn: function (item, options) {
            if ($scope.singAudioUploader.queue.length == 0) {
                return true
            } else {
                toastr.error('Sorry, you can only indetify one file each time');
                return false;
            }
        }
    });

    $scope.singAudioUploader.filters.push({
        name: 'mediaFileTypeFilter',
        fn: function (item, options) {
            var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';

            var userAgent = $window.navigator.userAgent;

            console.log(userAgent);

            var browsers = { chrome: /chrome/i, safari: /safari/i, firefox: /firefox/i, ie: /internet explorer/i };
            
            var mimeTypeSctring = '|mpeg|mp3|flac|x-ms-wma|mp4|';

            for (var key in browsers) {
                if (browsers[key].test(userAgent)) {
                    if (key == "chrome") {
                        mimeTypeSctring = '|mp3|flac|x-ms-wma|mp4|';
                    }
                }
            };

            if (mimeTypeSctring.indexOf(type) !== -1) {
                return true;
            } else {
                toastr.error('We only support FLAC, MP3, MP4, M4A, WMA', 'Unrecongnized File Type');
                return false;
            }
        }
    });

    $scope.singAudioUploader.filters.push({
        name: 'mediaSizeFilter',
        fn: function (item, options) {
            if (item.size < 104857600) {
                return true;
            } else {
                toastr.error("Sorry, the file size is too big, 100MB Maximun", "Large file");
                return false;
            }
        }
    });

    $scope.inputTagTrigger = function (tag) {
        if ($scope.singAudioUploader.queue.length > 0) {
            // have file already
            toastr.error('Sorry, you can only indetify one file each time');
        } else {
            $(tag).click();
        }
    }

    $scope.identifyAudio = function (optionId) {

        if ($scope.singAudioUploader.queue.length == 0) {
            toastr.error('Please select a media file');
        } else {
            $scope.homePageUICtrl.disableIfentifyButton = true;
            var config = {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }
            var fd = new FormData();
            fd.append('input', $scope.singAudioUploader.queue[0]._file);
            toastr.success('Uploading file to the system, please wait')
            $http.post('http://myvmlab.senecacollege.ca:5340/MusicFetch/_MediaInput/' + optionId, fd, config).then(function (response) {
                if (response.status == 200) {
                    if (response.data.statusCode == 200){
                        $scope.homePageUICtrl.disableIfentifyButton = false;
                        console.log(response);
                        var url = 'http://myvmlab.senecacollege.ca:5340/MusicFetch/Download?file=' + response.data.fileURL + '&fileName=' + response.data.fileName;
                        window.location.assign(url);
                        if (optionId == 2) {
                            var win = window.open('http://myvmlab.senecacollege.ca:5340/' + response.data.queryURL, "_blank");
                            win.focus();
                        }
                    } else if (response.data.statusCode == 400) {
                        toastr.error(response.data.msg);
                    } else if (response.data.statusCode == 500) {
                        toastr.error('Cannot find metadata for this file', 'Sorry');
                    }
                } else {
                    toastr.error('Cannot find metadata for this file', 'Sorry');
                }
                $scope.singAudioUploader.queue = [];
                $scope.homePageUICtrl.disableIfentifyButton = false;
            }, function (error) {
                $scope.homePageUICtrl.disableIfentifyButton = false;
                console.log(error);
                toastr.error('Sorry, cannot fild the metadata');
            });
        }
    }
});