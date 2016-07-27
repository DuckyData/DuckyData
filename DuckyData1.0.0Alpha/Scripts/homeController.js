duckyData.controller('homeCtrl', function ($scope, $http, $location, $window, FileUploader, toastr) {
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

    $scope.inputTagTrigger = function (tag) {
        if ($scope.singAudioUploader.queue.length > 0) {
            // have file already
            toastr.error('Sorry, you can only indetify one file each time');
        } else {
            $(tag).click();
        }
    }

    $scope.identifyAudio = function (optionId) {
        $scope.homePageUICtrl.disableIfentifyButton = true;
        var config = {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }
        var fd = new FormData();
        fd.append('input', $scope.singAudioUploader.queue[0]._file);
        toastr.success('Uploading file to the system, please wait')
        $http.post('http://localhost:8102/MusicFetch/_MediaInput/'+optionId, fd, config).then(function (response) {
            $scope.homePageUICtrl.disableIfentifyButton = false;
            $window.location.href = response.data
        }, function (error) {
            $scope.homePageUICtrl.disableIfentifyButton = false;
            toastr.error('Sorry, cannot fild the metadata');
        });
    }
});