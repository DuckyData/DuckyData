duckyData.controller('homeCtrl', function ($scope, $http, $location, $window, FileUploader, toastr) {
    $scope.singAudioUploader = new FileUploader();
    $scope.singVideoUploader = new FileUploader();

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
        $(tag).click();
    }

    $scope.identifyAudio = function () {
        var config = {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }

        var fd = new FormData();
        fd.append('input', $scope.singAudioUploader.queue[0]._file);
        $http.post('http://localhost:8102/MusicFetch/_MediaInput/1', fd, config).then(function (response) {
            $window.location.href = response.data
        }, function (error) {
           
        });
    }
});