using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows;

namespace ChatClientWpf
{
    public class Client
    {
        Thread tcpThread;      // Receiver
        bool _conn = false;    // Is connected/connecting?
		bool _logged = false; 
        string _user;          // Username
		string _pass;

        public event IMReceivedEventHandler MessageReceived;

        public string Server { get { return "localhost"; } }  // Address of server. In this case - local IP address.
        public int Port { get { return 2000; } }
	
        public string UserName { get { return _user; } }
		public string Password { get { return _pass; } }
		public bool IsLoggedIn { get { return _logged; } }

        virtual protected void OnMessageReceived(IMReceivedEventArgs e)
        {
            if (MessageReceived != null)
                MessageReceived(this, e);
        }
        // Start connection thread and login or register.
        public void connect(string user, string password)
        {
            if (!_conn)
            {
                _conn = true;
                _user = user;
				_pass = password;
                tcpThread = new Thread(new ThreadStart(SetupConn));
                tcpThread.Start();
            }
        }
		public void Login(string user, string password)
		{
			connect(user, password);
		}
        public void Disconnect()
        {
            if (_conn)
                CloseConn();
        }
        public void SendMessage(string to, string msg)
        {
            if (_conn)
            {
                bw.Write(to);
                bw.Write(msg);
                bw.Flush();
            }
        }

        TcpClient client;
        NetworkStream netStream;
        BinaryReader br;
        BinaryWriter bw;

        void SetupConn()  // Setup connection and login
        {
            client = new TcpClient(Server, Port);  // Connect to the server.
            netStream = client.GetStream();
            // Now we have encrypted connection.

            br = new BinaryReader(netStream, Encoding.UTF8);
            bw = new BinaryWriter(netStream, Encoding.UTF8);

            bw.Write(UserName);
			bw.Write(Password);
            bw.Flush();

            Receiver(); // Time for listening for incoming messages.
            if (_conn)
                CloseConn();
        }
        void CloseConn() // Close connection.
        {
            br.Close();
            bw.Close();
            netStream.Close();
            client.Close();
            _conn = false;
        }
        void Receiver()  // Receive all incoming packets.
        {
			_logged = true;
            try
            {
                while (client.Connected)  // While we are connected.
                {
                    string from = br.ReadString();
                    string msg = br.ReadString();
                    Console.WriteLine("[{0}] ({1} -> {2}) Message sent!", DateTime.Now, from, msg);
                    OnMessageReceived(new IMReceivedEventArgs(from, msg));
                }
            }
            catch (IOException) { }

			_logged = false;
        }

    }


    public class IMReceivedEventArgs : EventArgs
    {
        string user;
        string msg;

        public IMReceivedEventArgs(string user, string msg)
        {
            this.user = user;
            this.msg = msg;
        }

        public string From
        {
            get { return user; }
        }
        public string Message
        {
            get { return msg; }
        }
    }

    public delegate void IMReceivedEventHandler(object sender, IMReceivedEventArgs e);
}
