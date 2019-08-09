var homeApp = angular.module('scApp', [
        'ui.router',
        'ngResource',
        'ngCookies',
        'Utility',
        'extentDirective',
        'extentFilter',
        'extentComponent',
        'extentService',
        'angular-loading-bar',
        'ngAnimate',
        'angularFileUpload',
        //'ngVis'
])
    //固定IP
    .constant("resourceBase", "")
    //获取角色菜单信息
    .factory("roleModuleRes", ['$resource', 'resourceBase', function ($resource, resourceBase) {
        return $resource('api/module/:id', { id: '@id' });
    }])
    //获取一些公共的信息
    .factory("commonRes", ['$resource', 'resourceBase', function ($resource, resourceBase) {
        return $resource("api/Common/:id", { id: "@id" });
    }])
    //获取用户信息
    .factory("userRes", ['$resource', 'resourceBase', function ($resource, resourceBase) {
        return $resource("api/user/:id", { id: "@id" });
    }])
    //获取角色信息
    .factory("roleRes", ['$resource', 'resourceBase', function ($resource, resourceBase) {
        return $resource("api/role/:id", { id: "@id" });
    }])