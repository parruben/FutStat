var CidadeController = angular.module('FutStat.CidadeController', []);

CidadeController.controller('CidadeCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.model = {
        Cidades: {},
        Estados: {}
    };

    $scope.states = {
        ShowForm: false
    };

    $scope.novo = {};

    $scope.selecionado = {};

    $http.get('/cidade/IndexJSON').success(function (data) {
        $scope.model.Cidades = data;
    });

    $http.get('/estado/IndexJSON').success(function (data) {
        $scope.model.Estados = data;
    });

    $scope.limpar = function () {
        $scope.novo = {};
        $scope.selecionado = {};
    };

    $scope.showForm = function (show) {
        $scope.limpar();
        $scope.states.ShowForm = show;
    };

    $scope.addCidade = function () {
        $http.post('/cidade/Edit', $scope.novo).success(function (data) {
            $scope.model.Cidades.push(data);
            $scope.showForm(false);
        });
    };

    $scope.getTemplate = function (cidade) {
        if (cidade.CD_CIDADE === $scope.selecionado.CD_CIDADE)
            return 'edit';
        else
            return 'display';
    };

    $scope.edit = function (cidade) {
        $scope.showForm(false);
        $scope.selecionado = angular.copy(cidade);
    };

    $scope.editCidade = function (idx) {
        $scope.selecionado.estado.CD_ESTADO = $scope.selecionado.CD_ESTADO;
        $http.post('/cidade/Edit', $scope.selecionado).success(function (cidade) {
            $scope.model.Cidades[idx] = angular.copy(cidade);
            $scope.showForm(false);
        });
    };

    $scope.deleteCidade = function (cidade, idx){
        $http.post('/cidade/Delete', cidade).success(function () {
            $scope.model.Cidades.splice(idx, 1);
            $scope.limpar();
        });
    };
}]);