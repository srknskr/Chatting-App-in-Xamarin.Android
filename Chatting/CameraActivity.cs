using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Content.PM;
using Android.Graphics;
using Android.Provider;

using Java.IO;

using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;


namespace Chatting
{
    [Activity(Label = "CameraActivity")]
    public class CameraActivity : Activity
    {

        private File _dir;
        private File _file;
        private ImageView _imageView;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // make it available in the gallery
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(_file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // display in ImageView. We will resize the bitmap to fit the display
            // Loading the full sized image will consume to much memory 
            // and cause the application to crash.
            int height = _imageView.Height;
            int width = Resources.DisplayMetrics.WidthPixels;
            using (Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height))
            {
                _imageView.RecycleBitmap();
                _imageView.SetImageBitmap(bitmap);

                string filePath = _file.Path;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Camera);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                Button sendBtn = FindViewById<Button>(Resource.Id.sendCamera);
                Button button = FindViewById<Button>(Resource.Id.myButton);
                _imageView = FindViewById<ImageView>(Resource.Id.imageView1);

                sendBtn.Click += SendBtn_Click;
                button.Click += TakeAPicture;
            }
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            var intent = new Intent();
            Bitmap bitmap = _file.Path.LoadAndResizeBitmap(50, 50);
            string bit = Convert.ToString(bitmap);
            intent.PutExtra("image", bit);



            Uri contentUri = Uri.FromFile(_file);
            string uriName = Convert.ToString(contentUri);

            string filePath = _file.Path;
            intent.PutExtra("image2", filePath);



            intent.PutExtra("image3", uriName);

            SetResult(Result.Ok, intent);

            Finish();


        }

        private void CreateDirectoryForPictures()
        {
            _dir = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "CameraAppDemo");
            if (!_dir.Exists())
            {
                _dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            _file = new File(_dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(_file));

            StartActivityForResult(intent, 0);
            //var  filepath = _file.Path;
            //byte[] imageArray = System.IO.File.ReadAllBytes(_file.Path);
            //Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            //img.SetImageBitmap(bitmap);
        }
    }
}
