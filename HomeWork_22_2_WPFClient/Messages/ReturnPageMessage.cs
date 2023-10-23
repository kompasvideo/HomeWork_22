using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HomeWork_22_2_WPFClient.Messages
{
    public class ReturnPageMessage : IMessage
    {
        public ReturnPageMessage(Page text)
        {
            ReturnPage = text;
        }
        public Page ReturnPage { get; set; }
    }
}
