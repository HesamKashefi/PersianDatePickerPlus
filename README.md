<p align="center">
  <strong>A Persian date picker for .NET WPF (.NET Core)</strong>
</p>

## Build Status

[![build](https://github.com/HesamKashefi/WPF_PersianDatePicker/actions/workflows/build.yml/badge.svg)](https://github.com/HesamKashefi/WPF_PersianDatePicker/actions/workflows/build.yml)

## NuGet

<p>
  <a href="http://nuget.org/List/Packages/PersianDateControlsPlus">
  <img alt="ManyConsole" src="https://img.shields.io/nuget/v/PersianDateControlsPlus.svg">
  </a>

## How to use

First, install this package:

```ps
Install-Package PersianDateControlsPlus
```

Add this code at the top of your App.xaml:
```xaml
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="pack://application:,,,/PersianDateControlsPlus;component/Themes/PersianDateControlsPlus.Defaults.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
```


Then add this code at the top of your Xaml:

```xaml
xmlns:PersianDateControlsPlus="clr-namespace:PersianDateControlsPlus;assembly=PersianDateControlsPlus"
```

then paste

```xaml
<PersianDateControlsPlus:PersianDatePicker />
```

Whereever you wanted to use persian calender.

A sample project is present [Here](https://github.com/HesamKashefi/WPF_PersianDatePicker/tree/master/src/SampleProject)

![image](https://user-images.githubusercontent.com/22152065/60768601-01cced00-a0db-11e9-9a40-9affe9a160d5.png)

### About Perisan calendar

The Persian calendar is a sonar calendar, like Gregorian calendar, but there are some differences. One is that the origins are different, and the Persian calendar's origin is about 621 years after Gregorian calendar's; another one is that Persian calendar's first day of year is March 21; and probably the most important one is that the average length of a Persian calendar year is different from that of a Gregorian calendar: the Persian calendar has 8 leap years (that is a year that has an extra day than normal years) in each 33 years, whereas the Gregorian calendar has 8 leap years in each 32 years. This little difference means that Persian dates cannot be calculated directly from Gregorian dates.

[Additional documentation](https://www.codeproject.com/Articles/43521/PersianDate-and-Some-WPF-Controls-For-It)
[Persian documentation](https://www.dotnettips.info/newsarchive/details/10951)

## Thanks to

- Mohsens22 and Arash Sahebolamri, for making this library at first place.
