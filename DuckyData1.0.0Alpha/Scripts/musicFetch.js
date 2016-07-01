var duckyData = angular.module('duckyData', ['ngRoute']);

duckyData.config(function ( $httpProvider) {

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


// API factory
duckyData.factory('musicFetchFactory', function (APISwitch, $q) {
    var self = this;
    var deferred = $q.defer();
    var rec = {
    }
    return rec;
});

duckyData.filter('formatTimer', function () {
    return function (input) {
        function z(n) { return (n < 10 ? '0' : '') + n; }
        var seconds = input % 60;
        var minutes = Math.floor(input % 3600 / 60);
        return (z(minutes) + ':' + z(seconds));
    }
});

duckyData.filter("trustUrl", ['$sce', function ($sce) {
    return function (recordingUrl) {
        return $sce.trustAsResourceUrl(recordingUrl);
    };
}]);

// view controller
duckyData.controller('musicFetchCtrl', function ($scope, $http, $sce) {
    $scope.musicFetchData = {
        albumTrackList: null,
        album: null,
    }
    $scope.musicFetchUICtrl = {
        showPreviewWindow : false
    }

    $scope.musicFetchRawData = {
    }
    var config = {
        params: {
            output: "jsonp",
            callback: 'JSON_CALLBACK',
            q: 'The Eminem Show'
        }
    }

    $scope.albumFetch = function () {
        $http.jsonp("https://api.deezer.com/search/album", config).success(function (data) {

            if (data != null) {
                $scope.musicFetchData.album = data.data[0];
                $scope.getAlbumTrack($scope.musicFetchData.album.tracklist);

            } else {
                console.log('no data found');
            }
        }).error(function (data) {
            console.log(data);
            $scope.data = data;
        });
    }(function () { }());
    
    $scope.getAlbumTrack = function (url) {
        var config = {
            params: {
                output: "jsonp",
                callback: 'JSON_CALLBACK',
            }
        }
        $http.jsonp(url,config).success(function (data) {
            if (data != null) {
                $scope.musicFetchData.albumTrackList = data.data;

                angular.forEach($scope.musicFetchData.albumTrackList, function (track) {
                    track.previewAudio = $sce.trustAsResourceUrl('http://cdn-preview-0.deezer.com/stream/01ce4a31724e1dc8cccab627e233b5b9-3.mp3')
                });
            } else {
                console.log('no data found');
            }
        }).error(function (data) {
            $scope.data = data;
        });

    }

    $scope.playPreview= function (previewURL) {
       
        document.getElementById("previewAudio").load();
        console.log($scope.audios);
    }

});
