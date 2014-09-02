using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Fairhaven.Database; 

namespace Fairhaven
{
    public class BotInfo
    {
        public BotInfo()
        {
            Server = "irc.zievo.com";
            Port = 6667;
        }

        public string Server { get; set; }
        public int Port { get; set; }
        public string Nick { get; set; }
        public string Channel { get; set; }
    }

    public class Bot
    {
        private TcpClient _connection;
        private NetworkStream _stream;
        private StreamReader _reader;
        private StreamWriter _writer;
        private BotInfo _info;
        private Context _db = new Context();

        private bool _isReading = true;
        private Thread _readThread;

        public Bot(BotInfo info)
        {
            _info = info;
            Connect();
        }

        #region Server Connection
        public void Connect()
        {
            try
            {
                _connection = new TcpClient(_info.Server, _info.Port);
                _stream = _connection.GetStream();
                _reader = new StreamReader(_stream);
                _writer = new StreamWriter(_stream) { NewLine = "\r\n", AutoFlush = true };

                //todo: there should be one thread for all bots
                //      controlled by the server.
                _readThread = new Thread(new ThreadStart(ReadLoop));
                _readThread.Start();

                Handshake();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ex (Bot.Connect): " + ex.Message);
            }
        }

        public bool IsConnected()
        {
            return _connection == null ? true : _connection.Connected;
        }

        public void Disconnect(string quitMessage)
        {
            if(quitMessage == null) quitMessage = string.Empty;

            try
            {
                if(IsConnected())
                {
                    Command("QUIT " + quitMessage);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ex (Bot.Disconnect): " + ex.Message);
            }
            finally
            {
                _isReading = false;
                if(_reader != null) _reader.Close();
                if(_writer != null) _writer.Close();
                if(_connection != null && _connection.Connected) _connection.Close();
            }
        }
        #endregion

        public void Command(string command)
        {
            Console.WriteLine("BotTx(" + _info.Nick + "):" + command);
            _writer.WriteLine(command);
        }

        public void Say(string channel, string text)
        {
            if(string.IsNullOrEmpty(channel)) return;
            if(string.IsNullOrEmpty(text)) return;
            if(!channel.StartsWith("#")) channel = "#" + channel;

            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var line in lines)
            {
                Command(String.Format("PRIVMSG {0} :{1}", channel, line));
            }
        }

        public void Action(string channel, string text)
        {
            if(string.IsNullOrEmpty(channel)) return;
            if(string.IsNullOrEmpty(text)) return;

            if(!channel.StartsWith("#")) channel = "#" + channel;
            // the below string has two [SOH] characters \01, as follows:
            // PRIVMSG {0} :[SOH]ACTION {1}[SOH]
            Command(String.Format("PRIVMSG {0} :ACTION {1}", channel, text));
        }

        private void ReadLoop()
        {
            string rx = string.Empty;
            while(_isReading)
            {
                while((rx = _reader.ReadLine()) != null) 
                {
                    string[] rxTokens = rx.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine("BotRx(" + _info.Nick + "):" + rx);

                    if(rxTokens.Length >= 4) 
                    {
                        string command = rxTokens[3].ToLower();

                        switch(command)
                        {
                            case ":!randomweapon":
                                Say(rxTokens[2], Items.WeaponRandom(_db).ToString());
                                break;
                        }
                    }

                    if(rxTokens[0].ToUpper() == "PING") Command("PONG " + rxTokens[1]);
                    else
                    {
#if DEBUG
                        //System.Diagnostics.Debugger.Break();
#endif
                    }
                }
                Thread.Sleep(5);
            }
        }

        private void Handshake()
        {
            Command("NICK " + _info.Nick);
            Command("USER " + _info.Nick + " 8 * :Fairhaven resident");
        }
    }
}
