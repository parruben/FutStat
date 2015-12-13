var FormacaoCtrl = angular.module('FutStat.FormacaoController', []);

FormacaoCtrl.controller('FormacaoCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.model = {
        Formacoes: {}
    };

    $scope.states = {
        showForm: false
    };

    $scope.novo = {}

    $scope.selecionado = {}

    $http.get('/formacao/IndexJson').success(function (data) {
        $scope.model.Formacoes = data;
    });

    $scope.limpar = function () {
        $scope.novo = {};
        $scope.selecionado = {}
    };

    $scope.showForm = function (data) {
        $scope.states.showForm = data;
        $scope.limpar();
    };

    $scope.addFormacao = function () {
        $http.post('/formacao/Edit', $scope.novo).success(function (data) {
            $scope.model.Formacoes.push(data);
            $scope.showForm(false);
        })
    };

    $scope.getTemplate = function (formacao) {
        if (formacao.CD_FORMACAO === $scope.selecionado.CD_FORMACAO)
            return 'edit';
        else
            return 'display';
    };

    $scope.edit = function (formacao) {
        $scope.showForm(false);
        $scope.selecionado = angular.copy(formacao);
    }

    $scope.editFormacao = function (idx) {
        $http.post('/formacao/Edit', $scope.selecionado).success(function (data) {
            $scope.model.Formacoes[idx] = angular.copy(data);
            $scope.limpar();
        })
    };

    $scope.deleteFormacao = function (formacao, idx) {
        $http.post('/formacao/Delete', formacao).success(function () {
            $scope.model.Formacoes.splice(idx, 1);
            $scope.limpar();
        })
    };
}]);