using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Chatting
{
    [Table("Messages")]
    public class Msg
    {
        [PrimaryKey,AutoIncrement]
        public int MessageId { get; set; }
        public int Id { get; set; }
        

        public string Message { get; set; }

        //[ForeignKey(typeof(Person))] 
        //public int EmployeeId { get; set; }

    }

}