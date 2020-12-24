using System;
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

        public string Scheme { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        private static readonly HttpClient Client = new HttpClient();

        public Chart()
        {
            Width = 500;
            Height = 300;
            DevicePixelRatio = 1.0;
            BackgroundColor = "transparent";

            Scheme = "https";
            Host = "quickchart.io";
            Port = 443;
        }

        public string GetUrl()
        {
            if (Config == null)
            {
                throw new NullReferenceException("You must set Config on the QuickChart object before generating a URL");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("w=").Append(Width.ToString());
            builder.Append("&h=").Append(Height.ToString());

            builder.Append("&devicePixelRatio=").Append(DevicePixelRatio.ToString());
            builder.Append("&bkg=").Append(Uri.EscapeDataString(BackgroundColor));
            builder.Append("&c=").Append(Uri.EscapeDataString(Config));
            if (!string.IsNullOrEmpty(Key))
            {
                builder.Append("&key=").Append(Uri.EscapeDataString(Key));
            }

            return $"{Scheme}://{Host}:{Port}/chart?{builder}";
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

            string url = $"{Scheme}://{Host}:{Port}/chart/create";

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

            string url = $"{Scheme}://{Host}:{Port}/chart";

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
