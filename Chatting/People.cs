using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Chatting
{
    
    public class People
    {
        public People(string name, string image)
        {
            Name = name;
            Image = image;

        }
        public string Name { get; set; }
        public string Image { get; set; }



        public override string ToString()
        {
            return Name;
        }
    }
}