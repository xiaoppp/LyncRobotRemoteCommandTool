using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Lync.Model;
using LyncRobot.util;

namespace LyncRobot
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainView_Loaded);
        }

        LyncClient _LyncClient;
        LyncLoginController loginController;
        ResponseConversationController conversationController;

        void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            _LyncClient = LyncClient.GetClient();
            //_LyncClient.InSuppressedMode

            loginController = new LyncLoginController(_LyncClient);
            conversationController = new ResponseConversationController(_LyncClient);
            
        }

        private void btnSend_click(object sender, RoutedEventArgs e)
        {
            GroupConversation conversation = new GroupConversation(_LyncClient);
            conversation.StartOrderConversation();
        }
    }
}
