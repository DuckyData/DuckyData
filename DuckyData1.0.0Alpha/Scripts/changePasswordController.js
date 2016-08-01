duckyData.controller('changePasswordCtrl', function ($scope, $timeout, $window) {

    $scope.changePasswordData = {
        curPassword: null,
        newPassword: null,
        conPassword:null
    }

    $scope.changePasswordErrorMsg = {
        curPasswordMsgRequire: null,
        curPasswordMsgRegx: null, 
        curPasswordMsgLen: null,
        curPasswordMsgRange: null,
        newPasswordMsgRequire: null,
        newPasswordMsgRegx: null,
        newPasswordMsgLen: null,    
        newPasswordMsgRange: null,
        conPasswordMsgRequire: null,
        conPasswordMsgRegx: null,
        conPasswordMsgLen: null,
        conPasswordMsgRange: null,
        conPasswordMsgSame: null
    }

    $scope.changePasswordUI = {
        disableBtn: false
    }

    $scope.validCurrentPassword = function () {
        var re = /^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).*$/;

        if (!$scope.changePasswordData.curPassword) {
            $scope.changePasswordErrorMsg.curPasswordMsgRequire = "The filed current password is required";
            $scope.changePasswordErrorMsg.curPasswordMsgRegx = null;
            $scope.changePasswordErrorMsg.curPasswordMsgLen = null;
            $scope.changePasswordErrorMsg.curPasswordMsgRange = null;
        } else {
            $scope.changePasswordErrorMsg.curPasswordMsgRequire = null;
            if (re.test($scope.changePasswordData.curPassword)) {
                $scope.changePasswordErrorMsg.curPasswordMsgRegx = null
                $scope.changePasswordErrorMsg.curPasswordMsgRange = null;

                if ($scope.changePasswordData.curPassword.length < 10) {
                    $scope.changePasswordErrorMsg.curPasswordMsgLen = null
                } else {
                    $scope.changePasswordErrorMsg.curPasswordMsgLen = "Password must be at least 6 - 10 characters";
                }
            } else {
                $scope.changePasswordErrorMsg.curPasswordMsgRegx = "Passwords must contain at least one lower case letter, one upper case letter, one digit and one valid special character"
                $scope.changePasswordErrorMsg.curPasswordMsgRange = "Valid special characters are: @#$%^&+=!";

                if ($scope.changePasswordData.curPassword.length > 6 && $scope.changePasswordData.curPassword.length < 10) {
                    $scope.changePasswordErrorMsg.curPasswordMsgLen = null;
                } else {
                    $scope.changePasswordErrorMsg.curPasswordMsgLen = "Password must be at least 6 - 10 characters";
                }
            }
        }
        
        changePasswordDisableBtn();
    }

    $scope.validNewPassword = function () {
        var re = /^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).*$/;

       
        if (!$scope.changePasswordData.newPassword) {
            $scope.changePasswordErrorMsg.newPasswordMsgRequire = "The field new password is required";
            $scope.changePasswordErrorMsg.newPasswordMsgRegx = null;
            $scope.changePasswordErrorMsg.newPasswordMsgLen = null;
            $scope.changePasswordErrorMsg.newPasswordMsgRange = null;
        } else {
            $scope.changePasswordErrorMsg.newPasswordMsgRequire = null;
            if (re.test($scope.changePasswordData.newPassword)) {
                $scope.changePasswordErrorMsg.newPasswordMsgRegx = null
                $scope.changePasswordErrorMsg.newPasswordMsgRange = null;

                if ($scope.changePasswordData.newPassword.length < 10) {
                    $scope.changePasswordErrorMsg.newPasswordMsgLen = null;
                    if ($scope.changePasswordData.conPassword && ($scope.changePasswordData.conPassword != $scope.changePasswordData.newPassword)) {
                        $scope.changePasswordErrorMsg.conPasswordMsgSame = "New password and confirm new password not match"
                    } else {
                        $scope.changePasswordErrorMsg.conPasswordMsgSame = null;
                    }
                } else {
                    $scope.changePasswordErrorMsg.newPasswordMsgLen = "New password must be at least 6 - 10 characters";
                }
            } else {
                $scope.changePasswordErrorMsg.newPasswordMsgRegx = "New password must contain at least one lower case letter, one upper case letter, one digit and one valid special character"
                $scope.changePasswordErrorMsg.newPasswordMsgRange = "Valid special characters are: @#$%^&+=!";

                if ($scope.changePasswordData.newPassword.length > 6 && $scope.changePasswordData.newPassword.length < 10) {
                    $scope.changePasswordErrorMsg.newPasswordMsgLen = null;
                } else {
                    $scope.changePasswordErrorMsg.newPasswordMsgLen = "New password must be at least 6 - 10 characters";
                }
            }
        }
        changePasswordDisableBtn();
    }

    $scope.validConPassword = function () {

        if (!$scope.changePasswordData.conPassword) {
            $scope.changePasswordErrorMsg.conPasswordMsgRequire = "The field Confirm password is required";
            $scope.changePasswordErrorMsg.conPasswordMsgRegx = null;
            $scope.changePasswordErrorMsg.conPasswordMsgLen = null;
            $scope.changePasswordErrorMsg.conPasswordMsgSame = null;
            $scope.changePasswordErrorMsg.conPasswordMsgRange = null;
        } else {
            $scope.changePasswordErrorMsg.conPasswordMsgRequire = null;
            var re = /^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).*$/;
            if (re.test($scope.changePasswordData.conPassword)) {
                $scope.changePasswordErrorMsg.conPasswordMsgRegx = null
                $scope.changePasswordErrorMsg.conPasswordMsgRange = null;

                if ($scope.changePasswordData.conPassword.length < 10) {
                    $scope.changePasswordErrorMsg.conPasswordMsgLen = null
                    if ($scope.changePasswordData.conPassword != $scope.changePasswordData.newPassword) {
                        $scope.changePasswordErrorMsg.conPasswordMsgSame = "New password and confirm new password not match"
                    } else {
                        $scope.changePasswordErrorMsg.conPasswordMsgSame = null;
                    }
                } else {
                    $scope.changePasswordErrorMsg.conPasswordMsgLen = "New password must be at least 6 - 10 characters";
                }
            } else {
                $scope.changePasswordErrorMsg.conPasswordMsgRegx = "New password must contain at least one lower case letter, one upper case letter, one digit and one valid special character"
                $scope.changePasswordErrorMsg.conPasswordMsgRange = "Valid special characters are: @#$%^&+=!";

                if ($scope.changePasswordData.conPassword.length > 6 && $scope.changePasswordData.conPassword.length < 10) {
                    $scope.changePasswordErrorMsg.conPasswordMsgLen = null;
                } else {
                    $scope.changePasswordErrorMsg.conPasswordMsgLen = "New password must be at least 6 - 10 characters";
                }
            }
        }
        changePasswordDisableBtn();
    }

    function changePasswordDisableBtn() {
        if (!$scope.changePasswordData.conPassword || !$scope.changePasswordData.curPassword || !$scope.changePasswordData.newPassword) {
            $scope.changePasswordUI.disableBtn = true;
        } else {
            var hasError = false;
            angular.forEach($scope.changePasswordErrorMsg, function (error) {
                if (error) {
                    hasError = true
                }
            });

            console.log(hasError);
            $scope.changePasswordUI.disableBtn = hasError;
        }
    }

});