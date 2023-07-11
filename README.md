# Unity Advanced PlayerPrefs

An advanced playerprefs for Unity with additional features, including encryption, extra types, multi-instance PlayerPrefs.

## Introduction

We have all been appreciating the benefits of Unity PlayerPrefs for its convenience, simplicity, and efficiency. However, when it comes to encryption, encapsulation, diversity of your data, simplicity becomes a disadvantage.

This advanced playerprefs intends to fill in the gap with support for encryption, key classes, extra data types, and multi-instance capability.

## Installation

This repository is designed to have the format of Unity Package Manager (UPM). Therefore, you can install this package as a ***Git URL*** from ```Window -> Package Manager```

## Initialization

Here is how you initialize an object of Advanced Player

```csharp
// using directive
using UnityAdvancedPlayerPrefs;

// define an instance of AdvancedPlayerPrefs
AdvancedPlayerPrefs gsm1 = new AdvancedPlayerPrefs(prefix, password);
```

Here's what going to happen with that instance of AdvancedPlayerPrefs

- All the keys used with the instance will be prepended with *prefix* and saved to UnityEngine.PlayerPrefs. For example, *high_score* will be saved as *${prefix}_high_score*. Using a randomly meaningless *prefix* would likely be enough to prevent key collision. This feature is useful in projects where multiple parties need to use the same PlayerPrefs storage for data.
- All data in AdvancedPlayerPrefs will be stored as base64 encoded, for it can be used for multiple types of data rather than the default *int, float, and string*. AdvancedPlayerPrefs is set to support *bool, Color32, double, float, int, long, string*, and more comming in future versions.
- Password will be used to encrypt data stored in PlayerPrefs. If empty or null password is used, data will still be saved as base64 string but not encrypted.

## Usage

The usage of AdvancedPlayerPrefs doesn't differ from the usage of the standard UnityEngine.PlayerPrefs. Some examples can be found in the code below.

```csharp
AdvancedPlayerPrefs prefs = new AdvancedPlayerPrefs("mygameprefs", "secrets");

// set a bool pref
prefs.SetBool("userLoggedIn", true);
// get the bool out with false as fallback value
Debug.Log(prefs.GetBool("userLoggedIn", false));

// set a long value
prefs.SetLong("year2038UnixTime", unixTimeInSeconds);
// get a long value with 0 as fallback value
Debug.Log(prefs.GetLong("year2038UnixTime", 0));
```

## Performance issue

Complex features come with performance downgradation. Using encrypted AdvancedPlayerPrefs takes roughly 2.5 - 3.0 times the execution time to save and get the key. Therefore, if you intend to use AdvancedPlayerPrefs for large amount of data, you should run your own performance test and decide if it's appropriate. 