using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Lync.Model;
using System.Windows;

namespace LyncRobot.util
{
    public class LyncLoginController
    {
        LyncClient _LyncClient;

        public LyncLoginController(LyncClient lyncClient)
        {
            _LyncClient = lyncClient;

            LoginInit();
        }

        private void LoginInit()
        {

            //1) Register for the three Lync client events needed so that application is notified when:
            // * Lync client signs in or out
            _LyncClient.StateChanged += new EventHandler<ClientStateChangedEventArgs>(_LyncClient_StateChanged);
            _LyncClient.SignInDelayed += new EventHandler<SignInDelayedEventArgs>(_LyncClient_SignInDelayed);
            _LyncClient.CredentialRequested += new EventHandler<CredentialRequestedEventArgs>(_LyncClient_CredentialRequested);

            //2-4) Client state of uninitialized means that Lync is configured for UI suppression mode and
            //must be initialized before a user can sign in to Lync
            if (_LyncClient.State == ClientState.Uninitialized)
            {
                _LyncClient.BeginInitialize(
                    (ar) =>
                    {
                        _LyncClient.EndInitialize(ar);
                        //_thisProcessInitializedLync = true;
                    },
                    null);
            }

            //5) If the Lync client is signed out, sign into the Lync client
            else if (_LyncClient.State == ClientState.SignedOut)
            {
                SignUserIn();
            }
            //6) A sign in operation is pending
            else if (_LyncClient.State == ClientState.SigningIn)
            {
            
            }

            else if (_LyncClient.State == ClientState.SignedIn)
            { 
            
            }
        }

        void _LyncClient_CredentialRequested(object sender, CredentialRequestedEventArgs e)
        {
            
        }

        void _LyncClient_SignInDelayed(object sender, SignInDelayedEventArgs e)
        {
            
        }

        void _LyncClient_StateChanged(object sender, ClientStateChangedEventArgs e)
        {
            
        }

        private void SignUserIn()
        {
            try
            {
                _LyncClient.BeginSignIn(
                    "xiao.peng@aspect.com",
                    "xiao.peng@aspect.com",
                    "Win2013@",
                    (ar) =>
                    {
                        try
                        {
                            _LyncClient.EndSignIn(ar);

                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("exception on endsignin: " + exc.Message);
                        }
                    },
                    null);
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("exception on beginsignin: " + ae.Message);
            }
        }
    }
}
