duckyData.controller('createBugReportCtrl', function ($scope) {
    $scope.createBugReportData = {
        subject: null,
        desc: null
    }

    $scope.createBugReportMsg = {
        subject: null,
        desc: null
    }

    $scope.createBugReportUI = {
        disableBtn : false
    }

    $scope.validateSubject = function () {
        if (!$scope.createBugReportData.subject) {
            $scope.createBugReportMsg.subject = 'Subject is required';
        } else {
            if ($scope.createBugReportData.subject.length > 70) {
                $scope.createBugReportMsg.subject = 'The field Subject must be a string with a maximum length of 70.';
            }else{
                $scope.createBugReportMsg.subject = null;
            }
        }
        disableBtn();
    }

    $scope.validateDesc = function () {
        if (!$scope.createBugReportData.desc) {
            $scope.createBugReportMsg.desc = 'Description is required';
        }
        else {
            if ($scope.createBugReportData.desc.length > 1000) {
                $scope.createBugReportMsg.desc = 'The field Description must be a string with a maximum length of 1000.';
            } else {
                $scope.createBugReportMsg.desc = null;
            }
        }
        disableBtn();
    }

    function disableBtn() {
        if (!$scope.createBugReportData.subject || !$scope.createBugReportData.desc) {
            $scope.createBugReportUI.disableBtn = true;
        } else {
            if ($scope.createBugReportMsg.subject) {
                $scope.createBugReportUI.disableBtn = true;
            } else {
                $scope.createBugReportUI.disableBtn = false;
            }
        }
    }
});