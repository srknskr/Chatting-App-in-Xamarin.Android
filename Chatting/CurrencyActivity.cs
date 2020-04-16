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
        TextView txt1;
        TextView txt2;
        ListView ListViewItem;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Currency);
            // Create your application here

            //txt1 = FindViewById<Button>(Resource.Id.currency1);
            //txt2 = FindViewById<Button>(Resource.Id.currency2);
            ListView ListViewItem= FindViewById<ListView>(Resource.Id.listView1);



            string url = "http://finance.yahoo.com/webservice/" +
        "v1/symbols/allcurrencies/quote?format=xml";
            try
            {
                // Load the data.
                XmlDocument doc = new XmlDocument();
                doc.Load(url);

                // Process the resource nodes.
                XmlNode root = doc.DocumentElement;
                string xquery = "descendant::resource[@classname='Quote']";
                foreach (XmlNode node in root.SelectNodes(xquery))
                {
                    const string name_query =
                        "descendant::field[@name='name']";
                    const string price_query =
                        "descendant::field[@name='price']";
                    string name =
                        node.SelectSingleNode(name_query).InnerText;
                    string price =
                        node.SelectSingleNode(price_query).InnerText;
                    decimal inverse = 1m / decimal.Parse(price);

                    ListViewItem item = lvwPrices.Items.Add(name);
                    item.SubItems.Add(price);
                    item.SubItems.Add(inverse.ToString("f6"));
                }

                // Sort.
                lvwPrices.Sorting = SortOrder.Ascending;
                lvwPrices.FullRowSelect = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Read Error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }








        }
    }

    //private const string urlPattern = "http://rate-exchange-1.appspot.com/currency?from={0}&to={1}";
    //public string CurrencyConversion(decimal amount, string fromCurrency, string toCurrency)
    //{
    //    string Output = "";
    //    string fromCurrency1 = txt1.Text;
    //    string toCurrency1 = txt2.Text;
    //    decimal amount1 = Convert.ToDecimal(txt1.Text);

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
    //    textBox2.Text = Output;

    //    return Output;
    //}











}