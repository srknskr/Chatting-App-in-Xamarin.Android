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
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtName { get; set; }
        public TextView txtSurname { get; set; }
        

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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.PersonList, parent, false);
            var txtName = view.FindViewById<TextView>(Resource.Id.textName);
            var txtSurname = view.FindViewById<TextView>(Resource.Id.textSurname);

            txtName.Text = PersonList[position].Name;
            txtSurname.Text = PersonList[position].Image;

            return view;

        }
    }
}