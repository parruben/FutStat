var EstadoController = angular.module('FutStat.EstadoController', []);

EstadoController.controller('EstadoCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.model = {
        Estados: {}
    };

    $scope.states = {
        showForm: false
    };

    $scope.novo = {};

    $scope.selecionado = {};

    $http.get('/estado/IndexJSON').success(function (data) {
        $scope.model.Estados = data;
    });

    $scope.limpar = function () {
        $scope.novo = {};
        $scope.selecionado = {};
    };

    $scope.showForm = function (show) {
        $scope.states.showForm = show;
        $scope.limpar();
    };

    $scope.addEstado = function () {
        $http.post('/estado/Edit', $scope.novo).success(function (data) {
            $scope.model.Estados.push(data);
            $scope.showForm(false);
        })
    };

    $scope.getTemplate = function (estado) {
        if (estado.CD_ESTADO === $scope.selecionado.CD_ESTADO)
            return 'edit';
        else
            return 'display';
    }

    $scope.edit = function (estado) {
        $scope.showForm(false);
        $scope.selecionado = angular.copy(estado);
    }

    $scope.editEstado = function (idx) {
        $http.post('/estado/Edit', $scope.selecionado).success(function (data) {
            $scope.model.Estados[idx] = angular.copy(data);
            $scope.limpar();
        });
    }

    $scope.deleteEstado = function (estado, idx) {
        $http.post('/estado/Delete', estado).success(function () {
            $scope.model.Estados.splice(idx, 1);
            $scope.limpar();
        });
    }
}])