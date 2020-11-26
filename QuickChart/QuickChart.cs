using System;
using System.Collections.Specialized;
using System.Web;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.IO;

namespace QuickChart
{
    internal class QuickChartShortUrlResponse
    {
#pragma warning disable IDE1006 // Naming Styles
        public bool status { get; set; }
        public string url { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }

    public class Chart
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double DevicePixelRatio { get; set; }
        public string BackgroundColor { get; set; }
        public string Key { get; set; }
        public string Config { get; set; }

        public string Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        private static readonly HttpClient Client = new HttpClient();

        public Chart()
        {
            Width = 500;
            Height = 300;
            DevicePixelRatio = 1.0;
            BackgroundColor = "transparent";

            Protocol = "https";
            Host = "quickchart.io";
            Port = 443;
        }

        public string GetUrl()
        {
            if (Config == null)
            {
                throw new NullReferenceException("You must set Config on the QuickChart object before generating a URL");
            }

            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("w", Width.ToString());
            queryString.Add("h", Height.ToString());
            queryString.Add("devicePixelRatio", DevicePixelRatio.ToString());
            queryString.Add("bkg", BackgroundColor);
            queryString.Add("c", Config);
            if (!string.IsNullOrEmpty(Key))
            {
                queryString.Add("key", Key);
            }

            return $"{Protocol}://{Host}:{Port}/chart?{queryString}";
        }

        public string GetShortUrl()
        {
            if (Config == null)
            {
                throw new NullReferenceException("You must set Config on the QuickChart object before generating a URL");
            }

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IgnoreNullValues = true;

            String json = JsonSerializer.Serialize(new
            {
                width = Width,
                height = Height,
                backgroundColor = BackgroundColor,
                devicePixelRatio = DevicePixelRatio,
                chart = Config,
                key = Key,
            }, options);

            string url = $"{Protocol}://{Host}:{Port}/chart/create";

            HttpResponseMessage response = Client.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json")
            ).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unsuccessful response from API");
            }

            string responseText = response.Content.ReadAsStringAsync().Result;
            QuickChartShortUrlResponse result = JsonSerializer.Deserialize<QuickChartShortUrlResponse>(responseText);
            return result.url;
        }

        public byte[] ToByteArray()
        {
            if (Config == null)
            {
                throw new NullReferenceException("You must set Config on the QuickChart object before generating a URL");
            }

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IgnoreNullValues = true;

            String json = JsonSerializer.Serialize(new
            {
                width = Width,
                height = Height,
                backgroundColor = BackgroundColor,
                devicePixelRatio = DevicePixelRatio,
                chart = Config,
                key = Key,
            }, options);

            string url = $"{Protocol}://{Host}:{Port}/chart";

            HttpResponseMessage response = Client.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json")
            ).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unsuccessful response from API");
            }

            return response.Content.ReadAsByteArrayAsync().Result;
        }

        public void ToFile(string filePath)
        {
            File.WriteAllBytes(filePath, this.ToByteArray());
        }
    }
}
