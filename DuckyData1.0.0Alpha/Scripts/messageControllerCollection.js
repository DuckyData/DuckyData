duckyData.controller('messageCreateCtrl', function ($scope, $timeout, $window, toastr) {

    $scope.messageCreateData = {
        recipient: null,
        subject: null
    }

    $scope.messageCreateUI = {
        disableBtn: true
    }

    $scope.messageCreateMsg = {
        recipient: null,
        subject: null
    }

    $scope.validCreateRecipient = function(){
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (re.test($scope.messageCreateData.recipient)) {
            $scope.messageCreateMsg.recipient = null;
        }else{
            $scope.messageCreateMsg.recipient = "The recipient field is not a valid e-mail address."
        }

        if ($scope.messageCreateData.recipient && $scope.messageCreateData.recipient.length > 50) {
            $scope.messageCreateMsg.recipient = "Email too long, 50 characters maximun"
        }
        if (!$scope.messageCreateData.recipient) {
            $scope.messageCreateMsg.recipient = "The recipient field is required"
        }
        createMessageDisableBtn();
    }

    $scope.validCreateSubject = function () {

        if (!$scope.messageCreateData.subject) {
            $scope.messageCreateMsg.subject = "The subject field is required"
        } else { 
            if ($scope.messageCreateData.subject.length > 100) {
                $scope.messageCreateMsg.subject = "The field Subject must be a string with a maximum length of 100."
            } else {
                $scope.messageCreateMsg.subject = "";
            }
        }

        createMessageDisableBtn();
    }

    function createMessageDisableBtn() {
        if ($scope.messageCreateMsg.recipient || $scope.messageCreateMsg.subject) {
            $scope.messageCreateUI.disableBtn = true;
        } else {
            $scope.messageCreateUI.disableBtn = false;
        }

        if (!$scope.messageCreateData.subject || !$scope.messageCreateData.recipient) {
            $scope.messageCreateUI.disableBtn = true;
        }
    }

})