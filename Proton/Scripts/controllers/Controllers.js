var app = angular.module('MyApp');

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

app.controller('IndexController', ['$scope', '$routeParams', function ($scope, $routeParams) {
    $scope.Message = "This is User  Page with query string id value = ";
}]);

app.controller('UserController', ['$scope', '$routeParams', function ($scope, $routeParams) {
    $scope.Message = "This is User  Page with query string id value = ";
}]);

app.controller('ManagerController', function ($scope, $http, $routeParams, FileUploadService) {
    $scope.options = {
        language: 'en',
        allowedContent: true,
        entities: true
    };

    // Called when the editor is completely ready. 
    $scope.onReady = function () {
        // ... 
    };

    $scope.content = "<p> this is custom directive </p>";

    $scope.GetCompletedTasks = function () {
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
    };

    $scope.editObject = function (data) {
        console.log(data);
    };

    $scope.project = 
      {
          ProjeAdi: "",
          ProjeSehir: "",
          ProjeIlce: "",
          ProjeDurum: 0,
          ProjeAciklama: "",
          statusDesc: "Proje Durumu",
          ProjectFotos: []
      };
   
    $scope.projectStatus = [
      {
          completed: 1,
          desc: "Tamamlandı"
      },
      {
          completed: 0,
          desc: "Devam Ediyor"
      }
    ];

    $scope.setStatus = function (status,desc) {
        $scope.project.ProjeDurum = status;
        $scope.project.statusDesc = desc;
    };

    $scope.setEditStatus = function (status, desc) {
        $scope.editProjectObject.ProjeDurum = status;
        $scope.editProjectObject.statusDesc = desc;
    };

    $scope.editProject = function (project) {
        $scope.editProjectObject = project;
        console.log($scope.editProjectObject);
    };
    
    $scope.saveProject = function () {
        console.log($scope.project);
        var post = $http({
            method: "POST",
            url: "/Management/SaveProject",
            dataType: 'json',
            data: { project: $scope.project },
            headers: { "Content-Type": "application/json" }
        });

        post.success(function (data, status) {
            $scope.projects = data;
            console.log(data);
            //$window.alert("Hello: " + data[0].ProjeAdi + " .\nCurrent Date and Time: " + data.DateTime);
        });

        post.error(function (data, status) {
            //$window.alert(data.Message);
        });
    };

    $scope.GetProjectPhotos = function (projectObj) {
        console.log(projectObj.ProjeId);
        var post = $http({
            method: "POST",
            url: "/Management/GetProjectPhotos",
            dataType: 'json',
            data: { projectId: projectObj.ProjeId },
            headers: { "Content-Type": "application/json" }
        });

        post.success(function (data, status) {
            $scope.projectPhotoModel = data;
            console.log($scope.projectPhotoModel);
            //$window.alert("Hello: " + data[0].ProjeAdi + " .\nCurrent Date and Time: " + data.DateTime);
        });

        post.error(function (data, status) {
            $window.alert(data.Message);
        });
    };

    //SavePhoto
    $scope.Message = "";
    $scope.FileInvalidMessage = "";
    $scope.SelectedFileForUpload = null;
    $scope.FileDescription = "";
    $scope.FileProjectId = "";
    $scope.IsFormSubmitted = false;
    $scope.IsFileValid = false;
    $scope.IsFormValid = false;

    //Form Validation
    $scope.$watch("f1.$valid", function (isValid) {
        $scope.IsFormValid = isValid;
    });

    // THIS IS REQUIRED AS File Control is not supported 2 way binding features of Angular
    // ------------------------------------------------------------------------------------
    //File Validation
    $scope.ChechFileValid = function (file) {
        var isValid = false;
        if ($scope.SelectedFileForUpload != null) {
            if ((file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/gif') && file.size <= (512 * 1024) && file.size > 0) {
                $scope.FileInvalidMessage = "";
                isValid = true;
            }
            else {
                $scope.FileInvalidMessage = "Selected file is Invalid. (only file type png, jpeg and gif and 512 kb size allowed)";
            }
        }
        else {
            $scope.FileInvalidMessage = "Image required!";
        }
        $scope.IsFileValid = isValid;
    };

    //File Select event 
    $scope.selectFileforUpload = function (file) {
        $scope.SelectedFileForUpload = file[0];
    }
    //----------------------------------------------------------------------------------------

    //Save File
    $scope.SaveFile = function (projectId) {
        $scope.FileProjectId = projectId;
        console.log(projectId);
        $scope.IsFormSubmitted = true;
        $scope.Message = "";
        $scope.ChechFileValid($scope.SelectedFileForUpload);
        if ($scope.IsFormValid && $scope.IsFileValid) {
            $scope.loading = true;
            FileUploadService.UploadFile($scope.SelectedFileForUpload, $scope.FileProjectId).then(function (d) {
                $scope.loading = false;
                alert(d.Message);
                ClearForm();
            }, function (e) {
                $scope.loading = false;
                alert(e);
            });
        }
        else {
            $scope.Message = "All the fields are required.";
            $scope.loading = true;
            FileUploadService.UploadFile($scope.SelectedFileForUpload, $scope.FileProjectId).then(function (d) {
                $scope.loading = false;
                alert(d.Message);
                ClearForm();
            }, function (e) {
                $scope.loading = false;
                alert(e);
            });
        }
    };
    //Clear form 
    function ClearForm() {
        $scope.FileProjectId = "";
        //as 2 way binding not support for File input Type so we have to clear in this way
        //you can select based on your requirement
        angular.forEach(angular.element("input[type='file']"), function (inputElem) {
            angular.element(inputElem).val(null);
        });

        $scope.f1.$setPristine();
        $scope.IsFormSubmitted = false;
    }
});

app.controller('ManagementController', function ($scope, $http, $location) {

    //self.username.subscribe(function (data) {
    //    $("#val-password").trigger("change");
    //});

    $scope.Logon = function () {
        //webRequest.post("/api/Membership/Logon", JSON.stringify({ userName: self.username(), password: self.password() }), self.logonSucceeded);

        var post = $http({
            method: "POST",
            url: "/Membership/Logon",
            dataType: 'json',
            data: { UserName: $scope.username, Password: $scope.password },
            headers: { "Content-Type": "application/json" }
        });

        post.success(function (data,status) {
            console.log(data.Success);
            if (data.Success) {
                $location.path('/Home/Manager');
            } else {
                toastr.error('Kullanıcı Adı veya Şifre Yanlış', 'Giriş Hatası', { timeOut: 1000 });
            }
            //$window.alert("Hello: " + data[0].ProjeAdi + " .\nCurrent Date and Time: " + data.DateTime);
        });

        post.error(function (data, status) {
            $window.alert(data.Message);
        });
    }

    $scope.logonSucceeded = function (result) {
        if (result.success) {
            window.location.href = "/";
        }
    }

    //$scope.Logon = function () {
    //    console.log($scope.username + " " + $scope.password);
    //}
});

app.controller('CkeditorCtrl', function ($scope) {
    
});

app.controller('CompletedTasksController', function ($scope, $http, $window) {
    $scope.GetCompletedTasks = function () {
        var post = $http({
            method: "GET",
            url: "/Home/GetCompletedProjects",
            dataType: 'json',
            headers: { "Content-Type": "application/json" }
        });
 
        post.success(function (data, status) {
            $scope.completedProjects = data;
            console.log(data);
        });
 
        post.error(function (data, status) {
            $window.alert(data.Message);
        });
    }
});

app.controller('OnGoingTaskController', function ($scope, $http, $window) {
    $scope.GetOnGoingTasks = function () {
        var post = $http({
            method: "GET",
            url: "/Home/GetOnGoingTasks",
            dataType: 'json',
            headers: { "Content-Type": "application/json" }
        });

        post.success(function (data, status) {
            $scope.ongoingProjects = data;
            console.log(data);
        });

        post.error(function (data, status) {
            $window.alert(data.Message);
        });
    }
});

app.controller('ContactController', function ($scope, FileUploadService) {
    $scope.Message = "";
    $scope.FileInvalidMessage = "";
    $scope.SelectedFileForUpload = null;
    $scope.FileDescription = "";
    $scope.IsFormSubmitted = false;
    $scope.IsFileValid = false;
    $scope.IsFormValid = false;

    //Form Validation
    $scope.$watch("f1.$valid", function (isValid) {
        $scope.IsFormValid = isValid;
    });

    // THIS IS REQUIRED AS File Control is not supported 2 way binding features of Angular
    // ------------------------------------------------------------------------------------
    //File Validation
    $scope.ChechFileValid = function (file) {
        var isValid = false;
        if ($scope.SelectedFileForUpload != null) {
            if ((file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/gif') && file.size <= (512 * 1024)) {
                $scope.FileInvalidMessage = "";
                isValid = true;
            }
            else {
                $scope.FileInvalidMessage = "Selected file is Invalid. (only file type png, jpeg and gif and 512 kb size allowed)";
            }
        }
        else {
            $scope.FileInvalidMessage = "Image required!";
        }
        $scope.IsFileValid = isValid;
    };

    //File Select event 
    $scope.selectFileforUpload = function (file) {
        $scope.SelectedFileForUpload = file[0];
    }
    //----------------------------------------------------------------------------------------

    //Save File
    $scope.SaveFile = function () {
        $scope.IsFormSubmitted = true;
        $scope.Message = "";
        $scope.ChechFileValid($scope.SelectedFileForUpload);
        if ($scope.IsFormValid && $scope.IsFileValid) {
            FileUploadService.UploadFile($scope.SelectedFileForUpload, $scope.FileDescription).then(function (d) {
                alert(d.Message);
                ClearForm();
            }, function (e) {
                alert(e);
            });
        }
        else {
            $scope.Message = "All the fields are required.";
        }
    };
    //Clear form 
    function ClearForm() {
        $scope.FileDescription = "";
        //as 2 way binding not support for File input Type so we have to clear in this way
        //you can select based on your requirement
        angular.forEach(angular.element("input[type='file']"), function (inputElem) {
            angular.element(inputElem).val(null);
        });

        $scope.f1.$setPristine();
        $scope.IsFormSubmitted = false;
    }
})
.factory('FileUploadService', function ($http, $q) { // explained abour controller and service in part 2

    var fac = {};
    fac.UploadFile = function (file, fileProjectId) {
        var formData = new FormData();
        formData.append("file", file);
        //We can send more data to server using append         
        formData.append("projectId", fileProjectId);

        var defer = $q.defer();
        $http.post("/Data/SaveFiles", formData,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
        .success(function (d) {
            defer.resolve(d);
        })
        .error(function () {
            defer.reject("File Upload Failed!");
        });
        return defer.promise;
    }
    return fac;
});

app.controller('HeaderController', ['$scope', '$location', function ($scope, $location) {
    $scope.isActive = function (viewLocation) {
        console.log($location.path());
        return viewLocation === $location.path();
    };
}]);