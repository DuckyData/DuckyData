duckyData.controller('musicFetchCreateCtrl', function ($scope, $interval, $timeout, $location, $q, $interval, toastr, duckyDataFileUploader) {
    $scope.musicFetchCreateData = {
        file: null,
        buttonLogo: 'GO',
        currentUploadudio: null,
        uploadingLastOne: false,
        failedQueue : []
    }

    $scope.musicUploaderUICtrl = {
        showUploader: false,
        disableUploadAll: false
    }

    $scope.audioFileUploader = duckyDataFileUploader.uploader;

    SC.initialize({
        client_id: 'd468660bb9056f5d8e48361dd5327c80',
        redirect_uri: $location.protocol() + "://" + $location.host() + ":" + $location.port() + '/MusicFetch/CallBack'
    });
    $scope.inputTagClick = function(){
        $("#audio-file-select-tag").click();
    }

    function soundCloudConnectionCheck() {
        var deferred = $q.defer();
        if (SC.isConnected()) {
            deferred.resolve({ status:'connected',code:200});
        } else {
            SC.connect().then(function () {
                deferred.resolve({ status: 'connected', code: 200 });
            }).catch(function (error) {
                deferred.resolve({ status: 'unconnected', code: 401 });
            });
        }
        return deferred.promise;
    }

    $scope.uploadAllAudio = function () {
        // clear failed queue 
        $scope.musicFetchCreateData.failedQueue = [];
        soundCloudConnectionCheck().then(function (connectionResult) {
            if (connectionResult.code == 200) {
                if ($scope.audioFileUploader.queue.length) {
                    syncAudioUpload($scope.audioFileUploader.queue[0], 0);
                }
            } else {
                toastr.error('have problem connect to Sound Cloud, please check yuu sound cloud crendatial or try again later','Opps');
            }
        })
    }

    function syncAudioUpload(audio, index) {
        
        // if audio is not the last in file in list
        if ($scope.audioFileUploader.queue.length >= index + 1 || index == 0) {
            if (index + 1 == $scope.audioFileUploader.queue.length) {
                $scope.musicFetchCreateData.uploadingLastOne = true;
            } else {
                $scope.musicFetchCreateData.uploadingLastOne = false;
            }
            $scope.musicFetchCreateData.currentUploadudio = audio;
            if ($scope.audioFileUploader.queue.length > 0) {
                $scope.musicUploaderUICtrl.disableUploadAll = true;
            }
            
            SC.upload({
                file: audio._file,
                title: audio._file.name,
                progress: progressCallBack
            }).then(function (track) {
                // call upload for next audio
                syncAudioUpload($scope.audioFileUploader.queue[index + 1], index + 1);
                
            }).catch(function () {
                audio.pct = 0;
                $scope.musicFetchCreateData.failedQueue.push(audio)
                toastr.error('Failed to upload audio [' + audio._file.name + '], please try again latter')
                syncAudioUpload($scope.audioFileUploader.queue[index + 1], index + 1);
                if ($scope.musicFetchCreateData.uploadingLastOne) {
                    $scope.audioFileUploader.queue = [];
                    $scope.musicUploaderUICtrl.disableUploadAll = false;
                    checkForFailedUpload();
                }
            });
        } 
    }

    function progressCallBack(p) {
        var pct = Math.round((p.loaded / p.total) * 100);
        $scope.musicFetchCreateData.currentUploadudio.pct = pct;
        $scope.$apply($scope.musicFetchCreateData.currentUploadudio.pct);
        if ($scope.musicFetchCreateData.uploadingLastOne) {
            if (pct == 100) {
                if ($scope.audioFileUploader.queue.length > 0) {
                    $scope.audioFileUploader.queue = [];
                    $scope.musicUploaderUICtrl.disableUploadAll = false;
                    checkForFailedUpload();
                }
            }
        }
    }

    function checkForFailedUpload() {
        if ($scope.musicFetchCreateData.failedQueue.length > 0) {
            angular.forEach($scope.musicFetchCreateData.failedQueue, function (failed) {
                $scope.audioFileUploader.queue.push(failed);
            })
        }
    }
});