var duckyData = angular.module('duckyData', ['ngRoute','restangular']);

duckyData.config(function (RestangularProvider, $httpProvider) {

    RestangularProvider.setDefaultHeaders({
        withCredentials: true
    });
    RestangularProvider.setFullResponse(true);
});


duckyData.service('APISwitch', function () {
    var API_ENV = {
        gracenote: 'https://c415878569.web.cddbp.net/webapi/json/1.0/',
        deezer: 'https://api.deezer.com'
    }
    this.grecenoteBase = function () {
        return API_ENV.gracenote;
    }
    this.deezerBase = function () {
        return API_ENV.deezer;
    }
});



duckyData.service('grecenoteAPI', function (Restangular, APISwitch) {
    return Restangular.withConfig(function (RestangularConfigurer) {
        RestangularConfigurer.setBaseUrl(APISwitch.grecenoteBase());
    });
});


duckyData.service('deezerAPI', function (Restangular, APISwitch) {
    return Restangular.withConfig(function (RestangularConfigurer) {
        RestangularConfigurer.setBaseUrl(APISwitch.deezerBase());
    });
});

duckyData.factory('musicFetchFactory', function (APISwitch, $q, grecenoteAPI) {
    var self = this;
    var deferred = $q.defer();
    var rec = {
    }
    return rec;
});

duckyData.controller('musicFetchCtrl', function ($scope, deezerAPI,$http) {
    $scope.musicFetchData = {
        name: {
                first:'zhu'
            }
    }

    $scope.testfunction = function () {
        $http.jsonp("https://api.deezer.com/user/2529?callback=JSON_CALLBACK",{ method: 'POST'}).
            success(function(data) {
        $scope.data = data;
        console.log(data);
   
  }).
  error(function (data) {
    $scope.data = "Request failed";
  });
        //deezerAPI.one('user').customGET('2529', {}).get().then(function (data) {
        //    console.log(data);
        //});
    }
});
