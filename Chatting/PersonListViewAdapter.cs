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
using Plugin.Media;

namespace Chatting
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtName { get; set; }
        public TextView txtSurname { get; set; }

        public ImageView image { get; set; }
        string filepath;

    }
    public class PersonListViewAdapter : BaseAdapter
    {
        private Activity activity;
        
        private List<Person> PersonList;
        public PersonListViewAdapter(Activity activity, List<Person> PersonList)
        {
            this.activity = activity;
            this.PersonList = PersonList;
        }

        public override int Count
        {
            get { return PersonList.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return PersonList[position].Id;
        }

        public override  View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.PersonList, parent, false);
            var txtName = view.FindViewById<TextView>(Resource.Id.textName);
            //var txtSurname = view.FindViewById<TextView>(Resource.Id.textSurname);
            var image = view.FindViewById<ImageView>(Resource.Id.personimage);

            txtName.Text = PersonList[position].Name;
            //txtSurname.Text = PersonList[position].Image;

          

            

            byte[] imageArray = System.IO.File.ReadAllBytes(PersonList[position].Image);
            Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            image.SetImageBitmap(bitmap);

           



            return view;

        }
    }
}