using Fairhaven;
using Fairhaven.Database;
using IrcD;
using IrcD.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestUI
{
    public partial class TestUI : Form
    {
        private bool _isRunning = false;
        private Thread _thread = null;
        private IrcDaemon _daemon = null;
        private Context _db = null;

        public TestUI()
        {
            InitializeComponent();

            _db = new Context();
        }

        private void bStartServer_Click(object sender, EventArgs e)
        {
            if(!_isRunning)
            {
                var settings = new Settings();
                _daemon = new IrcDaemon(settings.GetIrcMode());
                settings.setDaemon(_daemon);
                settings.LoadSettings();
                _daemon.ServerRehash += ServerRehash;
                _thread = new Thread(_daemon.Start);
                _thread.IsBackground = false;
                _thread.Name = "serverThread-1";
                _isRunning = true;
                bStartServer.Text = "Stop Server";
                _thread.Start();
            }
            else
            {
                Close();
            }
        }

        static void ServerRehash(object sender, RehashEventArgs e)
        {
            var settings = new Settings();
            settings.setDaemon(e.IrcDaemon);
            settings.LoadSettings();
        }

        private void TestUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
            Application.Exit();
        }

        private void Exit()
        {
            if(_daemon != null) _daemon.Stop(false);
            if(_thread != null) if(_thread.IsAlive) _thread.Suspend();
            bStartServer.Text = "Start Server";
            _isRunning = false;
        }

        private Bot _bot;
        private void bStartBot_Click(object sender, EventArgs e)
        {
            var info = new BotInfo();
            info.Nick = "Fairhaven";
            //info.Server = "irc.paraphysics.net";
            info.Server = "127.0.0.1";
            info.Port = 6667;

            _bot = new Bot(info);
        }

        private void bStopBot_Click(object sender, EventArgs e)
        {
            if(_bot != null && _bot.IsConnected())
            {
                _bot.Disconnect("quit command received");
            }
            _bot = null;
        }

        private void bBotCommand_Click(object sender, EventArgs e)
        {
            if(_bot != null)
                _bot.Command(tBotCommand.Text.Trim()); ;
        }

        private void tBotCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if(_bot != null && e.KeyCode == Keys.Enter)
            {
                _bot.Command(tBotCommand.Text); ;
            }
        }

        private void tBotSay_KeyDown(object sender, KeyEventArgs e)
        {
            if(_bot != null && e.KeyCode == Keys.Enter)
            {
                _bot.Say(tBotJoin.Text, tBotSay.Text); ;
            }
        }

        private void tBotAction_KeyDown(object sender, KeyEventArgs e)
        {
            if(_bot != null && e.KeyCode == Keys.Enter)
            {
                _bot.Action(tBotJoin.Text, tBotAction.Text); ;
            }
        }

        private void tBotJoin_KeyDown(object sender, KeyEventArgs e)
        {
            if(_bot != null && e.KeyCode == Keys.Enter)
            {
                _bot.Command("JOIN " + tBotJoin.Text); ;
            }
        }

        private void bWeapon_Click(object sender, EventArgs e)
        {
            tWeapon.Text = Items.WeaponRandom(_db).ToString();
        }
    }
}