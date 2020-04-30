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

namespace Chatting
{
    public class MesssageDatabaseLayer
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Messages.db")))
                {
                    connection.CreateTable<Msg>();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Insert(Msg message)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Messages.db")))
                {
                    connection.Insert(message);
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }


        public List<Msg> Select()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Messages.db")))
                {
                    return connection.Table<Msg>().ToList();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }



        public List<Msg> DeleteMessage(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Messages.db")))
                {

                    return connection.Query<Msg>("DELETE FROM Messages WHERE Id=? ", id);
                }

            }
            catch (Exception)
            {
                return null;
            }

        }
        public List<Msg> SelectTable(int id)
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Messages.db")))
                {

                    return conn.Query<Msg>("SELECT * from Messages WHERE id=?", id);
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Msg> SelectMessageId(int id)
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Messages.db")))
                {

                    return conn.Query<Msg>("SELECT MessageId from Messages WHERE id=?", id);
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}