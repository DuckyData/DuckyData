duckyData.controller('musicFetchCreateCtrl', function ($scope, $interval, $timeout, $q, $interval, toastr, duckyDataFileUploader) {
    $scope.musicFetchCreateData = {
        file: null,
        buttonLogo: 'GO',
        currentUploadudio: null
    }
    $scope.musicUploaderUICtrl = {
        showUploader: false,
        disableUploadAll: false
    }

    $scope.audioFileUploader = duckyDataFileUploader.uploader;

    SC.initialize({
        client_id: 'd468660bb9056f5d8e48361dd5327c80',
        redirect_uri: 'http://localhost:8102/MusicFetch/CallBack'
    });

    $scope.connectFunction = function () {
        SC.connect();
    }

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
                deferred.resolve({ status: 'unconnected', code: 401});
            });
        }
        return deferred.promise;
    }

    $scope.uploadAllAudio = function () {
        soundCloudConnectionCheck().then(function (connectionResult) {
            console.log(connectionResult);
            if (connectionResult.code == 200) {

                console.log($scope.audioFileUploader.queue[0]);
                syncAudioUpload($scope.audioFileUploader.queue[0],0);
            } else {
                toastr.error('have problem connect to Sound Cloud, please check yuu sound cloud crendatial or try again later','Opps');
            }
        })
    }

    function syncAudioUpload(audio,index) {
        // if audio is not the last in file in list
        console.log();
        if ($scope.audioFileUploader.queue.length >= index + 1 || index == 0) {
            $scope.musicFetchCreateData.currentUploadudio = audio;
            SC.upload({
                file: audio._file,
                title: audio._file.name,
                progress: progressCallBack
            }).then(function (track) {
                console.log('upload next');
                // call upload for next audio
                syncAudioUpload($scope.audioFileUploader.queue[index + 1], index + 1);
            }).catch(function () {
                if ($scope.audioFileUploader.queue.lenght == 1) {
                    toastr.error('Failed to upload audio [' + audio.name + ']')
                } else {
                    toastr.error('Failed to upload audio [' + audio.name + '], will start next')
                }

            });
        }
    }

    function progressCallBack(p) {
        var pct = Math.round((p.loaded / p.total) * 100);
        $scope.musicFetchCreateData.currentUploadudio.pct = pct;
        $scope.$apply($scope.musicFetchCreateData.currentUploadudio.pct);
    }
    $timeout(function () {
        $scope.musicUploaderUICtrl.showUploader = true;
    }, 500);
    
});