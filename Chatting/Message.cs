using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Chatting
{
    public class Message
    {
        public Message()
        {
        }

        public Message(string name, int count, string message1, string message2, string message3, string message4, string message5, string message6, string message7, string message8, string message9, string message10)
        {
            Name = name;  
            Count = count;
            Message1 = message1;
            Message2 = message2;
            Message3 = message3;
            Message4 = message4;
            Message5 = message5;
            Message6 = message6;
            Message7 = message7;
            Message8 = message8;
            Message9 = message9;
            Message10 = message10;
        }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Message1 { get; set; }
        public string Message2 { get; set; }
        public string Message3 { get; set; }
        public string Message4 { get; set; }
        public string Message5 { get; set; }
        public string Message6 { get; set; }
        public string Message7 { get; set; }
        public string Message8 { get; set; }
        public string Message9 { get; set; }
        public string Message10 { get; set; }
    }
}