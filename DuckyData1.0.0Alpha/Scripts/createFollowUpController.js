duckyData.controller('followUpCtrl', function ($scope) {
    $scope.createFollowUpData = {
        subject: null,
        desc: null
    }

    $scope.createFollowUpMsg = {
        subject: null,
        desc: null
    }

    $scope.createFollowUpUI = {
        disableBtn: false
    }

    $scope.validateSubject = function () {
        if (!$scope.createFollowUpData.subject) {
            $scope.createFollowUpMsg.subject = 'Subject is required';
        } else {
            if ($scope.createFollowUpData.subject.length > 100) {
                $scope.createFollowUpMsg.subject = 'The field Subject must be a string with a maximum length of 100.';
            } else {
                $scope.createFollowUpMsg.subject = null;
            }
        }
        disableBtn();
    }

    $scope.validateDesc = function () {
        if (!$scope.createFollowUpData.desc) {
            $scope.createFollowUpMsg.desc = 'Description is required';
        }
        else {
            if ($scope.createFollowUpData.desc.length > 1000) {
                $scope.createFollowUpMsg.desc = 'The field Description must be a string with a maximum length of 1000.';
            } else {
                $scope.createFollowUpMsg.desc = null;
            }
        }
        disableBtn();
    }

    function disableBtn() {
        if (!$scope.createFollowUpData.subject || !$scope.createFollowUpData.desc) {
            $scope.createBugReportUI.disableBtn = true;
        } else {
            if ($scope.createFollowUpUI.subject) {
                $scope.createFollowUpUI.disableBtn = true;
            } else {
                if (!$scope.createFollowUpMsg.desc && !$scope.createFollowUpMsg.subject) {
                    $scope.createFollowUpUI.disableBtn = false;
                }
            }
        }
    }
});