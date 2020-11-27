# quickchart-csharp

[![Build Status](https://travis-ci.com/typpo/quickchart-csharp.svg?branch=main)](https://travis-ci.com/typpo/quickchart-csharp)
[![Nuget](http://img.shields.io/nuget/v/QuickChart.svg?style=flat)](https://www.nuget.org/packages/QuickChart)

A C# client for the [quickchart.io](https://quickchart.io/) chart API.

## Installation

Use `QuickChart/QuickChart.cs` in this project, or install the `QuickChart` package:

```
PM> Install-Package QuickChart -Version 1.0.0
```
or
```
dotnet add package QuickChart --version 1.0.0
```
---
## Usage

This library provides a `QuickChart` namespace containing a `Chart` class.  Import and instantiate it.  Then set properties on it and specify a [Chart.js](https://chartjs.org) config:

```csharp
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
```

Use `GetUrl()` on your QuickChart object to get the encoded URL that renders your chart:

```csharp
Console.WriteLine(qc.GetUrl());
// https://quickchart.io/chart?c=%7B%22chart%22%3A+%7B%22type%22%3A+%22bar%22%2C+%22data%22%3A+%7B%22labels%22%3A+%5B%22Hello+world%22%2C+%22Test%22%5D%2C+%22datasets%22%3A+%5B%7B%22label%22%3A+%22Foo%22%2C+%22data%22%3A+%5B1%2C+2%5D%7D%5D%7D%7D%7D&w=600&h=300&bkg=%23ffffff&devicePixelRatio=2.0&f=png
```

If you have a long or complicated chart, use `GetShortUrl()` to get a fixed-length URL using the quickchart.io web service (note that these URLs only persist for a short time unless you have a subscription):

```csharp
Console.WriteLine(qc.GetShortUrl());
# https://quickchart.io/chart/render/f-a1d3e804-dfea-442c-88b0-9801b9808401
```

The URLs will render an image of a chart:

<img src="https://quickchart.io/chart?c=%7B%22type%22%3A+%22bar%22%2C+%22data%22%3A+%7B%22labels%22%3A+%5B%22Hello+world%22%2C+%22Test%22%5D%2C+%22datasets%22%3A+%5B%7B%22label%22%3A+%22Foo%22%2C+%22data%22%3A+%5B1%2C+2%5D%7D%5D%7D%7D&w=600&h=300&bkg=%23ffffff&devicePixelRatio=2.0&f=png" width="500" />

---

### Customizing your chart

You can set the following properties:

#### Config: string
The actual Chart.js chart configuration.

#### Width: int
Width of the chart image in pixels.  Defaults to 500

#### Height: int
Height of the chart image  in pixels.  Defaults to 300

#### BackgroundColor: string
The background color of the chart. Any valid HTML color works. Defaults to #ffffff (white). Also takes rgb, rgba, and hsl values.

#### DevicePixelRatio: double
The device pixel ratio of the chart. This will multiply the number of pixels by the value. This is usually used for retina displays. Defaults to 1.0.

#### Key: string
API key (not required)

---

### Creating chart URLs

There are a few ways to get a URL for your chart object.

#### GetUrl(): string

Returns a URL that will display the chart image when loaded.

#### GetShortUrl(): string

Uses the quickchart.io web service to create a fixed-length chart URL that displays the chart image.  Returns a URL such as `https://quickchart.io/chart/render/f-a1d3e804-dfea-442c-88b0-9801b9808401`.

Note that short URLs expire after a few days for users of the free service.  You can [subscribe](https://quickchart.io/pricing/) to keep them around longer.

---

#### Other methods

#### ToFile(string path)

Write your chart to file.

#### ToByteArray(): byte[]

Returns an array of bytes representing your image.

## More examples

Checkout the `QuickChartExamples` project to see other usage.
