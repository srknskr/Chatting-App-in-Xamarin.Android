using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Android.Support.V4.App;

using Java.Lang;
using System.Net;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;

using Java.IO;

using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
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
        ImageView img;

        Button cameraButton;
        Button locationButton;
        Button currencyButton;

        string filePath;

        private File _dir;
        private File _file;



        // Unique ID for our notification: 
        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";

        // Number of times the button is tapped (starts with first tap):
        int count = 1;







        protected override void OnCreate(Bundle savedInstanceState)
        {
            CreateNotificationChannel();


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

            cameraButton = FindViewById<Button>(Resource.Id.cameraButton);
            locationButton = FindViewById<Button>(Resource.Id.locationButton);
            currencyButton = FindViewById<Button>(Resource.Id.currencyButton);
            img = FindViewById<ImageView>(Resource.Id.img);

            cameraButton.Click += CameraButton_Click;
            locationButton.Click += LocationButton_Click;
            currencyButton.Click += CurrencyButton_Click;

            //   name.Text = ContactData.Instructors[position].Name;
            //if (IsThereAnAppToTakePictures())
            //{
            //    CreateDirectoryForPictures();
            //}
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification 
                // channel on older versions of Android.
                return;
            }

            var name = Resources.GetString(Resource.String.channel_name);
            var description = GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }


        private void CurrencyButton_Click(object sender, EventArgs e)
        {
            //var intent = new Intent(this, typeof(CurrencyActivity));
            //StartActivityForResult(intent, 99);
            CurrencyConversion(1, "TRY", "USD");
        }
        private const string urlPattern = "http://rate-exchange-1.appspot.com/currency?from={0}&to={1}";
        public string CurrencyConversion(decimal amount, string fromCurrency, string toCurrency)
        {
            string url = string.Format(urlPattern, fromCurrency, toCurrency);
            string Output = edt.Text;
            using (var wc = new WebClient())
            {
                var json = wc.DownloadString(url);

                Newtonsoft.Json.Linq.JToken token = Newtonsoft.Json.Linq.JObject.Parse(json);
                decimal exchangeRate = (decimal)token.SelectToken("rate");

                Output = (amount * exchangeRate).ToString();
                edt.Text = "1 TRY = " + Output + " USD";
                return edt.Text;
            }
        }


        private void LocationButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LocationActivity));
            StartActivityForResult(intent, 98);

        }


        //private void CreateDirectoryForPictures()
        //{
        //    _dir = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "CameraAppDemo");
        //    if (!_dir.Exists())
        //    {
        //        _dir.Mkdirs();
        //    }
        //}

        //private bool IsThereAnAppToTakePictures()
        //{
        //    Intent intent = new Intent(MediaStore.ActionImageCapture);
        //    IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
        //    return availableActivities != null && availableActivities.Count > 0;
        //}
        async void CameraButton_Click(object sender, EventArgs e)
        {


            Intent intent = new Intent(this, typeof(CameraActivity));



            StartActivityForResult(intent, 99);

            ////////////var intent = new Intent(this, typeof(CameraActivity));
            ////////////StartActivityForResult(intent,100);

            //await CrossMedia.Current.Initialize();

            //if (!CrossMedia.Current.IsTakePhotoSupported)
            //{
            //    Toast.MakeText(this, "Upload not suported ", ToastLength.Short).Show();
            //    return;
            //}

            //var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            //{
            //    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
            //    CompressionQuality = 40
            //});
            //filePath = file.Path;
            //byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            //Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            //img.SetImageBitmap(bitmap);
        }

        private void OnSendClick(object sender, EventArgs e)
        {
            var position = Intent.GetIntExtra("PeoplePosition", -1);
            var messages = MessageData.ContactMessages[position];
            string message = edt.Text.ToString();

            if (edt.Text.Contains("ASAP")){
              message = edt.Text.ToString().Replace("ASAP", "As Soon As Possible");
            }
            else if (edt.Text.Contains("BBL"))
            {
                message = edt.Text.ToString().Replace("BBL", "Be Back Later");
            }
            else if (edt.Text.Contains("OMG"))
            {
                message = edt.Text.ToString().Replace("OMG", "Oh My God");
            }
            else if (edt.Text.Contains("TTYL"))
            {
                message = edt.Text.ToString().Replace("TTYL", "Talk To You Later");
            }

            int count = messages.Count;
            count++;

            if (message != "")
            {
                if (count <= 10)
                {
                    // Pass the current button press count value to the next activity:
                    var valuesForActivity = new Bundle();
                    valuesForActivity.PutInt(COUNT_KEY, count);

                    // When the user clicks the notification, SecondActivity will start up.
                    var resultIntent = new Intent(this, typeof(MessageActivity));
                    resultIntent.PutExtra("PeoplePosition", position);
                    //    StartActivity(resultIntent);
                    
                    

                    // Pass some values to SecondActivity:
                    resultIntent.PutExtras(valuesForActivity);

                    // Construct a back stack for cross-task navigation:
                    var stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
                    stackBuilder.AddParentStack(Class.FromType(typeof(MessageActivity)));
                    stackBuilder.AddNextIntent(resultIntent);

                    // Create the PendingIntent with the back stack:            
                    var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

                    // Build the notification:
                    var builder = new NotificationCompat.Builder(this, CHANNEL_ID)
                                  .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                                  .SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
                                  .SetContentTitle("Button Clicked") // Set the title
                                  .SetNumber(count) // Display the count in the Content Info
                                  .SetSmallIcon(Resource.Drawable.message) // This is the icon to display
                                  .SetContentText($"You have {count} messages.") // the message to display.
                                  .SetVibrate(new long[] { 500, 1000 })
                                  .SetLights(Color.Red, 3000, 3000)
                                  .SetSound(Android.Net.Uri.Parse("uri://sadfasdfasdf.mp3"));



                    // Finally, publish the notification:
                    var notificationManager = NotificationManagerCompat.From(this);
                    notificationManager.Notify(NOTIFICATION_ID, builder.Build());

                    // Increment the button press count:
                    //count++;


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


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            //// make it available in the gallery


            //Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            //Uri contentUri = Uri.FromFile(_file);
            //mediaScanIntent.SetData(contentUri);
            //SendBroadcast(mediaScanIntent);

            //// display in ImageView. We will resize the bitmap to fit the display
            //// Loading the full sized image will consume to much memory 
            //// and cause the application to crash.
            //int height = 50;
            //int width = 50;
            //using (Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height))
            //{
            //    img.RecycleBitmap();
            //    img.SetImageBitmap(bitmap);
            //    edt.Text = Convert.ToString(bitmap);
            //}

            if (requestCode == 98 && resultCode == Result.Ok)
            {
                string lat = data.GetStringExtra("Lat");
                string longt = data.GetStringExtra("Longt");
                edt.Text = lat + " " + longt;

            }
            if (requestCode == 99 && resultCode == Result.Ok)
            {
                string bitmapName = data.GetStringExtra("bitmap");
                
               
                // string uriName= data.GetStringExtra("image3");


                string filePath = data.GetStringExtra("filepath");
                byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                img.SetImageBitmap(bitmap);
                edt.Text = filePath;


                //  string longt= data.GetStringExtra("Longt");


                //Uri imageUri = data.getData();
                //Bitmap bitmap = MediaStore.Images.Media.getBitmap(this.getContentResolver(), imageUri);


            }
        }
    }



}