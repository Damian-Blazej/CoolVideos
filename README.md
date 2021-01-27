# CoolVideos
> Web application, based on ASP.NET Core and Vue.js for watching and uploading videos.

## Table of contents
* [Authors](#authors)
* [License](#license)
* [Technologies](#technologies)
* [Setup](#setup)
* [Features](#features)
* [Screenshots](#screenshots)

## Authors
* [Blazej Tempski](https://github.com/jaheyy)
* [Damian Goleniewski](https://github.com/dgoleniewski)

## License
Copyright (c) 2020 Damian Goleniewski, Błażej Tempski, Arkadiusz Wawrzyniak  
This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/ or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.

## Technologies
### API:
* ASP.NET Core 3.1,
* Entity Framework Core 3.1.11,
* Relational Database MySQL

### Client:
* Vue.Js 2.6.11,
* Bootstrap 4.5.3,
* Bootstrap-vue 2.21.2,
* Core-js 3.6.5,
* Vue-resource 1.5.1,
* Vue-router 3.2.0,
* Vue-slide-up-down 2.0.1,
* Vuelidate 0.7.6,
* Vuex 3.4.0

## Setup
To setup a project, you need to clone the repo or download its content and open up the main folder in Microsoft Visual Studio with ASP.NET installed. You will also need to configure JWT Tokens and MySQL Database connection in appsettings.json. Example appsettings.json proper configuration:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "ConnectionStrings": {
    "MySQL": "Server=localhost;Database=test;Uid=sa;Pwd=password;"
  },
  "Jwt": {
    "Key": "someKey",
    "Issuer": "Server",
    "Audience": "Client",
    "Subject": "AccessToken"
  },
  "AllowedHosts": "*"
}
```
Next step is install node modules using npm install command in /clientapp. 

## Features
List of ready features
* Authentication (JWT Tokens and Vuex)
* Searching videos
* Watching videos
* CRUD for videos (Onwer allowed only)
* Liking/unliking video
* Error handling
* Frontend form validations

## Screenshots
<img src="/sc/1.png" width="600"/>
<img src="/sc/2.png" width="600"/>
<img src="/sc/3.png" width="600"/>
<img src="/sc/4.png" width="600"/>
<img src="/sc/5.png" width="600"/>
<img src="/sc/6.png" width="600"/>
<img src="/sc/7.png" width="600"/>
<img src="/sc/8.png" width="600"/>
