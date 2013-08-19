using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using System.Xml;
using LyncRobotCommand.Entity;

namespace LyncRobot.util
{
    public class GroupConversation
    {
        //private List<LyncMessage> messageStack = new List<LyncMessage>();

        //void GroupConversationController_InstantMessageReceived(object sender, MessageSentEventArgs e)
        //{
        //    var model = sender as InstantMessageModality;

        //    IDictionary<InstantMessageContentType, string> messageFormatProperty = e.Contents;
        //    var key = messageFormatProperty.Keys;

        //    string content = string.Empty;

        //    if (messageFormatProperty.ContainsKey(InstantMessageContentType.PlainText))
        //        messageFormatProperty.TryGetValue(InstantMessageContentType.PlainText, out content);

        //    else if (messageFormatProperty.ContainsKey(InstantMessageContentType.RichText))
        //    {
        //        if (messageFormatProperty.TryGetValue(InstantMessageContentType.RichText, out content))
        //        {
        //            RTFDomDocument doc = new RTFDomDocument();
        //            doc.LoadRTFText(content);
        //            content = doc.InnerText;
        //        }
        //    }

        //    int choice;
        //    if (int.TryParse(content, out choice))
        //    {
        //        if (!OrderParticipantChoice.ContainsKey(model.Participant))
        //            OrderParticipantChoice.Add(model.Participant, choice);
        //    }

        //    if (OrderParticipantChoice.Count == OrderParticipantList.Count)
        //    {
        //        var output = string.Empty;
        //        foreach (var participant in OrderParticipantChoice.Keys)
        //        {
        //            output += participant.Contact.Uri + ": " + orderParticipantChoice[participant];
        //        }
        //        SendMessage(output);
        //    }
        //}

        //private Dictionary<Participant, int> orderParticipantChoice;
        //public Dictionary<Participant, int> OrderParticipantChoice
        //{
        //    get
        //    {
        //        if (orderParticipantChoice == null)
        //            orderParticipantChoice = new Dictionary<Participant, int>();
        //        return orderParticipantChoice;
        //    }
        //    set { orderParticipantChoice = value; }
        //}

        private LyncClient lyncClient;
        private Conversation conversation;

        public GroupConversation(LyncClient lyncClient)
        {
            this.lyncClient = lyncClient;
        }

        public void StartOrderConversation()
        {
            conversation = lyncClient.ConversationManager.AddConversation();

            foreach (Contact contact in OrderParticipantList)
                conversation.AddParticipant(contact);

            var output = OrderEntity.ShowOrder();
            SendMessage(output.ToString());
        }

        private List<Contact> orderParticipantList;
        public List<Contact> OrderParticipantList
        {
            get
            {
                if (orderParticipantList == null)
                {
                    var paul = lyncClient.ContactManager.GetContactByUri("paul.shao@aspect.com");
                    var andy = lyncClient.ContactManager.GetContactByUri("andy.li@aspect.com");
                    var yolanda = lyncClient.ContactManager.GetContactByUri("yolanda.zhou@aspect.com");
                    var simen = lyncClient.ContactManager.GetContactByUri("simon.shen@aspect.com");
                    var jerry = lyncClient.ContactManager.GetContactByUri("jerry.ma@aspect.com");
                    var jan = lyncClient.ContactManager.GetContactByUri("jan.liu@aspect.com");

                    orderParticipantList = new List<Contact> { paul, andy, yolanda, simen, jerry, jan };
                }
                return orderParticipantList;
            }
        }

        public void SendMessage(string messageToSend)
        {
            try
            {
                IDictionary<InstantMessageContentType, string> messageContent = new Dictionary<InstantMessageContentType, string>();
                messageContent.Add(InstantMessageContentType.PlainText, messageToSend);

                if (((InstantMessageModality)conversation.Modalities[ModalityTypes.InstantMessage]).CanInvoke(ModalityAction.SendInstantMessage))
                {
                    ((InstantMessageModality)conversation.Modalities[ModalityTypes.InstantMessage]).BeginSendMessage(
                        messageContent
                        , SendMessageCallback
                        , messageContent);
                }
            }
            catch
            {

            }
        }

        public AsyncCallback SendMessageCallback { get; set; }
    }
}
