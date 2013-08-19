using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyncRobotCommand;
using Microsoft.Lync.Model.Conversation;

namespace LyncRobot.util
{
    //hold all the Participant
    public class ParticipantManager
    {
        // key: participant name, value: command manager
        private Dictionary<string, CommandManager> commandManagerDic;

        private Dictionary<string, MessageController> messageControllerDic ;

        public ParticipantManager()
        {
            commandManagerDic = new Dictionary<string, CommandManager>();
            messageControllerDic = new Dictionary<string, MessageController>();
        }

        public bool IsShowWelcome { get; set; }

        // first string, participant name
        // second string, string welcome
        public event Action<string, string> ParticipantAdded;

        public MessageController AddParticipant(string name, Conversation conversation, Participant participant)
        {
            CommandManager manager = null;
            if (!commandManagerDic.ContainsKey(name))
            {
                manager = new CommandManager(name);
                commandManagerDic.Add(name, manager);
            }
            else
                manager = commandManagerDic[name];

            MessageController messagecontroller = null;
            if (!messageControllerDic.ContainsKey(name))
            {
                messagecontroller = new MessageController(conversation, participant, manager);
                messageControllerDic.Add(name, messagecontroller);

                if (ParticipantAdded != null && this.IsShowWelcome)
                    ParticipantAdded(name, manager.ShowWelcome());
            }
            else
                messagecontroller = messageControllerDic[name];

            return messagecontroller;
        }

        public void RemoveParticipant(string name)
        {
            commandManagerDic.Remove(name);
        }

        public string ExecuteCommand(string participantName, string command)
        {
            var manager = FindCommandManager(participantName);
            return manager.ExecuteCommand(command);
        }

        public MessageController FindMessageController(string name)
        {
            MessageController controller;
            if (!messageControllerDic.TryGetValue(name, out controller))
                return null;
            return controller;
        }

        public CommandManager FindCommandManager(string name)
        {
            CommandManager manager ;
            if (!commandManagerDic.TryGetValue(name, out manager))
                return null;
            return manager;
        }
    }
}
