var CampeonatoController = angular.module('FutStat.CampeonatoController', []);

CampeonatoController.controller('CampeonatoCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.model = {
        Campeonatos: {}
    };

    $scope.states = {
        showForm: false
    };

    $scope.novo = {};

    $scope.selecionado = {};

    $http.get('/campeonato/IndexJSON').success(function (data) {
        $scope.model.Campeonatos = data;
    });

    $scope.limpar = function () {
        $scope.novo = {};
        $scope.selecionado = {};
    };

    $scope.showForm = function (show) {
        $scope.limpar();
        $scope.states.showForm = show;
    };

    $scope.addCampeonato = function () {
        $http.post('/campeonato/Edit', $scope.novo).success(function (campeonato) {
            $scope.model.Campeonatos.push(campeonato);
            $scope.showForm(false);
        });
    };

    $scope.getTemplate = function (campeonato) {
        if (campeonato.CD_CAMPEONATO === $scope.selecionado.CD_CAMPEONATO)
            return 'edit';
        else
            return 'display';
    };

    $scope.edit = function (campeonato) {
        $scope.showForm();
        $scope.selecionado = angular.copy(campeonato);
    };

    $scope.editCampeonato = function (idx) {
        $http.post('/campeonato/Edit', $scope.selecionado).success(function (campeonato) {
            $scope.model.Campeonatos[idx] = angular.copy(campeonato);
            $scope.showForm(false);
        });
    };

    $scope.deleteCampeonato = function (campeonato, idx) {
        $http.post('/campeonato/Delete', campeonato).success(function () {
            $scope.model.Campeonatos.splice(idx, 1);
            $scope.limpar();
        });
    };
}]);