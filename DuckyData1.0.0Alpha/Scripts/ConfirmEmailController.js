duckyData.controller('ConfirmEmailCtrl', function ($scope, $window, $timeout,$location) {
    $scope.counter= 10;
    $scope.onTimeout = function () {
        $scope.counter--;
        if ($scope.counter == 0) {
            $scope.counter = 0;
            $scope.cancelTimeOut();
        }

        mytimeout = $timeout($scope.onTimeout, 1000);
    }
    var mytimeout = $timeout($scope.onTimeout, 1000);

    $scope.cancelTimeOut = function () {
        $timeout.cancel(mytimeout);
        var host = $location.host()
        console.log($location.search().userId)
        console.log(host)
        if (host == 'localhost') {
            $window.location.href = 'http://localhost:8102/Account/Edit/' + $location.search().userId
        } else {
            $window.location.href = 'http://myvmlab.senecacollege.ca:5340/Account/Edit/' + $location.search().userId
        }
    }

})