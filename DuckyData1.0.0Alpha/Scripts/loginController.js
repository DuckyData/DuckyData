duckyData.controller('loginAccountCtrl', function ($scope, $timeout, $window, toastr) {

    $scope.loginAccountData = {
        email: null,
        password: null
    }

    $scope.loginAccountErrorMsg = {
        emailMsg: null,
        passwordMsg: null,
        passwordMsgTwo: null,
        passwordMsgThree: null,
        cPasswordMsg: null
    }

    $scope.loginAccountUI = {
        disableBtn: false
    }

    $scope.validTheLogInEmailAddress = function () {

        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (re.test($scope.loginAccountData.email)) {
            $scope.loginAccountErrorMsg.emailMsg = null;
        } else {
            $scope.loginAccountErrorMsg.emailMsg = "The Email field is not a valid e-mail address."
        }

        if ($scope.loginAccountData.email && $scope.loginAccountData.email.length > 50) {
            $scope.loginAccountErrorMsg.emailMsg = "Email too long, 50 characters maximun"
        }
        loginAccountDisableBtn();
    }

    $scope.validThePassword = function () {

        var re = /^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).*$/;
        if (re.test($scope.loginAccountData.password)) {
            $scope.loginAccountErrorMsg.passwordMsg = null
            $scope.loginAccountErrorMsg.passwordMsgTwo = null;

            if ($scope.loginAccountData.password.length < 10) {
                $scope.loginAccountErrorMsg.passwordMsgThree = null
            } else {
                $scope.loginAccountErrorMsg.passwordMsgThree = "Password must be at least 6 - 10 characters";
            }
        } else {
            $scope.loginAccountErrorMsg.passwordMsg = "Passwords must contain at least one  lower case letter, one upper case letter, one digit and one valid special character"
            $scope.loginAccountErrorMsg.passwordMsgTwo = "valid special characters are: @#$%^&+=!";

            if ($scope.loginAccountData.password.length > 6 && $scope.loginAccountData.password.length < 10) {
                $scope.loginAccountErrorMsg.passwordMsgThree = null;
            } else {
                $scope.loginAccountErrorMsg.passwordMsgThree = "Password must be at least 6 - 10 characters";
            }

        }
        loginAccountDisableBtn();
    }

    function loginAccountDisableBtn() {
        if (!$scope.loginAccountErrorMsg.passwordMsg && !$scope.loginAccountErrorMsg.passwordMsgTwo && !$scope.loginAccountErrorMsg.passwordMsgThree && !$scope.loginAccountErrorMsg.emailMsg && !$scope.loginAccountErrorMsg.cPasswordMsg) {
            $scope.loginAccountUI.disableBtn = false;
        } else {
            $scope.loginAccountUI.disableBtn = true;
        }
    }


    $timeout(function(){
        $("").attr("autocomplete", "off");
    },1000)
})