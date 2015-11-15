# VSLiveConnectedApp
Demo application for the Resilient Networking module

The demo app uses a sample WebApi service. The source for the service is included in this repo, but you can also use:

http://vslivesampleservice.azurewebsites.net/api

Please note: Akavache requires SQLite, which is not part of Windows Phone by default. You need to add the Windows Phone SQLite component to your project reference via the Visual Studio SQLite extension from: https://visualstudiogallery.msdn.microsoft.com/cd120b42-30f4-446e-8287-45387a4f40b7

This sample app uses the following Nuget packages:

* [Refit](https://www.nuget.org/packages/refit/)
* [Akavache](https://www.nuget.org/packages/akavache/)
* [ModernHttpClient](https://www.nuget.org/packages/modernhttpclient/)
* [Fusillade](https://www.nuget.org/packages/fusillade/)
* [Connectivity Plugin for Xamarin and Windows](https://www.nuget.org/packages/Xam.Plugin.Connectivity/)

We're standing on the shoulders of giants... Many thanks to [Paul Betts](https://github.com/paulcbetts) and [Rob Gibbens](https://github.com/RobGibbens/ResilientServices) for the inspiration. Be sure to check out their work!
