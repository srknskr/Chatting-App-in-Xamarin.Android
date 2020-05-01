using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Plugin.Media;

namespace Chatting
{
    [Activity(Label = "AddContactActivity")]
    public class AddContactActivity : Activity
    {
        ImageView img;
        public string filePath;
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
            //var edtSurname = FindViewById<EditText>(Resource.Id.edtSurname);
            var btnAdd = FindViewById<Button>(Resource.Id.addButton);
            img = FindViewById<ImageView>(Resource.Id.contactimage);
            img.Click += İmg_Click;
            LoadData();

            btnAdd.Click += delegate
            {
                Person person = new Person()
                {
                    Name = edtName.Text,
                    Image = filePath,
                  



                };
                db.Insert(person);
                edtName.Text = "";
                img.SetImageResource(Resource.Drawable.picture); 
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

        public void İmg_Click(object sender, EventArgs e)
        {
            UploadPhoto();
        }
        async void UploadPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                Toast.MakeText(this, "Upload not suported ", ToastLength.Short).Show();
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                CompressionQuality = 40
            });


            filePath = file.Path;
            byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
            Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            img.SetImageBitmap(bitmap);

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