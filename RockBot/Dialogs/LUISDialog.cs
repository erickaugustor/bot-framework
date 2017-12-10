using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RockBot.Dialogs
{
    [Serializable]

    public class LUISDialog : IDialog<object>
    {

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
            var msg = await argument as Activity;

            await context.PostAsync($"Olar {msg.Text}! \n Eu sou Mr. Fofis! :)");
            await context.PostAsync($"Como você está?");
        }


    }
}