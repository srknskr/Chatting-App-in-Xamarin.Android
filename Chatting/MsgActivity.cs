using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Android.Support.V4.App;

using Java.Lang;

namespace Chatting
{
    [Activity(Label = "MsgActivity")]
    public class MsgActivity : Activity
    {
        //string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        MesssageDatabaseLayer db;

        int position;

        List<Msg> messageSource;

        //  public static List<Msg> personmessage = Person.MessageofPerson;



        ListView MessageList;
        EditText edt;
        static ProgressBar progress;
        ImageView img;
        Button deleteButton;
        Button cameraButton;
        Button locationButton;
        Button currencyButton;

        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            CreateNotificationChannel();

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Msg);
            progress = FindViewById<ProgressBar>(Resource.Id.progressBar3);
            MessageList = FindViewById<ListView>(Resource.Id.msgList);
            edt = FindViewById<EditText>(Resource.Id.msgText);
            var sendButton = FindViewById<Button>(Resource.Id.sendBtn);
            cameraButton = FindViewById<Button>(Resource.Id.cameraBtn);
            locationButton = FindViewById<Button>(Resource.Id.locationBtn);
            currencyButton = FindViewById<Button>(Resource.Id.currencyBtn);
            deleteButton = FindViewById<Button>(Resource.Id.deletebutton);
            img = FindViewById<ImageView>(Resource.Id.msgimg);

            sendButton.Click += SendButton_Click;
            cameraButton.Click += CameraButton_Click;
            locationButton.Click += LocationButton_Click;
            currencyButton.Click += CurrencyButton_Click;
            deleteButton.Click += DeleteButton_Click;

            position = Intent.GetIntExtra("PeoplePosition", -1) + 1;

            db = new MesssageDatabaseLayer();
            db.CreateDatabase();
            LoadData();

            // progress.Progress = messageSource.Count * 10;

            MessageList.ItemClick += (s, e) =>
            {
                //Set Backround for selected item  
                for (int i = 0; i < MessageList.Count; i++)
                {
                    if (e.Position == i)
                        MessageList.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Pink);
                    else
                        MessageList.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }


                ////Binding Data  
                //var txtName = e.View.FindViewById<TextView>(Resource.Id.textName);
                //var txtSurname = e.View.FindViewById<TextView>(Resource.Id.textSurname);




                edt.Tag = e.Id;




            };
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            bool isEmpty = !messageSource.Any();
            if (isEmpty)
            {
                Toast.MakeText(Android.App.Application.Context, "There is no message you can delete", ToastLength.Long).Show();
            }
            else
            {
                List<Msg> MessageId = db.SelectMessageId(position);
                //MessageId.Sort();
                //for (int i = 0; i < MessageId.Count; i++)
                //{
                //    var item = MessageId[i];


                //}
                //if (MessageId.Count > 0)
                //{
                //    var item = MessageId[MessageId.Count - 1];
                //}
                messageSource.RemoveAt(messageSource.Count - 1);

                db.DeleteMessage(position);
                edt.Text = "";
                LoadData();
            }
        }

        public void LoadData()
        {
            messageSource = db.SelectTable(position);
            var adapter = new MessageListViewAdaptor(this, messageSource);
            MessageList.Adapter = adapter;
            progress.Progress = messageSource.Count * 10;


        }


        private void CurrencyButton_Click(object sender, EventArgs e)
        {
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

        private void CameraButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(CameraActivity));



            StartActivityForResult(intent, 99);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            Msg message = new Msg()
            {
                Id = position,
                Message = edt.Text.ToString()

            };
            if (edt.Text.Contains("ASAP"))
            {
                message.Message = edt.Text.ToString().Replace("ASAP", "As Soon As Possible");
            }
            else if (edt.Text.Contains("BBL"))
            {
                message.Message = edt.Text.ToString().Replace("BBL", "Be Back Later");
            }
            else if (edt.Text.Contains("OMG"))
            {
                message.Message = edt.Text.ToString().Replace("OMG", "Oh My God");
            }
            else if (edt.Text.Contains("TTYL"))
            {
                message.Message = edt.Text.ToString().Replace("TTYL", "Talk To You Later");
            }

            int count = messageSource.Count;

            if (edt.Text != "")
            {
                if (count < 10)
                {
                    db.Insert(message);
                    edt.Text = "";
                    LoadData();


                    // Pass the current button press count value to the next activity:
                    var valuesForActivity = new Bundle();
                    valuesForActivity.PutInt(COUNT_KEY, count);

                    // When the user clicks the notification, SecondActivity will start up.
                    var resultIntent = new Intent(this, typeof(AddContactActivity));
                    resultIntent.PutExtra("PeoplePosition", position);
                    //    StartActivity(resultIntent);



                    // Pass some values to SecondActivity:
                    resultIntent.PutExtras(valuesForActivity);

                    // Construct a back stack for cross-task navigation:
                    var stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
                    stackBuilder.AddParentStack(Class.FromType(typeof(AddContactActivity)));
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
                                  .SetContentText($"You have {count + 1} messages.") // the message to display.
                                  .SetVibrate(new long[] { 500, 1000 })
                                  .SetLights(Color.Red, 3000, 3000)
                                  .SetSound(Android.Net.Uri.Parse("uri://sadfasdfasdf.mp3"));



                    // Finally, publish the notification:
                    var notificationManager = NotificationManagerCompat.From(this);
                    notificationManager.Notify(NOTIFICATION_ID, builder.Build());














                }
                else
                    Toast.MakeText(Application.Context, "You have reached the message limit ", ToastLength.Long).Show();
            }

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
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

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