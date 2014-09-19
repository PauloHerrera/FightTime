var lutadores = [];

var mensagensDeInicio = ["Bem Vindos a mais uma luta. Fique atento, o duelo entre esses dois mitos promete!",
    "Olá, vai começar mais uma luta sensacional!", "Não tire seus olhos da tela, essa luta promete fortes emoções",
    "Façam suas apostas, porque mais uma grande luta está prestes a começar!", "Quem será o vencedor dessa luta? Vamos descobrir a partir de agora!"];

var controleDeLuta;

$(document).ready(function () {
    $("#simulaLuta").click(function () {
        $("#iniciaTurno").removeAttr("disabled");

        $("#mensagensDaLuta").html("");
        
        //TODO: Verificar se os lutadores existem
        controleDeLuta = new ControleDeLuta(lutadores, document.getElementById("mensagensDaLuta"));
    });
    
    $("#iniciaTurno").click(function () {
        $("#iniciaTurno").attr("disabled", "disabled");
        $("#mensagensDaLuta").html("");
        
        controleDeLuta.acao();
        
        if (controleDeLuta.FimDaLuta == false) {
            $("#iniciaTurno").removeAttr("disabled");
        }
        
    });
});


function AddLutador(id, nome, apelido, saude, velocidade, agilidade, forca) {
    lutadores.push(new Lutador(id, nome, apelido, saude, velocidade, agilidade, forca));
}

//Controle Da Luta
ControleDeLuta = function (lutadores, mensagemContainer) {
    this.Lutadores = lutadores;
    
    this.EstaAtacando = 0;
    this.EstaSendoAtacado = 1;
    this.Debug = true;
    this.FimDaLuta = false;
    this.MensagemContainer = mensagemContainer;

    this.escreveMensagem(mensagensDeInicio[Math.floor((Math.random() * mensagensDeInicio.length -1) + 1)]);
};

ControleDeLuta.prototype = {
    acao: function () {

        this.verificaQuemAtacaPrimeiro();
        
        this.realizaAtaque();
        
        this.trocaAtacante();
        
        this.realizaAtaque();
       
    },
    verificaQuemAtacaPrimeiro: function () {
        //Cálculo da velocidade que defina quem ataque primeiro
        var velocidade1 = parseInt(this.Lutadores[0].Velocidade) + Math.floor((Math.random() * 5) + 1);
        var velocidade2 = parseInt(this.Lutadores[1].Velocidade) + Math.floor((Math.random() * 5) + 1);
        
        this.EstaAtacando = velocidade1 == velocidade2 ? (Math.floor((Math.random() * 2) + 1) - 1) :
                        (velocidade1 > velocidade2) ? 0 : 1;

        this.EstaSendoAtacado = this.EstaAtacando == 0 ? 1 : 0;
        
        this.escreveMensagem(this.Lutadores[this.EstaAtacando].Nome + " vai atacar primeiro.");
    },
    trocaAtacante: function () {
        this.EstaAtacando = (this.EstaAtacando == 1) ? 0 : 1;
        this.EstaSendoAtacado = this.EstaAtacando == 0 ? 1 : 0;
        this.escreveMensagem("Agora é a vez de " + this.Lutadores[this.EstaAtacando].Nome + " realizar o seu ataque.");
    },
    realizaAtaque: function () {
        if (this.FimDaLuta) return;
        
        var resultadoAtaque = this.tentativaDeEsquiva();
        
        if (resultadoAtaque == "esquiva") {
            this.escreveMensagem(this.Lutadores[this.EstaSendoAtacado].Nome + " esquivou o ataque!");
            return;
        }

        this.calculaDano(resultadoAtaque);
    },
    calculaDano: function (resultadoAtaque) {
        var dano = this.Lutadores[this.EstaAtacando].Forca;
        
        if (resultadoAtaque == "acertou") {
            this.escreveMensagem("Ele acertou o golpe em cheio! Dano: " + dano);
        } else {
            dano = dano - 1;
            this.escreveMensagem("Ele acertou esse golpe de raspão! Dano: " + dano);
        }

        this.Lutadores[this.EstaSendoAtacado].Saude = this.Lutadores[this.EstaSendoAtacado].Saude - dano;
        
        if (this.Lutadores[this.EstaSendoAtacado].Saude <= 0) {
            $("#saude_" + this.Lutadores[this.EstaSendoAtacado].Id).html("0");
            $("#resultado_derrota_" + this.Lutadores[this.EstaSendoAtacado].Id).show();
            $("#resultado_vitoria_" + this.Lutadores[this.EstaAtacando].Id).show();
            return;
        }
        
        $("#saude_" + this.Lutadores[this.EstaSendoAtacado].Id).html(this.Lutadores[this.EstaSendoAtacado].Saude);
        
        if (this.Lutadores[this.EstaSendoAtacado].Saude <= 10) {
            $("#saude_container_" + this.Lutadores[this.EstaSendoAtacado].Id).addClass("text_red");
        }
    },
    tentativaDeEsquiva: function () {
        var fatorAgilidade1 = parseInt(this.Lutadores[this.EstaAtacando].Agilidade) + Math.floor((Math.random() * 5) + 1);
        var fatorAgilidade2 = parseInt(this.Lutadores[this.EstaSendoAtacado].Agilidade) + Math.floor((Math.random() * 5) + 1);

        return fatorAgilidade2 > fatorAgilidade1 ? "esquiva" : fatorAgilidade2 < fatorAgilidade1 ? "acertou" : "danominimo";
    },
    escreveMensagem: function(texto) {
        var ul = this.MensagemContainer;
        var li = document.createElement("li");
        li.appendChild(document.createTextNode(texto));
        ul.appendChild(li);
    }
};

//LUTADORES
Lutador = function (id, nome, apelido, saude, velocidade, agilidade, forca) {
    this.Id = id;
    this.Nome = nome;
    this.Apelido = apelido;
    this.Saude = saude;
    this.Velocidade = velocidade;
    this.Agilidade = agilidade;
    this.Forca = forca;
};

Lutador.prototype = {
    exibeForca: function() {
        return this.Nome + " " + this.Saude;
    }
};