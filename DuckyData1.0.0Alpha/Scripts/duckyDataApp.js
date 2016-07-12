var duckyData = angular.module('duckyData', ['ngRoute', 'toastr','angularFileUpload']);

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

duckyData.service('duckyDataFileUploader', function (FileUploader,toastr) {
    var uploader = new FileUploader();

    uploader.filters.push({
        name: 'audioFilter',
        fn: function (item, options) {
            console.log(item);
            var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
            console.log(type);
            if ('|mp3|'.indexOf(type) !== -1) {
                return true;
            } else {
                toastr.error('We only support AIFF, WAVE, FLAC, OGG, MP2, MP3, AAC, AMR and WMA','Unrecongnize File Type');
                return false;
            }
            
        }
    });



    return {
        uploader: uploader
    }
});

// API factory
duckyData.factory('musicFetchFactory', function (APISwitch, $q, $http) {
    var self = this;
    var deferred = $q.defer();
    var rec = {
    }

    function jumuPage(filter) {
        var deferred = $q.defer();
        $http.jsonp("https://api.deezer.com/search/album", filter).success(function (albumData) {
            deferred.resolve({ data: albumData, status: 'OK' });
        }).error(function (data) {
            deferred.resolve({ status: 'ERROR' });
        });
        return deferred.promise;
    }

    return {
        jumpAlbumPage: jumuPage
    };
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
