using System;
using QuickChart;

namespace QuickChartExamples
{
    public class ShortUrlExample
    {
        static void Main(string[] args) {
            Console.WriteLine("Fetching short url...");
            Chart qc = new Chart();

            qc.Width = 500;
            qc.Height = 300;
            qc.Config = @"{
                type: 'bar',
                data: {
                    labels: ['Q1', 'Q2', 'Q3', 'Q4'],
                    datasets: [{
                    label: 'Users',
                    data: [50, 60, 70, 180]
                    }]
                }
            }";

            Console.WriteLine(qc.GetShortUrl());
        }
    }
}