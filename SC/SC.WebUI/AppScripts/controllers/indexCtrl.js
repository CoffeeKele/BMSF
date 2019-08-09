angular.module("scApp")
    .controller("msgListCtrl", ['$rootScope', '$scope', '$filter', '$compile', '$cookieStore', function ($rootScope, $scope, $filter, $compile, $cookieStore) {
        $scope.init = function () {
            $scope.Data = {
                Msgs: []
            }
            $scope.loginUser = "";

            var global = $cookieStore.get('Global');
        }

        $scope.logout = function () {
            $cookieStore.remove('Global');
            location.href = "/Home/Logout";
        }
        $scope.hide = function () {
            $scope.dialog.close();
        }
        $scope.changePass = function () {
            var html = $('<div></div>').load('Views/System/ChangeMyPass.html');

            $scope.dialog = BootstrapDialog.show({
                size: BootstrapDialog.SIZE_SMALL,
                title: '修改密码',
                message: html,
                onshow: function (dialog) {
                    dialog.getModalHeader().css('background-color', '#08c');
                    dialog.getModalHeader().css('color', '#fff');
                    dialog.getModalFooter().hide();
                    var obj = dialog.getModalBody().contents();
                    $compile(obj)($scope);
                }
            });
        };
        $scope.changeAccount = function (id) {
            var html =$('<div></div>').load('Views/System/MyAccount.html');

            $scope.dialog = BootstrapDialog.show({
                size: BootstrapDialog.SIZE_SMALL,
                title: '个人档案',
                message: html,
                onshow: function (dialog) {
                    dialog.getModalHeader().css('background-color', '#08c');
                    dialog.getModalHeader().css('color', '#fff');
                    dialog.getModalFooter().hide();
                    var obj = dialog.getModalBody().contents();
                    $compile(obj)($scope);
                }
            });
        };
        $scope.init();

    }])

.controller("menuCtrl", ['$scope', '$state', '$location', 'cloudAdminUi', 'roleModuleRes', function ($scope, $state, $location, cloudAdminUi, roleModuleRes) {
    $scope.init = function () {
        $scope.menus = [];
        roleModuleRes.query(function (data) {
            $scope.menus = data;
            $scope.selectMent = null;
            var arr = $.grep($scope.menus, function (item, i) {
                if (item.Target == "2") {
                    if (item.Url != null) {
                        var url = encodeURI(item.Url);
                        return $location.$$url.indexOf(url) > -1;
                    } else {
                        return false;
                    }
                }
                else {
                    var url = "";
                    var stateName = "";
                    if (item.Url != null) {
                        var stateArr = item.Url.split(".");
                        $.each(stateArr, function (i, item) {
                            stateName = stateName != "" ? stateName + "." + item : item;
                            var p = $state.get(stateName);
                            if (p != null) {
                                url = url + p.url;
                            }
                        });
                    }
                    if (url != "") {
                        url = encodeURI(url.replace(":id", ""));
                        return url != null && $location.$$url.indexOf(url) > -1;
                    }
                    else {
                        return false;
                    }
                }
            });
            if (arr.length > 0) {
                $scope.selectMent = arr[0];
            }
            cloudAdminUi.handleSidebar();
        });
    }
    //$scope.displayblock = {
    //    "display": "block"
    //}
    //$scope.displaynone = {
    //    "display": "none"
    //}
    var getTarget = function (t) {
        if (t.tagName === "A") {
            return t;
        } else if (t.tagName === "IMG") {
            return $(t).parent().parent();
        } else {
            return $(t).parent();
        }
    }
    $scope.init();
    $scope.menuClick = function ($event) {
        var target = getTarget($event.target);//$event.target.closest("a");
        var last = $('.has-sub.open', $('.sidebar-menu'));
        last.removeClass("open");
        //last.children(".sub").children(".active").removeClass("active");
        $('.arrow', last).removeClass("open");
        $('.sub', last).slideUp(200);
        var thisElement = $(target);
        var slideOffeset = -200;
        var slideSpeed = 200;

        var sub = $(target).next();
        if (sub.is(":visible")) {
            $('.arrow', $(target)).removeClass("open");
            $(target).parent().removeClass("open");
            sub.slideUp(slideSpeed, function () {
                if ($('#sidebar').hasClass('sidebar-fixed') === false) {
                    App.scrollTo(thisElement, slideOffeset);
                }
                cloudAdminUi.handleSidebarAndContentHeight();
            });
        } else {
            $('.arrow', jQuery(target)).addClass("open");
            $(target).parent().addClass("open");
            sub.slideDown(slideSpeed, function () {
                if ($('#sidebar').hasClass('sidebar-fixed') === false) {
                    App.scrollTo(thisElement, slideOffeset);
                }
                cloudAdminUi.handleSidebarAndContentHeight();
            });
        }
    }
    $scope.subMenuClick = function ($event, url, way) {

        var last = $('.has-sub', $('.sidebar-menu'));
        last.children(".sub").children(".active").removeClass("active");

        var target = getTarget($event.target);//$event.target.closest("a");
        // jQuery(target).parent().parent().children(".active").removeClass("active");
        jQuery(target).parent().addClass("active");
        if (way === '1') {
            $state.go(url, { id: 0 });
        } else if (way === '2') {
            $location.url(url);
        } else {
            $state.go(url);
        }
    }

    $scope.active = function (menu) {
        // alert($scope.selectMent.Url + " " + menu.Url + " " + $scope.selectMent.SuperModuleId + " " + menu.ModuleId);
        if ($scope.selectMent != null && ($scope.selectMent.Url == menu.Url || $scope.selectMent.SuperModuleId == menu.ModuleId)) {
            return true;
        }
        else {
            return false;
        }
    }
}])