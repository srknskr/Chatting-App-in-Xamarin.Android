using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Chatting
{

    [Activity(Label = "CurrencyActivity")]
    public class CurrencyActivity : Activity
    {
       
        TextView txt3;
        TextView txt4;
        Button pull;
        Button send;
        ListView ListViewItem;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Currency);
            // Create your application here

            
            txt3 = FindViewById<TextView>(Resource.Id.currency3);
            txt4 = FindViewById<TextView>(Resource.Id.currency4);

            pull= FindViewById<Button>(Resource.Id.pullButton);
            send= FindViewById<Button>(Resource.Id.sendCurrencyButton);

            pull.Click += Pull_Click;
            send.Click += Send_Click;
        }

        private void Send_Click(object sender, EventArgs e)
        {
          //  double dollar = Convert.ToDouble(txt4.Text);
          string dollar = txt4.Text;
            var intent = new Intent();
            intent.PutExtra("Dollar", dollar);
            SetResult(Result.Ok, intent);
            Finish();


        }

        private void Pull_Click(object sender, EventArgs e)
        {
             CurrencyConversion(1,"TRY","USD");
        }

        //private const string urlPattern = "http://rate-exchange-1.appspot.com/currency?from={0}&to={1}";


        //public string CurrencyConversion(decimal amount, string fromCurrency, string toCurrency)
        //{
        //    string Output = "";
        //    string fromCurrency1 = txt1.Text;
        //    string toCurrency1 = txt2.Text;
        //    decimal amount1 = Convert.ToDecimal(txt3.Text);

        //    // For other currency symbols see http://finance.yahoo.com/currency-converter/
        //    // Construct URL to query the Yahoo! Finance API

        //    const string urlPattern = "http://finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=l1";
        //    string url = string.Format(urlPattern, fromCurrency1, toCurrency1);

        //    // Get response as string
        //    string response = new WebClient().DownloadString(url);

        //    // Convert string to number
        //    decimal exchangeRate =
        //        decimal.Parse(response, System.Globalization.CultureInfo.InvariantCulture);

        //    // Output the result
        //    Output = (amount1 * exchangeRate).ToString();
        //    txt4.Text = Output;

        //    return Output;
        //}


        private const string urlPattern = "http://rate-exchange-1.appspot.com/currency?from={0}&to={1}";
        public string CurrencyConversion(decimal amount, string fromCurrency, string toCurrency)
        {
            string url = string.Format(urlPattern, fromCurrency, toCurrency);
            string Output = txt4.Text;
            using (var wc = new WebClient())
            {
                var json = wc.DownloadString(url);

                Newtonsoft.Json.Linq.JToken token = Newtonsoft.Json.Linq.JObject.Parse(json);
                decimal exchangeRate = (decimal)token.SelectToken("rate");

                Output = (amount * exchangeRate).ToString();
                txt4.Text = Output;
                return Output;
            }
        }
    }


    }
   












