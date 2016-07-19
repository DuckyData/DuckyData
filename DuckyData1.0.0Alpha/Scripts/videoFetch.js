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
        nextPageToken : null
    }

    $timeout(function () {
        GAPIFactory.searchVideo($location.search().video).then(function (searchResult) {
            console.log(searchResult);
            $scope.videoFetchData.videoList = searchResult.data.videoList;
        });
    }, 2000)


    $scope.playVideo = function () {
        $scope.videoFetchData.player = null;
        onYouTubeIframeAPIReady();
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
            setTimeout(stopVideo, 6000);
            done = true;
        }
    }
    function stopVideo() {
        $scope.videoFetchData.player.stopVideo();
    }
});
