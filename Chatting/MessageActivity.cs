using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Plugin.Media;
using Android.Support.V4.App;

using Java.Lang;
using Android.Media;

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
            throw new NotImplementedException();
        }

        private void LocationButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LocationActivity));
            StartActivity(intent);
        }

        async void CameraButton_Click(object sender, EventArgs e)
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
            img.SetImageBitmap(bitmap);
        }

        private void OnSendClick(object sender, EventArgs e)
        {
            var position = Intent.GetIntExtra("PeoplePosition", -1);
            var messages = MessageData.ContactMessages[position];
            string message = edt.Text.ToString();

            int count = messages.Count;
            count++;

            if (message != "")
            {
                if (count <= 10)
                {
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
                          .SetSmallIcon(Resource.Drawable.messenger) // This is the icon to display
                          .SetContentText($"You have {count} messages.") // the message to display.
                          .SetVibrate(new long[] { 500, 1000 })
                          .SetLights(Color.Red, 3000, 3000)
                          .SetSound(Android.Net.Uri.Parse("uri://sadfasdfasdf.mp3"));
                          


            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(NOTIFICATION_ID, builder.Build());

            // Increment the button press count:
            count++;


        }

        //private void OnDetailClick(object sender, EventArgs e)
        //{
        //    var intent = new Intent(this, typeof(DetailsActivity));

        //    intent.PutExtra("ItemPosition", position); // e.Position is the position in the list of the item the use touched

        //    StartActivity(intent);
        //}
    }
}