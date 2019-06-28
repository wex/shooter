using System;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

namespace colorPick
{
    class Imgur
    {
        public static string UploadImage(byte[] image)
        {
            using (WebClient client = new WebClient())
            {
                // Client-ID: 443aec9b34a7d88
                // Client-Secret: a837cf469c5aa97f3963babb26701347beeac52e
                client.Headers.Add(HttpRequestHeader.Authorization, $"Client-ID 443aec9b34a7d88");

                var response = client.UploadValues(new Uri("https://api.imgur.com/3/upload"), new NameValueCollection()
                {
                    {"image", Convert.ToBase64String(image)},
                    {"type", "base64"},
                    {"name", "Screenshot"},
                });

                var result = Encoding.UTF8.GetString(response);

                JavaScriptSerializer parser = new JavaScriptSerializer();
                dynamic json = parser.Deserialize<dynamic>(result);
                    
                return json["data"]["link"].ToString();
            }
        }
    }
}
