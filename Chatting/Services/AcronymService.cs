using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Chatting.Services
{
    [Service]
    class AcronymService : Service
    {
        EditText edt;
        string asap, bbl, omg, ttly;


        PendingIntent pendingIntent;
        public override IBinder OnBind(Intent intent)
        {
            Log.Debug(tag, "OnBind called");

            throw new NotImplementedException();
        }

        public override void OnCreate()
        {
            asap = "asap";
            bbl = "bbl";
            omg = "omg";
            ttly = "ttly";

            base.OnCreate();
            Log.Debug(tag, "Service created");

            pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MessageActivity)), 0);
        }


        const string tag = "MyDownloadService";



        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug(tag, "OnStartCommand called");
            Task.Run(() =>
            {
               

            });
            return StartCommandResult.RedeliverIntent;
        }

        public override void OnDestroy()
        {
            Log.Debug(tag, "Service destroyed");
        }
    }
}
