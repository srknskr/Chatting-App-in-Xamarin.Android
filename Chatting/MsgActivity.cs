using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SQLite.Net.Platform.XamarinAndroid;
using static Android.Provider.SyncStateContract;

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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Msg);

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


                //Binding Data  
                var txtName = e.View.FindViewById<TextView>(Resource.Id.textName);
                var txtSurname = e.View.FindViewById<TextView>(Resource.Id.textSurname);




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
                List < Msg> MessageId = db.SelectMessageId(position);
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
                LoadData();
            }
        }

        public void LoadData()
        {
            messageSource = db.SelectTable(position);
            var adapter = new MessageListViewAdaptor(this, messageSource);
            MessageList.Adapter = adapter;
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
            db.Insert(message);
            LoadData();

            edt.Text = "";






            //position = Intent.GetIntExtra("PeoplePosition", -1);
            //var messages =messageSource[position];
            //string mes = edt.Text.ToString();

            //messages.Message = mes;


            //db.Insert(messages);
            //LoadData();
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