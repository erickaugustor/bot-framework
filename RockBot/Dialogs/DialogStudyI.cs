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

            context.Wait(MsgRecebidaEscolha);
            //texto do usuario
        }

        //intercepta a msg do usuario

        private async Task MsgRecebidaEscolha(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var msg = await argument;
            //pega argumento do usuario

            //ifs do usuario
            if (msg.Text.ToLower().Equals("random", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Informe um número inteiro mínimo:");
                context.Wait(MsgRecebidaRand1);
            }
            else if (msg.Text.ToLower().Equals("idade", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Informe sua data de nascimento:");
                context.Wait(MsgRecebidaData);
            }
            else if (msg.Text.ToLower().Equals("elogio", StringComparison.InvariantCultureIgnoreCase))
            {
                Random rand = new Random();
                var pos = rand.Next(1, 4);
                var listaElogios = new string[] { "O rodrigo é um lixo? Sim!", "O rodrigo é muito lixo", "Rodrigo lixão" };
                await context.PostAsync(listaElogios[pos]);
            }
            else
            {
                await context.PostAsync("Desculpe, não entendi!");
                //volta para o inicio
                context.Wait(MsgRecebidaComeco);
            }

        }



        private async Task MsgRecebidaRand1(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var resp = await argument;

            int num = 0;
            //se não conveter para um número válido
            if (!int.TryParse(resp.Text, out num))
            {
                await context.PostAsync("Não entedi sua resposta. Informe outro valor");
                context.Wait(MsgRecebidaRand1);
                return;
            }

            //seta na variavel global
            this.num1 = num;

            await context.PostAsync("Informe um número inteiro máximo: ");
            context.Wait(MsgRecebidaRand2);
        }

        private async Task MsgRecebidaRand2(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var resp = await argument;

            int num = 0;
            //se não conveter para um número válido
            if (!int.TryParse(resp.Text, out num))
            {
                await context.PostAsync("Não entedi sua resposta. Informe outro valor");
                context.Wait(MsgRecebidaRand1);
                return;
            }

            //seta na variavel global
            this.num2 = num;

            Random rand = new Random();
            var info = rand.Next(num1, num2);

            await context.PostAsync("O seu´número random é " + info);
            context.Wait(MsgRecebidaComeco);
        }



    }
}