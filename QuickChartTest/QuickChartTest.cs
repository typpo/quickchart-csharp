using System;
using Xunit;

using QuickChart;

namespace QuickChartTest
{
    public class QuickChartTest
    {
        [Fact]
        public void TestBasic()
        {
            Chart qc = new Chart
            {
                Width = 500,
                Height = 300,
                Config = @"{
                type: 'bar',
                data: {
                    labels: ['Q1', 'Q2', 'Q3', 'Q4'],
                    datasets: [{
                        label: 'Users',
                        data: [50, 60, 70, 180]
                    }]
                }
            }"
            };

            string url = qc.GetUrl();
            Assert.Contains("https://quickchart.io:443/chart", url);
            Assert.Contains("w=500", url);
            Assert.Contains("h=300", url);
            Assert.DoesNotContain("key=", url);
        }

        [Fact]
        public void TestWithKey()
        {
            Chart qc = new Chart
            {
                Key = "abc123",
                Width = 500,
                Height = 300,
                Config = @"{
                type: 'bar',
                data: {
                    labels: ['Q1', 'Q2', 'Q3', 'Q4'],
                    datasets: [{
                        label: 'Users',
                        data: [50, 60, 70, 180]
                    }]
                }
            }"
            };

            string url = qc.GetUrl();
            Assert.Contains("https://quickchart.io:443/chart", url);
            Assert.Contains("w=500", url);
            Assert.Contains("h=300", url);
            Assert.Contains("key=abc123", url);
        }

        [Fact]
        public void TestWithFormat()
        {
            Chart qc = new Chart
            {
                Width = 500,
                Height = 300,
                Format = "svg",
                Config = @"{
                type: 'bar',
                data: {
                    labels: ['Q1', 'Q2', 'Q3', 'Q4'],
                    datasets: [{
                        label: 'Users',
                        data: [50, 60, 70, 180]
                    }]
                }
            }"
            };

            string url = qc.GetUrl();
            Assert.Contains("https://quickchart.io:443/chart", url);
            Assert.Contains("w=500", url);
            Assert.Contains("h=300", url);
            Assert.Contains("f=svg", url);
        }
    }
}
