// view controller
duckyData.controller('videoFetchCtrl', function ($scope, $location, $timeout, GAPIFactory) {
    var OAUTH2_CLIENT_ID = '254706105392-sac4crqcmko7lagnmkng0krfsdg1ongg';
    var OAUTH2_SCOPES = ['https://www.googleapis.com/auth/youtube'];

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
        GAPIFactory.searchVideo($location.search().video,{nextPageToken: null, prevPageToken: null}).then(function (searchResult) {
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
                'onStateChange': onPlayerStateChange
            }
        });
    }

    function onPlayerReady(event) {
        event.target.playVideo();
    }
    var done = true;
    function onPlayerStateChange(event) {
        if (event.data == YT.PlayerState.PLAYING && !done) {
            setTimeout($scope.stopVideo, 6000);
            done = true;
        }
    }
    $scope.stopVideo = function () {
        $scope.videoFetchData.player.stopVideo();
    }

    $scope.pageNavigation = function (pageType) {
        if (pageType > 0) {
            // go next
            GAPIFactory.searchVideo($location.search().video, { nextPageToken: $scope.videoFetchDataPageInfo.nextPageToken, prevPageToken:null }).then(function (searchResult) {
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
            GAPIFactory.searchVideo($location.search().video, { nextPageToken: null, prevPageToken: $scope.videoFetchDataPageInfo.prevPageToken }).then(function (searchResult) {
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
});
