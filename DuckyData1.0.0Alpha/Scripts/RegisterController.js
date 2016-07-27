duckyData.controller('registerAccountCtrl', function ($scope, $timeout, $location, $window, FileUploader, toastr) {

    $scope.registerAccountData = {
        email: null,
        password: null,
        cPassword: null
    }

    $scope.registerAccountErrorMsg= {
        emailMsg:null,
        passwordMsg:null,
        passwordMsgTwo:null,
        passwordMsgThree:null,
        cPasswordMsg: null
    }

    $scope.registerAccountUI = {
        disableBtn:false
    }

    $scope.validTheEmailAddress = function () {

        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if(re.test($scope.registerAccountData.email)){
            $scope.registerAccountErrorMsg.emailMsg = null;
        }else{
            $scope.registerAccountErrorMsg.emailMsg = "The Email field is not a valid e-mail address."
        }

        if($scope.registerAccountData.email && $scope.registerAccountData.email.length > 50){
            $scope.registerAccountErrorMsg.emailMsg = "Email too long, 50 characters maximun"
        }
        registerAccountDisableBtn();
    }

    $scope.validThePassword = function () {
        console.log('check');
        var re = /^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$/;
        if (re.test($scope.registerAccountData.password)) {
            $scope.registerAccountErrorMsg.passwordMsg = null
            $scope.registerAccountErrorMsg.passwordMsgTwo = null;

            if ($scope.registerAccountData.password.length < 10) {
                $scope.registerAccountErrorMsg.passwordMsgThree = null
            } else {
                $scope.registerAccountErrorMsg.passwordMsgThree = "Password must be at least 6 - 10 characters";
            }
        } else {
            $scope.registerAccountErrorMsg.passwordMsg = "Passwords must contain at least one  lower case letter, one upper case letter, one digit and one valid special character"
            $scope.registerAccountErrorMsg.passwordMsgTwo = "valid special characters are: @#$%^&+=";

            if ($scope.registerAccountData.password.length > 6 && $scope.registerAccountData.password.length < 10) {
                $scope.registerAccountErrorMsg.passwordMsgThree = null;
            } else {
                $scope.registerAccountErrorMsg.passwordMsgThree = "Password must be at least 6 - 10 characters";
            }
            
        }
        registerAccountDisableBtn();
    }

    $scope.validTheConfirmPassword = function () {
        if ($scope.registerAccountData.password && $scope.registerAccountData.cPassword) {
            if ($scope.registerAccountData.password == $scope.registerAccountData.cPassword) {
                $scope.registerAccountErrorMsg.cPasswordMsg = null;
            } else {
                $scope.registerAccountErrorMsg.cPasswordMsg = "The password and confirmation password do not match."
            }
        }
        registerAccountDisableBtn();
    }

    function registerAccountDisableBtn() {

        if (!$scope.registerAccountData.email || !$scope.registerAccountData.password || !$scope.registerAccountData.cPassword) {
            $scope.registerAccountUI.disableBtn = true;
        } else {
            if (!$scope.registerAccountErrorMsg.passwordMsg && !$scope.registerAccountErrorMsg.passwordMsgTwo && !$scope.registerAccountErrorMsg.passwordMsgThree && !$scope.registerAccountErrorMsg.emailMsg && !$scope.registerAccountErrorMsg.cPasswordMsg) {
                $scope.registerAccountUI.disableBtn = false;
            } else {
                $scope.registerAccountUI.disableBtn = true;
            }
        }
    }

    $(document).ready(function () {
        console.log('check onload');
        $timeout(function () {
            registerAccountDisableBtn();
        })
    });
});