duckyData.controller('homeCtrl', function ($scope, $http, $window, FileUploader, toastr) {
    $scope.singAudioUploader = new FileUploader();

    $scope.homePageUICtrl = {
        disableIfentifyButton: false
    }

    $scope.singAudioUploader.filters.push({
        name: 'singleAudioFilter',
        fn: function (item, options) {
            if ($scope.singAudioUploader.queue.length == 0) {
                return true
            } else {
                toastr.error('Sorry, you can only indetify one file each time');
                return false;
            }
        }
    });

    $scope.singAudioUploader.filters.push({
        name: 'mediaFileTypeFilter',
        fn: function (item, options) {
            var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';

            var userAgent = $window.navigator.userAgent;

            console.log(userAgent);

            var browsers = { chrome: /chrome/i, safari: /safari/i, firefox: /firefox/i, ie: /internet explorer/i };
            
            var mimeTypeSctring ='|mpeg|wav|x-wav|x-pn-wav|ogg|flac|x-ms-wma|mp4|avi|x-matroska|x-troff-msvideo|msvideo|x-msvideo|x-flv|x-ms-wmv|';

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
                toastr.error('We only support WAVE, FLAC, OGG, MP3, AAC, AMR, WMA, MP4, MKV, FLV, AVI', 'Unrecongnize File Type');
                return false;
            }
        }
    });

    $scope.inputTagTrigger = function (tag) {
        if ($scope.singAudioUploader.queue.length > 0) {
            // have file already
            toastr.error('Sorry, you can only indetify one file each time');
        } else {
            $(tag).click();
        }
    }

    $scope.identifyAudio = function (optionId) {
        $scope.homePageUICtrl.disableIfentifyButton = true;
        var config = {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }
        var fd = new FormData();
        fd.append('input', $scope.singAudioUploader.queue[0]._file);
        toastr.success('Uploading file to the system, please wait')
        $http.post('http://myvmlab.senecacollege.ca:5340/MusicFetch/_MediaInput/'+optionId, fd, config).then(function (response) {
            $scope.homePageUICtrl.disableIfentifyButton = false;
            $window.location.href = response.data
        }, function (error) {
            $scope.homePageUICtrl.disableIfentifyButton = false;
            toastr.error('Sorry, cannot fild the metadata');
        });
    }
});