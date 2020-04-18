using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Hardware;
using Android.Widget;
using Android.Util;

namespace Chatting.Services
{
    [Service]
    public class ShakeToLaunchService : Service, ISensorEventListener
    {
        SensorManager sensorManager;

        const int NotifcationID = 42;

        const double ShakeThreshold = 5.0;

        const int SamplingInterval = 100; //ms
        const int gestureTimeout = 500; //ms
        const int ShakesInGesture = 5;

        string intentAction;

        double lastSampleTime;
        double lastShakeTime;

        float lastX, lastY, lastZ;

        int shakeCount = 0;

        public override void OnCreate()
        {
            base.OnCreate();

            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 100, new Intent(this, typeof(AddPersonActivity)), 0);
           

            System.Diagnostics.Debug.WriteLine("ShakeToLaunchServices: OnCreate");

            sensorManager = (SensorManager)GetSystemService(Context.SensorService);

            var accelerometer = sensorManager.GetDefaultSensor(SensorType.Accelerometer);

            if (accelerometer != null)
            {
                sensorManager.RegisterListener(this, accelerometer, SensorDelay.Normal);
            }
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            
            System.Diagnostics.Debug.WriteLine("ShakeToLaunchServices: OnStartCommand");

            intentAction = intent.GetStringExtra("Action");

            var isForeground = intent.GetBooleanExtra("Notification", false);

            if (isForeground == true)
            {
                var title = intent.GetStringExtra("Title");

                var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MainActivity)), 0);

                var notification = new Notification.Builder(this)
                      .SetSmallIcon(Resource.Drawable.plus)
                    .SetColor(0x81CBC4)
                    // .SetContentTitle(GetString(Resource.String.ApplicationName))
                    //   .SetContentText(String.Format(GetString(Resource.String.NotificationDescription), title))
                    .SetContentIntent(pendingIntent).Build();

                StartForeground(NotifcationID, notification);



            }
            else
            {
                StopForeground(true);
            }

            return StartCommandResult.RedeliverIntent;
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override void OnDestroy()
        {
            System.Diagnostics.Debug.WriteLine("ShakeToLaunchServices: OnDestroy");

            sensorManager.UnregisterListener(this);
            base.OnDestroy();
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            var currentTime = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond).TotalMilliseconds;

            var deltaTime = currentTime - lastSampleTime;

            if (deltaTime < SamplingInterval) // only check every 100ms as defined in SamplingInterval
                return;

            //reset shake count if it's been too long since last shake
            if ((currentTime - lastShakeTime) > gestureTimeout)
                shakeCount = 0;

            var currentX = e.Values[0];
            var currentY = e.Values[1];
            var currentZ = e.Values[2];

            bool isShake = IsSignifigantChange(currentX, lastX) | IsSignifigantChange(currentY, lastY) | IsSignifigantChange(currentZ, lastZ);

            if (isShake)
            {
                shakeCount++;
                lastShakeTime = currentTime;
            }

            if (shakeCount >= ShakesInGesture)
            {
                OnShake();
                shakeCount = 0;
            }

            //save the accel values
            lastX = currentX;
            lastY = currentY;
            lastZ = currentZ;

            lastSampleTime = currentTime;
        }


       public void OnShake()
        {
            System.Diagnostics.Debug.WriteLine("shake detected");




            //var intent = new Intent(this, typeof(AddPersonActivity));
            //intent.SetFlags(ActivityFlags.NewTask);
            //StartActivity(intent);


            Intent intent = new Intent(this,typeof(AddPersonActivity));
            intent.SetFlags(ActivityFlags.NewTask);

            StartActivity(intent);
            

            string name = intent.GetStringExtra("PersonName");
            string image = intent.GetStringExtra("Image");

            Toast.MakeText(Android.App.Application.Context, image, ToastLength.Short).Show();



            MainActivity.PeopleList.Add(new People(name, image));
            MessageData.ContactMessages.Add(new Message(name, 0, "", "", "", "", "", "", "", "", "", ""));
            //  MainActivity.PeopleList.Add(new People("", image));
            //  PendingIntent pendingIntent = PendingIntent.GetActivity(this, 100, new Intent(this, typeof(AddPersonActivity)), 0);

        }


        bool IsSignifigantChange(double value1, double value2)
        {
            return Math.Abs(value1 - value2) > ShakeThreshold;
        }
        
    }
}