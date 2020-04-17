using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System;
using Android.Content;
using Android;
using System.Drawing;
using Chatting.Services;
using Android.Provider;

namespace Chatting
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        readonly string[] permissionGroup =
           {
            Manifest.Permission.ReadExternalStorage,
        Manifest.Permission.WriteExternalStorage,
        Manifest.Permission.Camera
        };
        public static List<People> PeopleList = new List<People>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestPermissions(permissionGroup, 0);
            //PeopleList.Add(new People("Serkan", ""));
            //PeopleList.Add(new People("Ahmet"));
            //PeopleList.Add(new People("Ali"));
            //PeopleList.Add(new People("Elif"));
            //PeopleList.Add(new People("Ayşe"));

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);


            FindViewById<Button>(Resource.Id.peopleButton).Click += OnPeopleClick;
            FindViewById<Button>(Resource.Id.addPersonButton).Click += OnAddPersonClick;
            FindViewById<Button>(Resource.Id.aboutButton).Click += OnAboutClick;

            /////////////////////////////////
            ///
            
            string intentAction;
            string title;

            
                intentAction = MediaStore.IntentActionStillImageCamera;
                title = "Camera";
            
           

            var intent = new Intent(this, typeof(ShakeToLaunchService));
            intent.PutExtra("Title", title);
            intent.PutExtra("Action", intentAction);
            //intent.PutExtra("Notification", switchNotification.Checked);

            StartService(intent);

            


            //////////////////


        }

        private void OnPeopleClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PeopleActivity));
            StartActivity(intent);
        }

        private void OnAddPersonClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AddPersonActivity));
            StartActivityForResult(intent, 100);
        }

        private void OnAboutClick(object sender, EventArgs e)
        {
            StartActivity(typeof(AboutActivity));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 100 && resultCode == Result.Ok)
            {
                string name = data.GetStringExtra("PersonName");
                string image = data.GetStringExtra("Image");

                Toast.MakeText(Android.App.Application.Context, image, ToastLength.Short).Show();



                MainActivity.PeopleList.Add(new People(name, image));
                MessageData.ContactMessages.Add(new Message(name, 0, "", "", "", "", "", "", "", "", "", ""));
            }

            else if (requestCode == 99 && resultCode == Result.Ok)
            {
                string dollar = data.GetStringExtra("Dollar");
                FindViewById<EditText>(Resource.Id.textView10).Text = dollar;


            }
        }


    }
}