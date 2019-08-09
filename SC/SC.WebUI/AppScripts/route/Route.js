angular.module('scApp')
.config([
        '$stateProvider', '$urlRouterProvider', '$locationProvider', 'cfpLoadingBarProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, cfpLoadingBarProvider) {

            cfpLoadingBarProvider.spinnerTemplate = '<div class="loading"><span class="fa fa-spinner">加载中...</span></div>';
            cfpLoadingBarProvider.latencyThreshold = 800;
            $urlRouterProvider.when("/", "/angular/MyDesk").otherwise('/');

            $locationProvider.html5Mode(true);

            //Demo
            $stateProvider.state('Demo', { url: '/angular/Demo', templateUrl: '/Views/Demo/DemoOne.html', controller: 'demoCtrl' });


            //首页
            $stateProvider.state('MyDesk', { url: '/angular/MyDesk', templateUrl: '/Views/System/MyDesk.html', controller: 'myDeskCtrl' });


            //用户管理
            $stateProvider.state('UserList', { url: '/angular/UserList', templateUrl: '/Views/System/UserList.html', controller: 'userCtrl' });
            $stateProvider.state('UserAdd', { url: '/angular/UserEdit', templateUrl: '/Views/System/UserEdit.html', controller: 'userEditCtrl' });
            $stateProvider.state('UserEdit', { url: '/angular/UserEdit/:id', templateUrl: '/Views/System/UserEdit.html', controller: 'userEditCtrl' });

            //角色管理
            $stateProvider.state('RoleList', { url: '/angular/RoleList', templateUrl: '/Views/System/RoleList.html', controller: 'roleCtrl' });
            $stateProvider.state('RoleAdd', { url: '/angular/RoleEdit', templateUrl: '/Views/System/RoleEdit.html', controller: 'roleEditCtrl' });
            $stateProvider.state('RoleEdit', { url: '/angular/RoleEdit/:id', templateUrl: '/Views/System/RoleEdit.html', controller: 'roleEditCtrl' });


        }
]);