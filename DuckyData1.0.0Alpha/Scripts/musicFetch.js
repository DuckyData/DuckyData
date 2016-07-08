var duckyData = angular.module('duckyData', ['ngRoute', 'toastr']);

duckyData.config(function ($httpProvider, $locationProvider, toastrConfig) {
    $locationProvider.html5Mode({ enabled: true, requireBase: false });

    angular.extend(toastrConfig, {
        autoDismiss: false,
        allowHtml: true,
        containerId: 'toast-container',
        maxOpened: 0,
        newestOnTop: true,
        positionClass: 'toast-bottom-right',
        preventDuplicates: false,
        timeOut: 20000,
        preventOpenDuplicates: true,
        target: 'body'
    });
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
duckyData.controller('musicFetchCtrl', function ($scope, $http, $sce, $location,toastr) {
    $scope.musicFetchData = {
        albumTrackList: null,
        album: null,
        possibleAlbunList:null
    }

    $scope.musicFetchUICtrl = {
        showPreviewWindow: false,
        showPossibleAlbum: false,
        showTrackList: false,
        showNoMachedResult: false
    }

    var config = {
        params: {
            output: "jsonp",
            callback: 'JSON_CALLBACK',
            q: $location.search().album
        }
    }

    $scope.albumFetch = function () {
        if (config.params.q) {
            $http.jsonp("https://api.deezer.com/search/album", config).success(function (data) {
                if (data != null) {
                    console.log(data);
                    if (data.total == 1) {
                        $scope.musicFetchData.album = data.data[0];
                        $scope.musicFetchUICtrl.showTrackList = true;
                        $scope.musicFetchUICtrl.showPreviewWindow = true;
                        $scope.getAlbumTrack($scope.musicFetchData.album.tracklist);
                    } else if (data.total > 1) {
                        $scope.musicFetchUICtrl.showPossibleAlbum = true;
                        $scope.musicFetchData.possibleAlbunList = data.data
                    } else {
                        toastr.error('Cannot find information about this album', 'Sorry');
                        $scope.musicFetchData.showNoMachedResult = false;
                    }
                } else {
                    $scope.musicFetchData.showNoMachedResult = true;
                }
            }).error(function (data) {
                toastr.error('Cannot find information about this album', 'Sorry');
            });
        } else {
            toastr.error('Ablum paramater not found!','Opps');
        }
    }(function(){}());
    

    $scope.getAlbumTrack = function(url) {
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

                $scope.musicFetchUICtrl.showPreviewWindow= true;
                $scope.musicFetchUICtrl.showPossibleAlbum= false;
                $scope.musicFetchUICtrl.showTrackList= true;
            } else {
                console.log('no data found');
            }
        }).error(function (data) {
            $scope.data = data;
        });
    }

    $scope.playPausePreview = function (operation) {
        if (operation == 'play') {
            document.getElementById("previewAudio").play();
        } else {
            document.getElementById("previewAudio").pause();
        }
    }
});
