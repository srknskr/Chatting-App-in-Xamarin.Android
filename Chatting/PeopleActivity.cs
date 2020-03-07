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
    [Activity(Label = "PeopleActivity")]
    public class PeopleActivity : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.People);
            var lv = FindViewById<ListView>(Resource.Id.listView);


            lv.Adapter = new ArrayAdapter<People>(this, Android.Resource.Layout.SimpleListItem1, Android.Resource.Id.Text1, MainActivity.PeopleList);


            lv.ItemClick += OnPeopleClick;
        }

        private void OnPeopleClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(DetailsActivity));

            intent.PutExtra("PeoplePosition", e.Position); // e.Position is the position in the list of the item the use toucheds
            StartActivity(intent);
        }
    }
}