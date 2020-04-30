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
    [Activity(Label = "AddContactActivity")]
    public class AddContactActivity : Activity
    {
        
        ListView List;
        public static List<Person> listSource = new List<Person>();
        int position;
        EditText edtName;
        PersonDatabasLayer db;
        MesssageDatabaseLayer msgdb;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AddContact);

            db = new PersonDatabasLayer();
            db.CreateDatabase();

            msgdb = new MesssageDatabaseLayer();


            List = FindViewById<ListView>(Resource.Id.PersonListView);
            List.ItemClick += List_ItemClick;
            edtName = FindViewById<EditText>(Resource.Id.edtName);
            var edtSurname = FindViewById<EditText>(Resource.Id.edtSurname);
            var btnAdd = FindViewById<Button>(Resource.Id.addButton);
            LoadData();

            btnAdd.Click += delegate
            {
                Person person = new Person()
                {
                    Name = edtName.Text,
                    Image = edtSurname.Text,
                  



                };
                db.Insert(person);
                LoadData();
            };

            //List.ItemClick += List_ItemClick;
            //{
            //    //Set Backround for selected item  
            //    for (int i = 0; i < List.Count; i++)
            //    {
            //        if (e.Position == i)
            //            List.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Pink);
            //        else
            //            List.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
            //    }


            //    //Binding Data  
            //var txtName = e.View.FindViewById<TextView>(Resource.Id.textName);
            //var txtSurname = e.View.FindViewById<TextView>(Resource.Id.textSurname);



            //    edtName.Tag = e.Id;



            //    var intent = new Intent(this, typeof(MsgActivity));
            //    intent.PutExtra("PeoplePosition", position);
            //    StartActivity(intent);
            //};
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(MsgActivity));
            intent.PutExtra("PeoplePosition", e.Position); // e.Position is the position in the list of the item the use toucheds
            StartActivity(intent);

        }

        private void LoadData()
        {
            listSource = db.Select();
            var adapter = new PersonListViewAdapter(this, listSource);
            List.Adapter = adapter;
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}