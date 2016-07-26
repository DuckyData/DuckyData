duckyData.controller('homeCtrl', function ($scope, FileUploader, toastr) {
    $scope.singAudioUploader = new FileUploader();
    $scope.singVideoUploader = new FileUploader();

    $scope.singAudioUploader.onAfterAddingFile = function (fileItem) {
        // $scope.singAudioUploader.queue = fileItem;

    };

    $scope.inputTagTrigger = function (tag) {
        $(tag).click();
    }



});