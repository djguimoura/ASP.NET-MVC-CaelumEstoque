var tempoInicial = $("#tempo-digitacao").text();
var campo = $(".campo-digitacao");
/*BOA PRÁTICA DE PROGRAMAÇÃO: DEIXAR CADA CÓDIGO RESPONSÁVEL POR ALGO SEPARADOS POR FUNÇÕES*/
/*BOA PRÁTICA DE PROGRAMAÇÃO: ISOLAR TODAS AS VARIAVEIS GERAIS NO INICIO DO CÓDIGO*/

//quando estiver carregado todos os elementos da página, chamamos as funções através do $(document).ready(function()
$(function(){//somente o $ é um atalho para (document).ready
  atualizaTamanhoFrase();
  inicializaContadores();
  inicializaCronometro();
  inicializaMarcadores();

  $("#botao-reiniciar").click (reiniciaJogo); //.click é um atalho do .on("click")
  atualizaPlacar();

  $("#usuarios").selectize({
    create: true,
    sortField: 'text'
  })

  $(".tooltip").tooltipster({
    trigger: "custom"
  });

})

function atualizaTempoInicial(tempo){
    tempoInicial = tempo;
    $("#tempo-digitacao").text(tempo);
}


function atualizaTamanhoFrase(){
  //função $ mesmo que "jQuery", é a função jQuery para selecionar a tag
  var frase = $(".frase").text(); //.text para acessar o conteudo da tag
  //função split para quebrar a frase com espaços, para contar quantas palavras tem
  var numeroPalavras = frase.split(" ").length; //cria um array
  var tamanhoFrase= $("#tamanho-frase");
  tamanhoFrase.text(numeroPalavras);
}

function inicializaContadores(){
  //quando vc clicar irá receber a função, 'on' para ativar o evento
  campo.on("input", function(){//evento input para o campo pegar o que for digitado, captura do teclado
      var conteudo = campo.val();//campo.val para pegar o valor do conteudo 'input'
      //S+ serve para buscar qqr espaço ou quebra de linha do conteudo
      var qtdPalavras = conteudo.split(/\S+/).length - 1;//o -1 serve para subtrair um elemento do array ou seja os espaços para contar corretamente somente até o tamanho da lista
      $("#contador-palavras").text(qtdPalavras);//altera o contador de palavras no html
      var qtdCaracteres= conteudo.length;
      $("#contador-caracteres").text(qtdCaracteres);//altera o contador de caracteres no html
  });
}

//função para iniciar o contador de tempo do jogo
function inicializaCronometro(){
  //campo.on para ativar o evento dentro da area de digitação
  campo.one("focus", function(){//one para ativar somente a primeira vez do evento e focus serve para ativar qualquer entrada do usuario na area especificada

      $("#botao-reiniciar").attr("disabled", true);

      var tempoRestante = $("#tempo-digitacao").text();
      var cronometroId = setInterval(function(){//setInterval serve para chamar eventos de tempo ou contagem
        tempoRestante --;//irá subtrair de 1 em 1 segundo
        //para alterar o valor do campo de tempo restante (segundos)
        $("#tempo-digitacao").text(tempoRestante);
        if (tempoRestante < 1) {
          clearInterval(cronometroId);//para fazer o contador de tempo parar, pega o id do setInterval
          finalizaJogo();//chamando a função finalizaJogo
        }
      },1000);//qtd de milisegundos
  });
}

//função para finalizar jogo, onde estão: desabilita a text area, reabilita o botão reiniciar e insere o placar na tabela
function finalizaJogo(){
  campo.attr("disabled", true);
  campo.addClass("campo-desativado"); //para alterar a estilização do campo text area
  inserePlacar();
  $("#botao-reiniciar").attr("disabled", false);
}

//função para pintar a borda da caixa de texto para fazer verificação de certo ou errado do texto digitado
function inicializaMarcadores(){
  campo.on("input", function(){
    var frase = $(".frase").text();
    var digitado = campo.val();
    var compararavel = frase.substr(0, digitado.length);
    if(digitado == compararavel){
      campo.addClass("borda-verde");
      campo.removeClass("borda-vermelha");
    }else {
      campo.addClass("borda-vermelha");
      campo.removeClass("borda-verde");
    }
  });
}

//função para reiniciar o jogo ao clicar no botão de reiniciar
function reiniciaJogo(){
  campo.attr("disabled", false);
  campo.val("");//limpa o campo de digitação
  $("#contador-caracteres").text("0"); //limpa o contador
  $("#contador-palavras").text("0"); //limpa o contador
  $("#tempo-digitacao").text(tempoInicial); //reseta o contador
  inicializaCronometro(); //chamando a função cronometro para ativar o novo jogo
  campo.removeClass("campo-desativado");
  campo.removeClass("borda-vermelha");
  campo.removeClass("borda-verde");
}
