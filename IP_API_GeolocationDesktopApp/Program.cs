using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace IP_API_GeolocationDesktopApp
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        public string QueryIPLocation(string strIPAddress)
        {
            // IP API URL
            var Ip_Api_Url = "http://ip-api.com/json/" + strIPAddress; // 146.129.30.102 - This is a sample IP address. You can pass yours if you want to test

            String result = String.Empty;

            // Use HttpClient to get the details from the Json response
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // Pass API address to get the Geolocation details 
                httpClient.BaseAddress = new Uri(Ip_Api_Url);
                HttpResponseMessage httpResponse = httpClient.GetAsync(Ip_Api_Url).GetAwaiter().GetResult();
                // If API is success and receive the response, then get the location details
                if (httpResponse.IsSuccessStatusCode)
                {
                    var geolocationInfo = httpResponse.Content.ReadAsAsync<LocationDetails_IpApi>().GetAwaiter().GetResult();
                    if (geolocationInfo != null)
                    {
                        result += "IP API GeoLocation Results:\n===========================================\n";
                        result += "Query: " + geolocationInfo.query + "\n";
                        result += "Country: " + geolocationInfo.country + "\n";
                        result += "Country Code: " + geolocationInfo.countryCode + "\n";
                        result += "Region: " + geolocationInfo.regionName + "\n";
                        result += "Region: " + geolocationInfo.region + "\n";
                        result += "City: " + geolocationInfo.city + "\n";

                        result += "Isp: " + geolocationInfo.isp + "\n";
                        result += "Organization: " + geolocationInfo.org + "\n";
                        result += "Latitude: " + geolocationInfo.lat + "\n";
                        result += "Longitude: " + geolocationInfo.lon + "\n";

                        result += "Timezone: " + geolocationInfo.timezone + "\n";
                        result += "Zip: " + geolocationInfo.zip + "\n";
                        result += "Status: " + geolocationInfo.status + "\n";

                    }

                }
            }

            return result;
        }
    }



}




