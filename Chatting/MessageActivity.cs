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
    [Activity(Label = "MessageActivity")]
    public class MessageActivity : Activity
    {
        EditText edt;
        TextView msg1;
        TextView msg2;
        TextView msg3;
        TextView msg4;
        TextView msg5;
        TextView msg6;
        TextView msg7;
        TextView msg8;
        TextView msg9;
        TextView msg10;
        static ProgressBar progress;
        int position;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Message);

            position = Intent.GetIntExtra("PeoplePosition", -1);
            // var instructor = ContactData.Instructors[position];
            var messages = MessageData.ContactMessages[position];


            edt = FindViewById<EditText>(Resource.Id.msgEditText);

            progress = FindViewById<ProgressBar>(Resource.Id.progressBar);

            msg1 = FindViewById<TextView>(Resource.Id.textView1);
            msg2 = FindViewById<TextView>(Resource.Id.textView2);
            msg3 = FindViewById<TextView>(Resource.Id.textView3);
            msg4 = FindViewById<TextView>(Resource.Id.textView4);
            msg5 = FindViewById<TextView>(Resource.Id.textView5);
            msg6 = FindViewById<TextView>(Resource.Id.textView6);
            msg7 = FindViewById<TextView>(Resource.Id.textView7);
            msg8 = FindViewById<TextView>(Resource.Id.textView8);
            msg9 = FindViewById<TextView>(Resource.Id.textView9);
            msg10 = FindViewById<TextView>(Resource.Id.textView10);
            var sendButton = FindViewById<Button>(Resource.Id.sendButton);

            msg1.Text = messages.Message1;
            msg2.Text = messages.Message2;
            msg3.Text = messages.Message3;
            msg4.Text = messages.Message4;
            msg5.Text = messages.Message5;
            msg6.Text = messages.Message6;
            msg7.Text = messages.Message7;
            msg8.Text = messages.Message8;
            msg9.Text = messages.Message9;
            msg10.Text = messages.Message10;

            progress.Progress = messages.Count * 10;

            sendButton.Click += OnSendClick;
            







         //   name.Text = ContactData.Instructors[position].Name;

        }
        private void OnSendClick(object sender, EventArgs e)
        {
            var position = Intent.GetIntExtra("PeoplePosition", -1);
            var messages = MessageData.ContactMessages[position];
            string message = edt.Text.ToString();

            int count = messages.Count;
            count++;

            if (message != "")
            {
                if (count <= 10)
                {
                    switch (count)
                    {
                        case 1:
                            messages.Message1 = message;
                            msg1.Text = messages.Message1;
                            break;
                        case 2:
                            messages.Message2 = message;
                            msg2.Text = messages.Message2;
                            break;
                        case 3:
                            messages.Message3 = message;
                            msg3.Text = messages.Message3;
                            break;
                        case 4:
                            messages.Message4 = message;
                            msg4.Text = messages.Message4;
                            break;
                        case 5:
                            messages.Message5 = message;
                            msg5.Text = messages.Message5;
                            break;
                        case 6:
                            messages.Message6 = message;
                            msg6.Text = messages.Message6;
                            break;
                        case 7:
                            messages.Message7 = message;
                            msg7.Text = messages.Message7;
                            break;
                        case 8:
                            messages.Message8 = message;
                            msg8.Text = messages.Message8;
                            break;
                        case 9:
                            messages.Message9 = message;
                            msg9.Text = messages.Message9;
                            break;
                        case 10:
                            messages.Message10 = message;
                            msg10.Text = messages.Message10;
                            break;
                    }
                    messages.Count = count;
                    progress.Progress = messages.Count * 10;
                    edt.Text = "";
                }
                else
                    Toast.MakeText(Application.Context, "You have reached the message limit ", ToastLength.Long).Show();
            }
        }

        //private void OnDetailClick(object sender, EventArgs e)
        //{
        //    var intent = new Intent(this, typeof(DetailsActivity));

        //    intent.PutExtra("ItemPosition", position); // e.Position is the position in the list of the item the use touched

        //    StartActivity(intent);
        //}
    }
}