using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client c = new Client();
            string str = Console.ReadLine();
            c.connect(str);
            string mess, to;
            for (; ; )
            {
                mess = Console.ReadLine();
                to = Console.ReadLine();
                c.SendMessage(mess, to);
            }
        }
    }
}
