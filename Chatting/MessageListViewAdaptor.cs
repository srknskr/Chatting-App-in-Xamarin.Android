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
    
    public class MessageListViewAdaptor: BaseAdapter
    {

        private Activity activity;

        private List<Msg> MessageList;
        public MessageListViewAdaptor(Activity activity, List<Msg> MessageList)
        {
            this.activity = activity;
            this.MessageList = MessageList;
        }
        public override int Count
        {
            get { return MessageList.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return MessageList[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.MessageList, parent, false);
            var txtMessage = view.FindViewById<TextView>(Resource.Id.showMessage);


            txtMessage.Text = MessageList[position].Message;
           

            return view;
        }
    }
}