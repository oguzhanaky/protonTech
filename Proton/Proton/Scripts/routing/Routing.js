var app = angular.module('MyApp', ['ngRoute']);

app.config(['$locationProvider', '$routeProvider',
    function ($locationProvider, $routeProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    }).hashPrefix('!');

    $routeProvider
    .when('/', {
        controller: 'MainController',
        templateUrl: '/Templates/Index.html'
    })
    .when('/Home/AboutUs', {
        controller: 'UserController',
        templateUrl: '/Templates/AboutUs.html'
    })
    .when('/Home/CompletedTasks', {
        controller: 'CompletedTasksController',
        templateUrl: '/Templates/CompletedTasks.html'
    })
    .when('/Home/OngoingTasks', {
        controller: 'CompletedTasksController',
        templateUrl: '/Templates/OngoingTasks.html'
    })
    .when('/Home/Contact', {
        controller: 'ContactController',
        templateUrl: '/Templates/Contact.html'
    })
    .otherwise({
        redirectTo: '/'
    })   
    .when('/Account/Login', {
        controller: 'UserController',
        templateUrl: '/Templates/Login.html'
    });
}]);