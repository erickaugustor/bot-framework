using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace RockBot.FormFlow
{
    [Serializable]
    //tudo é serializable

    public class Questionario
    {
        [Prompt("Como você se chama?")]
        public string Nome { get; set; }

        [Prompt("Qual seu e-mail?")]
        public string Email { get; set; }

        [Prompt("Qual seu telefone?")]
        public string Telefone { get; set; }

        //[Prompt("É criança?")]
        public bool EhCrianca { get; set; }

        public enum Dados
        {
            Estudante, Programador, Analista, Suporte
        }

        public static IForm<Questionario> MontarFormulario()
        {
            return new FormBuilder<Questionario>()
                .Field("ConheciaCanal")
                .Field("ProfissaoAtual")
                .AddRemainingFields()
                .Build();
        }

    }
}