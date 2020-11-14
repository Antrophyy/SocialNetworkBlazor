using Fluxor;
using SocialNetworkBlazor.Client.Store.Messages.Actions;

namespace SocialNetworkBlazor.Client.Store.Messages
{
    public class MessageReducer
    {
        [ReducerMethod]
        public static MessageState GetMessages(MessageState state, GetMessagesAction action)
        {
            return state with
            {
                ClientMessages = state.ClientMessages
            };
        }

        [ReducerMethod]
        public static MessageState GetMessagesSuccess(MessageState state, GetMessagesSuccessAction action)
        {
            return state with
            {
                ClientMessages = action.ClientMessages
            };
        }

        [ReducerMethod]
        public static MessageState MessageFailed(MessageState state, MessageFailedAction action)
        {
            return state with
            {
                ClientMessages = state.ClientMessages
            };
        }

        [ReducerMethod]
        public static MessageState AddMessage(MessageState state, AddMessageAction action)
        {
            return state with
            {
                ClientMessages = state.ClientMessages
            };
        }
    }
}
