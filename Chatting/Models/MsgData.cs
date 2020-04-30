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
    public  class MsgData
    {
        public static List<Msg> MsgList { get; set; }
        //static MsgData()
        //{
        //    var temp = new List<Msg>();
        //    MsgList=temp.OrderBy(i => i.Id).ToList();
        //}
    }
}