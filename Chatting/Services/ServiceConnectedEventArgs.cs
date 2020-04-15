using System;

using Android.OS;

namespace Chatting.Services
{
    public class ServiceConnectedEventArgs : EventArgs
    {
        public IBinder Binder { get; set; }
    }
}
