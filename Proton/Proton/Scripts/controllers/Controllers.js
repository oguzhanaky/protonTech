var app = angular.module('MyApp');
TEST

app.controller('MainController', ['$scope', 'forecast', function ($scope, forecast) {
    $scope.title = 'This Month\'s Bestsellers';
    $scope.promo = 'The most popular books this month.';
    $scope.products = [
      {
          name: 'Harry Potter & The Prisoner of Azkaban',
          price: 11.99,
          pubdate: new Date('1999', '07', '08'),
          cover: 'http://upload.wikimedia.org/wikipedia/en/b/b4/Harry_Potter_and_the_Prisoner_of_Azkaban_(US_cover).jpg',
          likes: 0,
          dislikes: 0
      },
      {
          name: 'Ready Player One',
          price: 7.99,
          pubdate: new Date('2011', '08', '16'),
          cover: 'http://upload.wikimedia.org/wikipedia/en/a/a4/Ready_Player_One_cover.jpg',
          likes: 0,
          dislikes: 0
      }
    ];
    $scope.plusOne = function (index) {
        $scope.products[index].likes += 1;
    };
    $scope.minusOne = function (index) {
        $scope.products[index].dislikes += 1;
    };

    forecast.success(function (data) {
        $scope.fiveDay = data;
    });

    $scope.clicked = function(){   

        $location.path('/test.html');
    }

    $scope.runTest = function (myTest) {
        var config = {
            params: { myTest: "myTest" }
        }
        $http.post('/Home/runTest', config).success(function (data) {
            if (data != null && data.success) {
                console.log(data);
            }
        }).error(function (error) {
            console.log("error ", error);
        });
    };
}]);

app.controller('UserController', ['$scope', '$routeParams', function ($scope, $routeParams) {
    $scope.Message = "This is User  Page with query string id value = ";
}]);

app.controller('CompletedTasksController11', ['$scope', '$routeParams', function ($scope, $routeParams) {
    var post = $http({
        method: "GET",
        url: "/Home/AjaxMethod",
        dataType: 'json',
        data: { name: $scope.Name },
        headers: { "Content-Type": "application/json" }
    });

    post.success(function (data, status) {
        console.log(data);
        //$window.alert("Hello: " + data.Name + " .\nCurrent Date and Time: " + data.DateTime);
    });

    post.error(function (data, status) {
        $window.alert(data.Message);
    });
}]);

app.controller('CompletedTasksController', function ($scope, $http, $window) {
    $scope.ButtonClick = function () {
        var post = $http({
            method: "GET",
            url: "/Home/GetProjects",
            dataType: 'json',
            data: { name: $scope.Name },
            headers: { "Content-Type": "application/json" }
        });
 
        post.success(function (data, status) {
            $scope.projects = data;
            console.log(data);
            //$window.alert("Hello: " + data[0].ProjeAdi + " .\nCurrent Date and Time: " + data.DateTime);
        });
 
        post.error(function (data, status) {
            $window.alert(data.Message);
        });
    }
});