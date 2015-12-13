var TimeController = angular.module('FutStat.TimeController', []);

TimeController.controller('TimeCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.model = {
        Times: {},
        Cidades: {}
    };

    $scope.states = {
        ShowForm: false
    };

    $scope.novo = {};

    $scope.selecionado = {};

    $http.get('/time/IndexJSON').success(function (data) {
        $scope.model.Times = data;
    });

    $http.get('/cidade/IndexJSON').success(function (data) {
        $scope.model.Cidades = data;
    });

    $scope.limpar = function () {
        $scope.novo = {};
        $scope.selecionado = {};
    };

    $scope.showForm = function (show) {
        $scope.limpar();
        $scope.states.ShowForm = show;
    };

    $scope.addTime = function () {
        $http.post('/time/Edit', $scope.novo).success(function (data) {
            $scope.model.Times.push(data);
            $scope.showForm(false);
        });
    };

    $scope.getTemplate = function (time) {
        if (time.CD_TIME == $scope.selecionado.CD_TIME)
            return 'edit';
        else
            return 'display';
    };

    $scope.edit = function (time) {
        $scope.showForm(false);
        $scope.selecionado = angular.copy(time);
    };

    $scope.editTime = function (idx) {
        $scope.selecionado.cidade.CD_CIDADE = $scope.selecionado.CD_CIDADE;
        $http.post('/time/Edit', $scope.selecionado).success(function (time) {
            $scope.model.Times[idx] = angular.copy(time);
            $scope.showForm(false);
        });
    };

    $scope.deleteTime = function (time, idx) {
        $http.post('/time/Delete', time).success(function () {
            $scope.model.Times.splice(idx, 1);
            $scope.limpar();
        });
    };

    $scope.returnDate = function (time, data) {
        var date = new Date(parseInt(data.substr(6)));
        time.DT_FUNDACAO = angular.copy(date);
        time.DT_FUNDACAO_VW = $scope.formataData(date);
    };

    $scope.formataData = function dataAtualFormatada(data) {
        var dia = data.getDate();
        if (dia.toString().length == 1)
            dia = "0" + dia;
        var mes = data.getMonth() + 1;
        if (mes.toString().length == 1)
            mes = "0" + mes;
        var ano = data.getFullYear();
        return dia + "/" + mes + "/" + ano;
    }
}]);