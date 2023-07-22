using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MessageServer.Entity
{
    [Serializable]
    public class Message
    {
        public string Sender { get; set; }

        public string Reccepient { get; set; }

        public DateTime IncomeTime { get; set; }
        
        public string Messages { get; set; }

        public override string ToString()
        {
            return $"{IncomeTime} от {Sender}. Сообщение: {Messages}"; 
        }

        public byte ToByte()
        {
            return Convert.ToByte(this.ToString());
        }
        public string Command { get; set; }
    }
}
