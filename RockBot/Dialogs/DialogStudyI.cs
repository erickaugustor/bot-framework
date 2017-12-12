using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RockBot.Dialogs
{
    public class DialogStudyI : IDialog<object>
    {

        protected int num1 { get; set; }

        protected int num2 { get; set; }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MsgRecebidaComeco);
        }

        private async Task MsgRecebidaComeco(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            //Metodo com COntext e um Awaitable que referência o IMessageActivity
            //context.postAsync, o robo envia algo para o usuário!

            await context.PostAsync($"Ola!\n Voce quer um número RANDOM? Sua IDADE? Um ELOGIO?");

            context.Wait(MsgRecebidaComeco);
        }
    }
}