using Fluxor;
using SocialNetworkBlazor.Client.Store.Message.Actions;

namespace SocialNetworkBlazor.Client.Store.Message
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
        public static MessageState SendMessage(MessageState state, SendMessageAction action)
        {
            return state with
            {
                ClientMessages = state.ClientMessages
            };
        }

        [ReducerMethod]
        public static MessageState RecieveMessage(MessageState state, RecieveMessageAction action)
        {
            state.ClientMessages.Insert(0, action.Message);
            return state with
            {
                ClientMessages = state.ClientMessages
            };
        }
    }
}
