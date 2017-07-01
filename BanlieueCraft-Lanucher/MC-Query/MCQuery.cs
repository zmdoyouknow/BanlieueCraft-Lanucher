using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace MCQuery
{
    public class McQuery : ServerQuery
    {
        private bool _success = false;
        private ServerInfo _info;
        private int _challenge = 0;
        private int _sid = 0;
        private int _ping = 0;

        private Socket _sock;

        public bool Success () { return _success; }
        public ServerInfo Info () { return _info; }

        public void Connect (string host, int port = 25565, double timeout = 2.5)
        {
            try
            {
                _sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                if (_sock == null) return;

                _sock.Connect(host, port);
                _sock.ReceiveTimeout = (int)(timeout * 1000);
                _sock.SendTimeout = (int)(timeout * 1000);

                this.GetChallenge();
            }
            catch
            {
                _success = false;
            }
        }

        internal void GetChallenge ()
        {
            _sid = new Random().Next() & 0xCF;
            Packet data = this.GrabData(Packets.Challenge(this._sid));
            if (data.Read<byte>() == (byte)0x09 && data.Read<int>() == this._sid)
            {
                int.TryParse(data.Read<String>(), out this._challenge);
                byte[] iabi = BitConverter.GetBytes(this._challenge);
                Array.Reverse(iabi);
                this._challenge = BitConverter.ToInt32(iabi, 0);

                _ping = Environment.TickCount;

                this.GetInfo();
            }
            else GetChallenge();
        }

        internal void GetInfo ()
        {
            Packet data = this.GrabData(Packets.QueryData(this._sid, this._challenge));

            if (data == null)
            {
                this.GetChallenge();
                return;
            }

            if (data.Read<byte>() == (byte)0x00 && data.Read<int>() == this._sid)
            {
                _info = new ServerInfo();

                _info.Latency = Environment.TickCount - _ping;

                data.Skip(11); // Unknown padding.

                string key, value;

                while (true)
                {
                    key = data.Read<string>();
                    value = data.Read<string>();

                    if (key.Length == 0) break;

                    if (key == "hostname")
                        _info.Name = value;

                    else if (key == "gametype")
                        _info.GameType = value;

                    else if (key == "game_id")
                        _info.GameID = value;

                    else if (key == "version")
                        _info.Version = value;

                    else if (key == "plugins")
                        _info.Plugins = value;

                    else if (key == "map")
                        _info.Map = value;

                    else if (key == "numplayers")
                    {
                        if (!int.TryParse(value, out _info.OnlinePlayers)) return;
                    }

                    else if (key == "maxplayers")
                    {
                        if (!int.TryParse(value, out _info.MaxPlayers)) return;
                    }

                    else if (key == "hostport")
                        _info.HostPort = value;

                    else if (key == "hostip")
                        _info.HostIP = value;
                }

                data.Skip(1);

                _info.Players = new List<string>();

                while (true)
                {
                    key = data.Read<string>();

                    if (key.Length == 0) break;

                    _info.Players.Add(key);
                }

                _success = true;
            }
            else GetChallenge();
        }

        internal Packet GrabData (byte[] packet)
        {
            this.Send(packet);

            Packet recv = this.Receive(2048, packet[2]);

            if (recv == null) return null;

            return recv;
        }

        internal Packet Receive (int len, byte check)
        {
            try
            {
                byte[] buffer = new byte[len];
                int recv = this._sock.Receive(buffer, 0, len, SocketFlags.None);

                if (recv < 5 || buffer[0] != check)
                    return null;

                byte[] nbuf = new byte[recv];
                Buffer.BlockCopy(buffer, 0, nbuf, 0, recv);

                return new Packet(nbuf);
            }
            catch
            {
                return null;
            }
        }

        internal void Send (byte[] data)
        {
            try
            {
                _sock.Send(data);
            }
            catch
            {

            }
        }

        internal void Send (string data)
        {
            try
            {
                this.Send(Encoding.Unicode.GetBytes(data));
            }
            catch
            {

            }
        }
    }
}
