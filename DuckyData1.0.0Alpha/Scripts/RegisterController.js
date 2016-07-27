duckyData.controller('registerAccountCtrl', function ($scope, $http, $location, $window, FileUploader, toastr) {

    $scope.registerAccountData = {
        email: null,
    }

    $scope.validTheEmailAddress = function () {
        console.log($scope.registerAccountData.email);
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        console.log(re.test($scope.registerAccountData.email))
    }

});