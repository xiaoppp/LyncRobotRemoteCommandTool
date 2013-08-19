using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using System.Diagnostics;
using LyncRobotCommand;

namespace LyncRobot.util
{
    public class ResponseConversationController
    {
        private LyncClient lyncClient;
        ParticipantManager participantManager;

        public ResponseConversationController(LyncClient lyncClient) 
        {
            this.lyncClient = lyncClient;

            participantManager = new ParticipantManager();
            participantManager.ParticipantAdded += new Action<string, string>(manager_ParticipantAdded);

            lyncClient.ConversationManager.ConversationAdded += new EventHandler<ConversationManagerEventArgs>(ConversationManager_ConversationAdded);
            lyncClient.ConversationManager.ConversationRemoved += new EventHandler<ConversationManagerEventArgs>(ConversationManager_ConversationRemoved);
        }

        void ConversationManager_ConversationRemoved(object sender, ConversationManagerEventArgs e) { }
        void ConversationManager_ConversationAdded(object sender, ConversationManagerEventArgs e)
        {
            var participants = e.Conversation.Participants;

            e.Conversation.ParticipantAdded += new EventHandler<ParticipantCollectionChangedEventArgs>(Conversation_ParticipantAdded);
            e.Conversation.StateChanged += new EventHandler<ConversationStateChangedEventArgs>(Conversation_StateChanged);
        }

        void Conversation_StateChanged(object sender, ConversationStateChangedEventArgs e)
        {

        }

        void Conversation_ParticipantAdded(Object sender, ParticipantCollectionChangedEventArgs e)
        {
            Conversation conversation = sender as Conversation;
            Participant participant = e.Participant as Participant;

            //for this, not a new conversation....
            if (conversation.Participants.Count > 2)
                participantManager.IsShowWelcome = false;
            else
                participantManager.IsShowWelcome = true;

            // add event handlers for modalities of participants other than self participant:
            if (participant.IsSelf == false)
            {
                var messagecontroller = participantManager.AddParticipant(participant.Contact.Uri, conversation, participant);

                if (conversation.Modalities.ContainsKey(ModalityTypes.InstantMessage))
                {
                    ((InstantMessageModality)participant.Modalities[ModalityTypes.InstantMessage]).InstantMessageReceived += new EventHandler<MessageSentEventArgs>(messagecontroller.ReceiveMessage);
                    ((InstantMessageModality)participant.Modalities[ModalityTypes.InstantMessage]).IsTypingChanged += new EventHandler<IsTypingChangedEventArgs>(messagecontroller.TypeingChanged);
                }
            }
        }

        private void manager_ParticipantAdded(string participantname, string welcome)
        {
            var controller = participantManager.FindMessageController(participantname);
            if (controller != null)
                controller.SendMessage(welcome);
        }
    }
}
