﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using RockBot.Services;

namespace RockBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>

        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                // Antigo: await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
                // Antigo-novo: await Conversation.SendAsync(activity, () => new Dialogs.DialogStudy());
                // await Conversation.SendAsync(activity, () => new Dialogs.LUISDialog());

                bool bSetStock = false;
                StockLUIS stLuis = await LUISStockClient.ParseUserInput(activity.Text);
                string strRet = string.Empty;
                string strStock = activity.Text;

             
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                StateClient _stateClient = activity.GetStateClient();
                BotData _botData = _stateClient.BotState.GetUserData(activity.ChannelId, activity.Conversation.Id);

                switch (stLuis.intents[0].intent)
                {
                    case "sentimento-ruim":
                        strRet = "I don't have a previous stock to look up!";
                        break;
                    case "como-voce-esta":
                        strRet = "I don't have a previous stock to look up11111!";
                        break;
                    default:
                        strRet = "I don't have a previous stock to look up!222222";
                        break;

                }

                if (bSetStock)
                {
                    _botData.SetProperty<string>("LastStock", stLuis.entities[0].entity);
                    _stateClient.BotState.SetUserData(activity.ChannelId, activity.Conversation.Id, _botData);
                }

                Activity reply = activity.CreateReply(strRet);
                await connector.Conversations.ReplyToActivityAsync(reply);

            }
            else
            {
                HandleSystemMessage(activity);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

    }
}