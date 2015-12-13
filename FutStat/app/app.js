var app = angular.module('app'
    , ['ngRoute'
    , 'FutStat.PosicaoController'
    , 'FutStat.FormacaoController'
    , 'FutStat.EstadoController'
    , 'FutStat.CidadeController'
    , 'FutStat.TimeController'
    , 'FutStat.CampeonatoController']);

app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/posicao', {
            templateUrl: 'app/view/posicao/index.html',
            controller: 'PosicaoCtrl'
        }).
        when('/formacao', {
            templateUrl: 'app/view/formacao/index.html',
            controller: 'FormacaoCtrl'
        }).
        when('/estado', {
            templateUrl: 'app/view/estado/index.html',
            controller: 'EstadoCtrl'
        }).
        when('/cidade', {
            templateUrl: 'app/view/cidade/index.html',
            controller: 'CidadeCtrl'
        }).
        when('/time', {
            templateUrl: 'app/view/time/index.html',
            controller: 'TimeCtrl'
        }).
        when('/campeonato', {
            templateUrl: 'app/view/campeonato/index.html',
            controller: 'CampeonatoCtrl'
        }).
        otherwise({
            redirectTo: ''
        });
  }]);