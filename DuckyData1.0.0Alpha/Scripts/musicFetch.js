// view controller
duckyData.controller('musicFetchCtrl', function ($scope, $http, $sce, $interval, $timeout, toastr, musicFetchFactory) {
   
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

    $scope.audioPreviewCtrl = {
        loadAnotherOfAlbumBusy: false,
        checkStage:null
    }

    $scope.albumPagination = {
        pageList: [],
        totalPageNumber:null,
        current: null,
        begin:0
    }

    $scope.albumFilter = {
        params: {
            output: "jsonp",
            callback: 'JSON_CALLBACK',
            q: getUrlParameter('album'),
            index: 0,
        }
    }

    // album init call, get possible based on filter
    $scope.albumFetch = function () {
        if ($scope.albumFilter.params.q) {
            $http.jsonp("https://api.deezer.com/search/album", $scope.albumFilter).success(function (data) {
                if (data != null) {
                    if (data.total == 1) {
                        $scope.musicFetchData.possibleAlbunList = data.data;
                        $scope.musicFetchData.album = data.data[0];
                        $scope.musicFetchUICtrl.showTrackList = true;
                        $scope.musicFetchUICtrl.showPreviewWindow = true;
                        $scope.getAlbumTrack($scope.musicFetchData.album.tracklist);
                        createPageList(0);
                    } else if (data.total > 1) {
                        $scope.musicFetchUICtrl.showPossibleAlbum = true;
                        $scope.musicFetchData.possibleAlbunList = data.data;
                        createPageList(Math.ceil(data.total / 25));
                        $scope.albumPagination.current = $scope.albumFilter.params.index == 0 ? 1 : $scope.albumFilter.params.index % 25;
                    } else {
                        toastr.error('We cannot find anything public accessable infornation for this album', 'Sorry');
                        createPageList(0);
                        $scope.musicFetchData.showNoMachedResult = false;
                    }
                } else {
                    $scope.musicFetchData.showNoMachedResult = true;
                }
            }).error(function (data) {
                toastr.error('Cannot find information about this album', 'Sorry');
            });
        } else {
            toastr.error('Album paramater not found!','Opps');
        }
    }(function(){}());
    
    // function to get track of a album
    $scope.getAlbumTrack = function(url) {
        var config = {
            params: {
                output: "jsonp",
                callback: 'JSON_CALLBACK',
                limit: 100
            }
        }
        $http.jsonp(url,config).success(function (data) {
            if (data != null) {
                $scope.musicFetchData.albumTrackList = data.data;
                $scope.musicFetchData.currentPreview = data.data[0];
                $scope.musicFetchUICtrl.showPreviewWindow= true;
                $scope.musicFetchUICtrl.showPossibleAlbum= false;
                $scope.musicFetchUICtrl.showTrackList = true;
            } else {
            }
        }).error(function (data) {
            $scope.data = data;
        });
    }

    // function to play or pause a preview 
    $scope.playPausePreview = function (operation) {
        var audio = document.getElementById("previewAudio");
        if (operation == 'play') {
            $scope.audioPreviewCtrl.checkStage = $interval(playWhenReady, 500);
        } else {
            audio.pause();
            audio.currentTime  = operation == 'reset' ? 0:audio.currentTime;
        }
    }

    // function to wait audio finish loading then play
    function playWhenReady() {
        var audio = document.getElementById("previewAudio");
        if (audio.readyState == 4) {
            audio.play();
            $interval.cancel($scope.audioPreviewCtrl.checkStage);
            $scope.audioPreviewCtrl.checkStage = null;
        }
    }
   
    // pagination functions
    function createPageList(pageTotal) {
        if (pageTotal >= 2) {
            for (var i = 0; i < pageTotal; i++) {
                $scope.albumPagination.pageList[i] = i + 1;
            }
        } else {
            $scope.albumPagination.pageList = [];
        }
        $scope.albumPagination.totalPageNumber = pageTotal;
    }

    function increasePage(head) {
        $scope.albumPagination.begin = head + 5 > $scope.albumPagination.totalPageNumber ? $scope.albumPagination.totalPageNumber : head + 5;
    }

    function decreasePage(tail) {
        $scope.albumPagination.begin = $scope.albumPagination.begin - 5;
    }

    $scope.jumpPage = function (page) {
        $scope.audioPreviewCtrl.loadAnotherOfAlbumBusy = true;
        // go forward
        if (page > $scope.albumPagination.current) {
            $scope.albumPagination.current = page >= $scope.albumPagination.totalPageNumber ? $scope.albumPagination.totalPageNumber : page;
            if ((page > 4) && (page % 5 == 1)) {
                $scope.albumPagination.begin =  page -1;
            }

            reloadAlbumWithPage($scope.albumPagination.current);
        // go backward
        } else if (page < $scope.albumPagination.current) {
            if (page>0 && page % 5 == 0) {
                $scope.albumPagination.begin = $scope.albumPagination.begin - 5;
            }
            $scope.albumPagination.current = page <= 1 ? 1 : page;
            reloadAlbumWithPage($scope.albumPagination.current);
        }
    }

    function reloadAlbumWithPage(pageNum) {
        $scope.musicFetchData.possibleAlbunList = [];
        var filter = {
            params: {
                output: "jsonp",
                callback: 'JSON_CALLBACK',
                q: getUrlParameter('album'),
                index: (pageNum-1)*25,
            }
        }

        musicFetchFactory.jumpAlbumPage(filter).then(function (result) {
            if (result.status == 'OK') {
                $scope.musicFetchData.possibleAlbunList = result.data.data;
                $scope.audioPreviewCtrl.loadAnotherOfAlbumBusy = false;
            }
        })
    }

    $scope.addToMyAudioFavourite = function (audio) {
        musicFetchFactory.addToMyFavourite({ MusicURL: audio.link, MusicTitle: audio.title_short, Artist: audio.artist.name, Album: $scope.musicFetchData.album.title, AlbumCover: $scope.musicFetchData.album.cover }).then(function (data) {
            if (data.data.Status == 200) {
                toastr.success(data.data.message);
            } else {
                toastr.error(data.data.message);
            }
        })
    }
});
