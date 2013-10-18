using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows;

using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Model;

using LyncRobotCommand;

namespace LyncRobot.util
{
    // take response for send / receive message
    public class MessageController
    {
        public Conversation conversation;
        public Participant participant;
        public CommandManager commandManager;

        public MessageController(Conversation conversation, Participant participant, CommandManager commandManager)
        {
            this.conversation = conversation;
            this.participant = participant;
            this.commandManager = commandManager;
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

        /// <summary>
        /// Async callback method invoked by InstantMessageModality instance when SendMessage completes
        /// </summary>
        /// <param name="_asyncOperation">IAsyncResult The operation result</param>
        /// 
        private void SendMessageCallback(IAsyncResult ar)
        {
            ((InstantMessageModality)conversation.Modalities[ModalityTypes.InstantMessage]).BeginSetComposing(false, ComposingCallback, null);
            if (ar.IsCompleted == true)
            {
                try
                {
                    ((InstantMessageModality)conversation.Modalities[ModalityTypes.InstantMessage]).EndSendMessage(ar);
                }
                catch (LyncClientException lce)
                {
                    MessageBox.Show("Lync Client Exception on EndSendMessage " + lce.Message);
                }
            }
        }

        /// <summary>
        /// Callback is invoked when IM Modality state changes upon receipt of message
        /// </summary>
        /// <param name="source">InstantMessageModality Modality </param>
        /// <param name="data">SendMessageEventArgs The new message.</param>
        public void ReceiveMessage(Object sender, MessageSentEventArgs e)
        {
            IDictionary<InstantMessageContentType, string> messageFormatProperty = e.Contents;
            var key = messageFormatProperty.Keys;
            
            string content = string.Empty;
            
            if (messageFormatProperty.ContainsKey(InstantMessageContentType.PlainText))
                messageFormatProperty.TryGetValue(InstantMessageContentType.PlainText, out content);

            else if (messageFormatProperty.ContainsKey(InstantMessageContentType.RichText))
            {
                if (messageFormatProperty.TryGetValue(InstantMessageContentType.RichText, out content))
                {
                    //RTFDomDocument doc = new RTFDomDocument();
                    //doc.LoadRTFText(content);
                    //content = doc.InnerText;
                    System.Windows.Forms.RichTextBox rtb = new System.Windows.Forms.RichTextBox();
                    rtb.Rtf = content;
                    content = rtb.Text;
                }
            }

            var replymessage= this.commandManager.ExecuteCommand(content);
            SendMessage(replymessage);

            Debug.WriteLine(content);
            Debug.WriteLine(replymessage);
        }

        public void TypeingChanged(Object source, IsTypingChangedEventArgs data)
        { 
        
        }

        public AsyncCallback ComposingCallback { get; set; }
    }

    public class LyncMessage
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public MessageOrientation Orientation { get; set; }
        //public LyncMessageType MessageType { get; set; }
    }

    public class TEventArgs<T> : EventArgs 
    {
        private T t;
        public T Args
        {
            get { return t; }
            set { t = value; }
        }

        public TEventArgs(T t)
        {
            this.t = t;
        }
    }

    //public enum LyncMessageType
    //{ 
    //    Order,
    //    Setup
    //}

    public enum MessageOrientation
    { 
        In,
        Out
    }
}
