using System;
using System.Collections.Generic;
using System.Drawing;
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
    [Activity(Label = "AddPersonActivity")]
    public class AddPersonActivity : Activity
    {
        ImageView ImgView;
        string filePath;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddPerson);

            FindViewById<Button>(Resource.Id.saveButton).Click += OnSaveClick;
            FindViewById<Button>(Resource.Id.cancelButton).Click += OnCancelClick;
            FindViewById<Button>(Resource.Id.uploadButton).Click += OnUploadButton;
            ImgView = (ImageView)FindViewById(Resource.Id.imageView);
            
        }

        private void OnUploadButton(object sender, EventArgs e)
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
            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            ImgView.SetImageBitmap(bitmap);

        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            Finish();
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            string name = FindViewById<EditText>(Resource.Id.nameInput).Text;
            string image = filePath;
            var intent = new Intent();
            intent.PutExtra("PersonName", name);
            intent.PutExtra("Image", image);
            SetResult(Result.Ok, intent);


            Finish();
        }
    }
}