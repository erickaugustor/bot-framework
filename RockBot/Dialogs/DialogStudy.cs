using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace RockBot.Dialogs { 

    [Serializable]

    public class DialogStudy : IDialog<object>
    {
        protected int num1 { get; set; }

        protected int num2 { get; set; }


        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MsgComeco);

        }

        private async Task MsgComeco(IDialogContext context, IAwaitable<object> result)
        {

            await context.PostAsync($"Ola! \n Qual o seu nome?");

            context.Wait(MsgRespNome);
        }

        private async Task MsgRespNome(IDialogContext context, IAwaitable<object> argument)
        {
            var msg = await argument;

            //transformar await em string

            var olar = "Olar (colocar nome aqui)! \n Eu sou Mr. Fofis! :)";
            await context.PostAsync(olar);

        }


    }
}