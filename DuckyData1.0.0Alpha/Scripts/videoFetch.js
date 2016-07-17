// view controller
duckyData.controller('videoFetchCtrl', function ($scope) {
    var OAUTH2_CLIENT_ID = '254706105392-sac4crqcmko7lagnmkng0krfsdg1ongg';
    var OAUTH2_SCOPES = [
      'https://www.googleapis.com/auth/youtube'
    ];

    $scope.googleApiClientReady = function() {
        gapi.auth.init(function () {
            window.setTimeout(checkAuth, 1);
        });
    }

    function checkAuth() {
        gapi.auth.authorize({
            client_id: OAUTH2_CLIENT_ID,
            scope: OAUTH2_SCOPES,
            immediate: false
        }, handleAuthResult);
    }

    function handleAuthResult(authResult) {
        if (authResult && !authResult.error) {
            gapi.client.load('youtube', 'v3', function () {
                search();
            });
      } 
    }


    function search(){
        var q = 'cat';

        console.log(gapi);
        var request = gapi.client.youtube.search.list({
            q: q,
            maxResults: 30,
            nextPageToken: 'CB4QAA',
            part: 'snippet'
        });

        request.execute(function (response) {
            var str = JSON.stringify(response.result);
            console.log(response);
            $('#search-container').html('<pre>' + str + '</pre>');
        });
    }
});
