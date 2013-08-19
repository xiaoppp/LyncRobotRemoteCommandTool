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

namespace LyncRobot
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public event EventHandler LoginClick;

        public string UserID { get; set; }
        public string Password { get; set; }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            UserID = txtUserID.Text ?? string.Empty;
            Password = txtPassword.Text ?? string.Empty;

            if (LoginClick != null)
                LoginClick(sender, e);
        }
    }
}
