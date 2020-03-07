using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Chatting
{
    [Activity(Label = "DetailsActivity")]
    public class DetailsActivity : Activity
    {
        int position;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Details);
            position = Intent.GetIntExtra("PeoplePosition", -1);

            var item = MainActivity.PeopleList[position];

            var fullPath = MainActivity.PeopleList[position].Image;
            var button = FindViewById<Button>(Resource.Id.msgButton);
            button.Click += OnMsgButtonClick;
            FindViewById<TextView>(Resource.Id.nameTextView).Text = "Name: " + item.Name;
            var image = FindViewById<ImageView>(Resource.Id.imageView);
            Bitmap photo = BitmapFactory.DecodeFile(fullPath);
            image.SetImageBitmap(photo);
        }

        private void OnMsgButtonClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MessageActivity));
            intent.PutExtra("PeoplePosition", position);
            StartActivity(intent);
           // intent.PutExtra("ItemPosition", position);
        }
    }
}