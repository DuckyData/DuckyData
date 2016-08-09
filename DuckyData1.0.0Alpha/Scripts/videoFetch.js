// view controller
duckyData.controller('videoFetchCtrl', function ($scope, $timeout, GAPIFactory, musicFetchFactory, toastr) {
    var OAUTH2_CLIENT_ID = '254706105392-sac4crqcmko7lagnmkng0krfsdg1ongg';
    var OAUTH2_SCOPES = ['https://www.googleapis.com/auth/youtube'];

    // helper function to get param from URL
    function getUrlParameter(sParam) {
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;
        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : sParameterName[1];
            }
        }
    };

    $scope.videoFetchData = {
        videoList: [],
        selectedVideo: null,
        player:null
    }

    $scope.videoFetchDataPageInfo = {
        nextPageToken: null,
        prevPageToken: null
    }
    $scope.videoFetchDataUICtrl= {
        disablePrev: true,
        disableNext: false
    }

    $timeout(function () {
        GAPIFactory.searchVideo(getUrlParameter('video'), { nextPageToken: null, prevPageToken: null }).then(function (searchResult) {
            console.log(searchResult);
            $scope.videoFetchDataPageInfo.nextPageToken = searchResult.nextPageToken;
            $scope.videoFetchDataPageInfo.prevPageToken = searchResult.prevPageToken;
            $scope.videoFetchData.videoList = searchResult.videoList;
        });
    }, 2000)

    $scope.playVideo = function () {
        if ($scope.videoFetchData.player) {
            $scope.videoFetchData.player.loadVideoById($scope.videoFetchData.selectedVideo.id.videoId)
        } else {
            onYouTubeIframeAPIReady();
        }
    }
    
    function onYouTubeIframeAPIReady() {
        //document.getElementById("player").remove();
        $scope.videoFetchData.player = new YT.Player('player', {
            height: '390',
            width: '640',
            videoId: $scope.videoFetchData.selectedVideo.id.videoId,
            events: {
                'onReady': onPlayerReady
            }
        });
    }

    function onPlayerReady(event) {
        $scope.videoFetchData.player.playVideo();
    }

    $scope.stopVideo = function () {
        $scope.videoFetchData.player.stopVideo();
    }

    $scope.pageNavigation = function (pageType) {
        if (pageType > 0) {
            // go next
            GAPIFactory.searchVideo(getUrlParameter('video'), { nextPageToken: $scope.videoFetchDataPageInfo.nextPageToken, prevPageToken: null }).then(function (searchResult) {
                console.log(searchResult);
                $scope.videoFetchDataPageInfo.nextPageToken = searchResult.nextPageToken;
                $scope.videoFetchDataPageInfo.prevPageToken = searchResult.prevPageToken;
                if (searchResult.nextPageToken) {
                    $scope.videoFetchDataUICtrl.disableNext = false;
                } else {
                    $scope.videoFetchDataUICtrl.disableNext = true;
                }

                if (searchResult.prevPageToken) {
                    $scope.videoFetchDataUICtrl.disablePrev = false;
                } else {
                    $scope.videoFetchDataUICtrl.disablePrev = true;
                }
                $scope.videoFetchData.videoList = searchResult.videoList;
            })
        } else {
            // go prev
            GAPIFactory.searchVideo(getUrlParameter('video'), { nextPageToken: null, prevPageToken: $scope.videoFetchDataPageInfo.prevPageToken }).then(function (searchResult) {
                console.log(searchResult);
                $scope.videoFetchDataPageInfo.nextPageToken = searchResult.nextPageToken;
                $scope.videoFetchDataPageInfo.prevPageToken = searchResult.prevPageToken;
                if (searchResult.nextPageToken) {
                    $scope.videoFetchDataUICtrl.disableNext = false;
                } else {
                    $scope.videoFetchDataUICtrl.disableNext = true;
                }

                if (searchResult.prevPageToken) {
                    $scope.videoFetchDataUICtrl.disablePrev = false;
                } else {
                    $scope.videoFetchDataUICtrl.disablePrev = true;
                }
                $scope.videoFetchData.videoList = searchResult.videoList;
            })
        }
    }

    $scope.addToMyVideoFavourite = function (video) {

        musicFetchFactory.addToMyFavourite({ VideoId: video.id.videoId, VideoTitle: video.snippet.title, VideoImg: video.snippet.thumbnails.default.url ,VideoURL:"https://www.youtube.com/watch?v=" + video.id.videoId}).then(function (data) {
            if (data.data.Status == 200) {
                toastr.success(data.data.message);
            } else {
                toastr.error(data.data.message);
            }
        })
    }
});
