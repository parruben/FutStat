var PosicaoController = angular.module('FutStat.PosicaoController', [])

PosicaoController.controller('PosicaoCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.model = {
        Posicoes: {}
    };
    $scope.states = {
        showForm: false
    };

    $scope.novo = {};

    $scope.selecionado = {};

    $http.get('/posicao/IndexJSON').success(function (data) {
        $scope.model.Posicoes = data;
    });

    $scope.limpar = function () {
        $scope.novo = {};
        $scope.selecionado = {};
    };

    $scope.showForm = function (show) {
        $scope.states.showForm = show;
        $scope.limpar();
    };

    $scope.addPosicao = function () {
        $http.post('/posicao/Edit', $scope.novo).success(function (data) {
            $scope.model.Posicoes.push(data);
            $scope.showForm(false);
        })
    };

    $scope.getTemplate = function (posicao) {
        if (posicao.CD_POSICAO === $scope.selecionado.CD_POSICAO)
            return 'edit';
        else
            return 'display';
    };

    $scope.edit = function (posicao) {
        $scope.showForm(false);
        $scope.selecionado = angular.copy(posicao);
    };

    $scope.editPosicao = function (idx) {
        $http.post('/posicao/Edit', $scope.selecionado).success(function (data) {
            $scope.model.Posicoes[idx] = angular.copy($scope.selecionado);
            $scope.limpar();
        })
    };

    $scope.deletePosicao = function (posicao, idx) {
        $http.post('/posicao/Delete', posicao).success(function () {
            $scope.model.Posicoes.splice(idx, 1);
            $scope.limpar();
        })
    };

}]);