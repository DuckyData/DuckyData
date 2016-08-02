duckyData.controller('editUserProfileCtrl', function ($scope) {

    $scope.editUserProfileData = {
        FirstName: null,
        LastName: null,
        PhoneNumber: null
    }

    $scope.editUserProfileMsg = {
        fname: null,
        lname: null,
        phone: null
    }

    $scope.editUserProfileUI = {
        disableButton:false
    }

    $(document).ready(function () {
        $("#FirstName").bind("change paste keyup", function () {
            var fname = $('#FirstName').val()
            if (fname && fname.length > 20) {
                setFnameMsg("First name must no longer than 20 characters");
            } else {
                setFnameMsg("");
            }
        });

        $("#LastName").bind("change paste keyup", function () {
            var lname = $('#LastName').val()
            if (lname && lname.length > 20) {
                setLnameMsg("Last name must no longer than 20 characters");
            } else {
                setLnameMsg("");
            }
        });

        $("#PhoneNumber").bind("change paste keyup", function () {
            var phone = $('#PhoneNumber').val();
            if (phone && phone.length == 10) {
                var re = /^\d{10}$/;
                if (re.test(phone)) {
                    setPhoneMsg("");
                } else {
                    setPhoneMsg("Please use this format 9999999999");
                }
                
            } else {
                if (phone == "" || phone == undefined) {
                    setPhoneMsg("");
                } else {
                    setPhoneMsg("Please enter 10 digits phone number");
                }
                
            }
        });
    });
  
    function setFnameMsg(msg) {
        $scope.$apply(function () {
            $scope.editUserProfileMsg.fname = msg;
        })
        disableSaveBtn();
    }

    function setLnameMsg(msg) {
        $scope.$apply(function () {
            $scope.editUserProfileMsg.lname = msg;
        })
        disableSaveBtn();
    }

    function setPhoneMsg(msg) {
        $scope.$apply(function () {
            $scope.editUserProfileMsg.phone = msg;
        })
        disableSaveBtn();
    }

    function disableSaveBtn() {
        if (!$scope.editUserProfileMsg.phone && !$scope.editUserProfileMsg.lname && !$scope.editUserProfileMsg.fname) {
            $scope.$apply(function () {
                $scope.editUserProfileUI.disableButton = false;
            })
        } else {
            $scope.$apply(function () {
                $scope.editUserProfileUI.disableButton = true;
            })
        }
    }
});