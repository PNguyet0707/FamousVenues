using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInitializationOrder
{
    public class Initialization
    {
        public static string ConnectStr
        {
            get;
            private set;
        } = "this is connection string";
        public static int ConnectCount 
        { 
            get;
            private set; 
        } 
        public string QueueName { get; set; }
        public Initialization(string  queueName)
        {
            QueueName = queueName;
            ConnectCount++;
        }

    }
}
