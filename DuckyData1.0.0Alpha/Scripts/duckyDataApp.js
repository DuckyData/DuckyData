var duckyData = angular.module('duckyData', ['toastr','angularFileUpload','ngCookies']);

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

duckyData.service('GAPIFactory', function (toastr,$q,$cookies) {
    var OAUTH2_CLIENT_ID = '254706105392-sac4crqcmko7lagnmkng0krfsdg1ongg';
    var OAUTH2_SCOPES = ['https://www.googleapis.com/auth/youtube','https://www.googleapis.com/auth/youtube.upload'];
    // get session, set token
    function getConnection() {
        var deferred = $q.defer();
        gapi.auth.authorize({
            client_id: OAUTH2_CLIENT_ID,
            response_type: 'token',
            scope: OAUTH2_SCOPES,
            immediate: false
        }).then(function (signInResult) {
            console.log(gapi.auth.getToken());
            if (signInResult && !signInResult.error) {
                $cookies.put('gapiToken', signInResult.access_token);
                deferred.resolve({ status: 200, token: $cookies.get('gapiToken') });
            } else {
                deferred.resolve({ status: 400 });
            }
        });
        return deferred.promise;
    }

    function searchVideo(param, pageInfo) {
        var deferred = $q.defer();
        var page = pageInfo.nextPageToken ? pageInfo.nextPageToken : pageInfo.prevPageToken;
        gapi.client.load('youtube', 'v3', function () {
            var q = param;
            var request = gapi.client.youtube.search.list({
                q: q,
                maxResults: 30,
                pageToken: page,
                access_token: $cookies.get('gapiToken'),
                part: 'snippet'
            });
            request.execute(function (response) {
                if (response.error && (response.code == 401 || response.code == 403)) {
                    // token expired, reconnect
                    getConnection().then(function () {
                        searchVideo(param, pageInfo).then(function (response) {
                            console.log(response);
                            deferred.resolve(response);
                        })
                    })
                } else {
                    deferred.resolve({ videoList: response.result.items, pageInfo: response.pageInfo, nextPageToken: response.nextPageToken, prevPageToken: response.prevPageToken });
                }
            });
        });
        return deferred.promise;
    }

    return {
        searchVideo: searchVideo,
        signIn: getConnection
    }
});

duckyData.service('duckyDataFileUploader', function (FileUploader, toastr, $window) {
    var uploaderAudio = new FileUploader();
    var uploaderVideo = new FileUploader();

    var singAudioUploader = new FileUploader();
    var singVideoUploader = new FileUploader();

    uploaderAudio.filters.push({
        name: 'audioFilter',
        fn: function (item, options) {
            var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
            var userAgent = $window.navigator.userAgent;
            var browsers = { chrome: /chrome/i, safari: /safari/i, firefox: /firefox/i, ie: /internet explorer/i };
            var mimeTypeSctring = '|mpeg|wav|x-wav|x-pn-wav|ogg|flac|x-ms-wma|mp4|avi|x-matroska|x-troff-msvideo|msvideo|x-msvideo|x-flv|x-ms-wmv|';

            for (var key in browsers) {
                if (browsers[key].test(userAgent)) {
                    if (key == "chrome") {
                        mimeTypeSctring = '|mp3|wav|x-wav|x-pn-wav|ogg|flac|x-ms-wma|mp4|avi|x-matroska|x-troff-msvideo|msvideo|x-msvideo|x-flv|x-ms-wmv|';
                    }
                }
            };

            if (mimeTypeSctring.indexOf(type) !== -1) {
                return true;
            } else {
                toastr.error('We only support AIFF, WAVE, FLAC, OGG, MP2, MP3, AAC, AMR and WMA','Unrecongnize File Type');
                return false;
            }
        }
    });

    uploaderAudio.filters.push({
        name: 'audioSizeFilter',
        fn: function (item, options) {
            if (item.size < 104857600) {
                return true;
            } else {
                toastr.error("Sorry, the file size is too big, 100MB Maximun", "Large file");
                return false;
            }
        }
    });


    uploaderVideo.filters.push({
        name: 'videoFilter',
        fn: function (item, options) {
            var type = item.type.slice(0, 5);
            console.log(type);
            if (type == 'video') {
                return true;
            } else {
                toastr.error('We only support video files', 'Unrecongnize File Type');
                return false;
            }
        }
    });

    uploaderVideo.filters.push({
        name: 'videoSizeFilter',
        fn: function (item, options) {
            if (item.size < 104857600) {
                return true;
            } else {
                toastr.error("Sorry, the file size is too big, 100MB Maximun", "Large file");
                return false;
            }
        }
    });

    return {
        uploaderAudio: uploaderAudio,
        uploaderVideo: uploaderVideo,
        singVideoUploader: singVideoUploader,
        singAudioUploader: singAudioUploader
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

    function addToMyFavourite(item){
        var deferred = $q.defer();
        var config = {
            dataType: "json",
            contentType: "application/json; charset=utf-8",
        }

        $http.post('AddFavourite', JSON.stringify(item),config).then(function (response) {
            deferred.resolve(response);
        }, function (error) {
            deferred.resolve({ Status: 500 });
        });
        return deferred.promise;
    }

    return {
        jumpAlbumPage: jumuPage,
        addToMyFavourite: addToMyFavourite
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
