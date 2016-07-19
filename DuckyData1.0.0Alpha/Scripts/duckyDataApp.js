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

duckyData.service('GAPIFactory', function (toastr,$q) {
    var OAUTH2_CLIENT_ID = '254706105392-sac4crqcmko7lagnmkng0krfsdg1ongg';
    var OAUTH2_SCOPES = ['https://www.googleapis.com/auth/youtube'];
    

    function getConnection() {
        console.log('get connection');
        var deferred = $q.defer();
        gapi.auth.init(function () {
            console.log('done init');
            gapi.auth.authorize({
                client_id: OAUTH2_CLIENT_ID,
                scope: OAUTH2_SCOPES,
                immediate: false
            }).then(function (signInResult) {
                if (signInResult && !signInResult.error) {
                    // connected
                    console.log('connected');
                    deferred.resolve({ status: 200 });
                } else {
                    console.log('failed to connect');
                    deferred.resolve({ status: 400 });
                }
            });
        });
        return deferred.promise;
    }

    function checkConnection() {
        var deferred = $q.defer();
        sessionParams = {
            'client_id': OAUTH2_CLIENT_ID,
            'session_state': null
        };
        gapi.auth.checkSessionState(sessionParams, function (stateMatched) {
            console.log('connection check');
            console.log(stateMatched);
            if (stateMatched == false) {
                console.log('not connected');
                getConnection().then(function (result) {
                    if (result.status == 200) {
                        deferred.resolve({ status: 200 });
                    } else {
                        eferred.resolve({ status: 400 });
                    }
                })
            } else {
                deferred.resolve({ status: 200 });
                console.log('connected in check');
            }
        });

        return deferred.promise;
    }

    function searchVideo(param) {
        var deferred = $q.defer();
        checkConnection().then(function (connection) {
            if (connection.status == 200) {
                searchFunction(param).then(function (result) {
                    deferred.resolve({ status: 200, data: result });
                });
            } else {
                getConnection(param).then(function () {
                    searchFunction().then(function (result) {
                        deferred.resolve({ status: 200, data: result });
                    });
                })
            }
        })

        return deferred.promise;
    }

    function searchFunction(param) {
        var deferred = $q.defer();
        gapi.client.load('youtube', 'v3', function () {
            var q = param;
            var request = gapi.client.youtube.search.list({
                q: q,
                maxResults: 30,
                part: 'snippet'
            });
            request.execute(function (response) {
                console.log(gapi);
                deferred.resolve({videoList: response.result.items, pageInfo: response.pageInfo, nextPageToken: response.nextPageToken });
            });
        });
        return deferred.promise;
    }

    return {
        searchVideo: searchVideo
    }
});

duckyData.service('duckyDataFileUploader', function (FileUploader,toastr) {
    var uploader = new FileUploader();
    uploader.filters.push({
        name: 'audioFilter',
        fn: function (item, options) {
            var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
            if ('|mp3|wav|x-wav|x-pn-wav|ogg|aiff|x-aiff|mpeg|x-mpeg|flac|x-aac|x-ms-wma|'.indexOf(type) !== -1) {
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

duckyData.filter("percentage", function () {
    return function (value) {
        if (value == null || value == 0 || value == undefined) {
            return 'Waiting';
        } else if (value == 100) {
            return 'Done';
        } else if (isNaN(value)) {
            return 'Waiting';
        } else {
            return value + '%';
        }
    };
});

duckyData.filter("trustUrl", ['$sce', function ($sce) {
    return function (recordingUrl) {
        return $sce.trustAsResourceUrl(recordingUrl);
    };
}]);
