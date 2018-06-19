using System;

namespace webapp.Models
{
    public class MinecraftEvent
    {
        public string status;
        public bool online;
        public string motd;
        public string error;
        public PlayerInfo players;
        public ServerInfo server;
        public string last_online;
        public string last_updated;
        public string duration;
    }

    public class PlayerInfo
    {
        public string max;
        public string now;
    }

    public class ServerInfo
    {
        public string name;
        public Double protocol;
    }
}