duckyData.controller('musicFetchCreateCtrl', function ($scope, $http, $sce, $location, $interval, $timeout, $rootScope, toastr, musicFetchFactory, duckyDataFileUploader) {
    $scope.musicFetchCreateData = {
        file: null,
        buttonLogo: 'GO',
    }
    $scope.musicUploaderUICtrl = {
        showUploader : false
    }

    $scope.audioFileUploader = duckyDataFileUploader.uploader;

    console.log($scope.audioFileUploader);
   // $rootScope.scoundClod = SC;
    SC.initialize({
        client_id: 'd468660bb9056f5d8e48361dd5327c80',
        redirect_uri: 'http://localhost:8102/MusicFetch/CallBack'
    });

    $scope.connectFunction = function () {

        console.log(SC.isConnected());
        SC.connect(function () {
            SC.get('/me', function (data) {
                $('#name').text(data.username);
            });
        });
    }

    $scope.textFunction = function(){
        console.log(SC.isConnected());
        console.log(SC);
        SC.upload({
            file: document.querySelector('input[type=file]').files[0],
            title: 'This is my sound'
        }).then(function(track){
            alert('Upload is done! Check your sound at ' + track);
        });
    }
    $timeout(function () {
        $scope.musicUploaderUICtrl.showUploader = true;
    }, 500);
    
});