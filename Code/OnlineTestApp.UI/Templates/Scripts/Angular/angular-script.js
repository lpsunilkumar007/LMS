// Application module
var onlineTestApp = angular.module('onlineTestApp', ['ngGrid']);
onlineTestApp.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
}]);

onlineTestApp.controller('TopNavController', function ($scope, $http, $rootScope) {
    $scope.loadTopPanelUserDetails = function () {
        $http.get("/Menu/GetTopMenuDetails")
            .then(function (response) {
                console.log(response);
                $scope.FullName = response.data.FullName;
                // $rootScope.IsMenuOpen = response.data.ApplicationUserSettings.IsMenuOpen;              
                if (response.data.ApplicationUserSettings.IsMenuOpen) {
                    // if ($("#closeMenu").length > 0) {
                    LeftMenuToggle($("menu-toggle"));
                    // }
                }
                $scope.DataLoaded = true;
            });
    }
    $scope.loadTopPanelUserDetails();
});


onlineTestApp.controller('LookUpController', function ($scope, $http, $rootScope) {
    $scope.GetLookUpValue = function (lookUpCode) {
        $http.get("/lookup/getlookupvalues?lookupdomaincode=" + lookUpCode)
            .then(function (response) {
                console.log(response);
                $scope.LooUpValues = response.data;
                // $scope.DataLoaded = true;
            });
    }

});

onlineTestApp.controller('ViewUsersController', function ($scope, $http, $rootScope) {
    $scope.currentDataPosition = 1;
    $scope.isScrollDatadataFetched = false;
    $scope.GetMoreData = function (page_index) {
        $scope.DataLoaded = false;
    
        $http.get("/manageapplicationuser/getusersdetail", {
            cache: false,
            params: {
                //data: $("#Form1").serialize(),
                page: page_index
            }
        }).then(function (response) {
            console.log(response);
            
            //$scope.searchData;
            if (page_index != 1) {
                $scope.searchData = $scope.searchData.concat(response.data);
                //$scope.searchData.concat(response.data);
                $scope.dtgSearchResult.$gridServices.DomUtilityService.RebuildGrid($scope.dtgSearchResult.$gridScope, $scope.dtgSearchResult.ngGrid);
                $scope.isScrollDatadataFetched = false;
            }
            else {
                $scope.searchData = response.data;
            }
            $scope.DataLoaded = true;
        },
            function (data) {
                // Handle error here
            });
    }
    var sortNgGrid = function (a, b) {
        alert(a);
       
        //var NUMBER_GROUPS = /(-?\d*\.?\d+)/g;

        //var myAwesomeSortFn = function (a, b) {

        //    var aa = String(a).split(NUMBER_GROUPS),
        //        bb = String(b).split(NUMBER_GROUPS),
        //        min = Math.min(aa.length, bb.length);

        //    for (var i = 0; i < min; i++) {
        //        var x = parseFloat(aa[i]) || aa[i].toLowerCase(),
        //            y = parseFloat(bb[i]) || bb[i].toLowerCase();
        //        if (x < y) return -1;
        //        else if (x > y) return 1;
        //    }

            return 0;
        };
    $scope.dtgSearchResult = {
        data: 'searchData',
        columnDefs: 'gridColumnDefs'
    };

    $scope.gridColumnDefs = [
        {
            displayName: "User Name",
            field: "UserName",
            width: 200,
            sortFn: sortNgGrid
            
        },
        {
            displayName: "Company",
            field: "UserCompany.CompanyName",
            width: 100
            
        },
        {
            displayName: "User Role",
            field: "ApplicationUserRoles.UserRoleName",
            width: 100
           
        },
        {
            displayName: "Created At",
            field: "CreatedDateTime_ToLongDateString",
            width: 100
            
        },
        {
            field: "Action",
            width: 200,            
            cellTemplate: '<div class="tr-action"><div class="dropdown"> <button class="btn btn-success dropdown-toggle" type="button" id="dropdownMenu2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> Actions </button><div class="dropdown-menu" aria-labelledby="dropdownMenu2"> <a href="/manageapplicationuser/edituser/?applicationUserId={{row.entity.ApplicationUserId}}" class="dropdown-item">Edit <i class="fas fa-edit"></i></a> <a href="javascript:void(0)" data-inline-delete="true" data-succuss-message="User deleted successfully" data-process-url="/manageapplicationuser/deleteuser?applicationUserId={{row.entity.ApplicationUserId}}" class="dropdown-item">Delete<i class="fas fa fa-trash"></i></a></div></div></div>'
        }
    ];


    $scope.$on('ngGridEventScroll', function () {
      
        if ($scope.isScrollDatadataFetched == false) {
            
            $scope.currentDataPosition++;
            $scope.GetMoreData($scope.currentDataPosition);
            $scope.isScrollDatadataFetched = true;
        }
    });

});


