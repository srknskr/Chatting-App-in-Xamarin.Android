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
    public class PersonDatabasLayer
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDatabase()
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Person.db")))
                {
                    conn.CreateTable<Person>();
                    
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Insert(Person person)
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Person.db")))
                {
                    conn.Insert(person);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Person> Select()
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Person.db")))
                {
                    return conn.Table<Person>().ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        public List<Msg> SelectList(int id)
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Person.db")))
                {
                    return conn.Query<Msg>("SELECT MessageofPerson from Person WHERE id=?", id);
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
     
        public bool Delete(Person person)
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Person.db")))
                {
                    conn.Delete(person);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool SelectTable(int id)
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(folder, "Person.db")))
                {
                    conn.Query<Person>("SELECT * from Person WHERE id=?", id);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}