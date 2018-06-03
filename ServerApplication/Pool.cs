using CardGamesLibrary.Models.NetworkPacket;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication
{
    public class Pool
    {
        private readonly List<Connection> connections = new List<Connection>();
        public const int MAX_SIZE = 4;

        public int AddConnection(Connection connection)
        {
            Console.WriteLine("Connection added : " + connection.ToString());
            Console.WriteLine("Connection number : " + this.connections.Count);
            this.connections.Add(connection);
            return 0;
        }

        public int RemoveConnection(Connection connection)
        {
            foreach (Connection test in this.connections)
            {
                if (test.GetHashCode() == connection.GetHashCode())
                {
                    Console.WriteLine("Connection number : " + this.connections.IndexOf(connection));
                    this.connections.Remove(test);
                    Console.WriteLine("Connection removed : " + connection.ToString());
                    return 0;
                }
            }
            Console.WriteLine("Failed to remove connection : " + connection.ToString());
            return 84;
        }

        public Connection getConnection(int position)
        {
            if (position < this.GetSize())
            {
                return this.connections[position];
            }
            else
            {
                return null;
            }
        }
        public int GetSize()
        {
            return this.connections.Count;
        }
    }
}
