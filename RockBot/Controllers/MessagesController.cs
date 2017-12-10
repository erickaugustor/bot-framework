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

                StockLUIS stLuis = await LUISStockClient.ParseUserInput(activity.Text);

              
                


                 if(stLuis.entities[0].entity == "como-voce-esta") 
                 {
                        //case "sentimento-bom":
                            await Conversation.SendAsync(activity, () => new Dialogs.LUISDialog());
                    //  break;
                    //  case "sentimento-ruim":
                    ///  await Conversation.SendAsync(activity, () => new Dialogs.LUISDialog());
                    ////   break;
                    // case "como-voce-esta":
                    //  await Conversation.SendAsync(activity, () => new Dialogs.LUISDialog());
                    // break;
                    //default:
                    // break;
                }
                else if(stLuis.entities[0].entity == "sentimento-ruim")
                {
                    await Conversation.SendAsync(activity, () => new Dialogs.LUISDialog());
                }
                else if (stLuis.intents[0].intent == "sentimento-ruim")
                {
                    await Conversation.SendAsync(activity, () => new Dialogs.LUISDialog());
                }
                else if (stLuis.intents[0].intent == "como-voce-esta")
                {
                    await Conversation.SendAsync(activity, () => new Dialogs.LUISDialog());
                }
                else
                {
                    await Conversation.SendAsync(activity, () => new Dialogs.DialogStudy());
                }

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