angular.module("scApp")
.controller("roleCtrl", ['$scope', '$http', '$location', '$rootScope', 'roleRes', 'utility', function ($scope, $http, $location, $rootScope, roleRes, utility) {
    $scope.Data = {};
    $scope.loadRoles = function () {
        $scope.options = {
            buttons: [],//需要打印按钮时设置
            ajaxObject: roleRes,//异步请求的res
            params: { roleName: "" },
            success: function (data) {//请求成功时执行函数
                $scope.Data.Roles = data.Data;
            },
            pageInfo: {//分页信息
                CurrentPage: 1, PageSize: 10
            }
        }
    }

    $scope.RoleDelete = function (item) {
        if (confirm("您确定删除该角色信息吗?")) {
            roleRes.delete({ id: item.RoleId }, function (data) {
                utility.message(data.ResultMessage);

                if (data.ResultCode == 1) {
                    $scope.Data.Roles.splice($scope.Data.Roles.indexOf(item), 1);
                }
            });
        }
    }

    $scope.loadRoles();
}])
.controller("roleEditCtrl", ['$scope', '$http', '$location', '$stateParams', '$rootScope', 'dictionary', 'roleRes', 'roleModuleRes', function ($scope, $http, $location, $stateParams, $rootScope, dictionary, roleRes, roleModuleRes) {
    $scope.Data = {};
    $scope.RoleId = "0"
    if ($stateParams.id) {
        $scope.RoleId = $stateParams.id;
        roleRes.get({ id: $stateParams.id }, function (data) {
            $scope.Data.Role = data.Data;
            $scope.RoleId = data.Data.RoleId;
        });
    }
    else {
        $scope.Data.Role = {};
        $scope.Data.Role.Status = true;
        $scope.Data.Role.RoleType = "Normal";
    }

    $scope.change = function () {
        $('#roleName').trigger('blur');
    };


    var treeHelper = new TreeHelper(roleModuleRes, "#moduleTree", { id: $scope.RoleId, type: "tree", loadTreeByRole: true });

    $scope.expandAll = function () {
        treeHelper.expandAll();
    }

    $scope.collapseAll = function () {
        treeHelper.collapseAll();
    }
    $scope.chooseAll = function () {
        treeHelper.checkAll();
    }

    $scope.cancelChooseAll = function () {
        treeHelper.uncheckAll();
    }
    $scope.save = function () {
        $scope.Data.Role.CheckModuleList = treeHelper.getChecked();
        roleRes.save($scope.Data.Role, function (data) {
            $location.url('/angular/RoleList');

        });
    }

    treeHelper.loadTree();
}]);
