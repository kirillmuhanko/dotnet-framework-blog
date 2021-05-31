# Getting Started

## Front-End

```bash
cd BlogApplication/BlogApplication.Web
npm i
npm run build:dev
```

## Back-End

### Credentials

* Copy: BlogApplication.Web/Configs/Templates/UserSecretsTemplate.xml
* Paste: BlogApplication.Web/Configs/UserSecrets.xml
* Edit: UserSecrets.xml
* Smtp client requires a real email username and password.
    * if you use gmail then you need to enable less secure apps:
        * https://myaccount.google.com/lesssecureapps

### IIS Web Server

Publish BlogApplication to a local folder.

#### Folder permissions

* Right click on published folder;
* Properties | Security | Edit | Add;
* Enter IIS_IUSRS | Ok;
* Allow | Full control = true | Ok.

#### User permissions

* Open SQL Management Studio;
* Security | Logins | New Login;
* General | SQL Server authentication:
    * Enforce password policy = false,
    * Login name = BlogAdmin,
    * Password = Qwerty123.
* Server Roles:
    * public = true,
    * sysadmin = true.

# Blog task

## Architectural requirements

* Front end: HTML5, CSS3, use bootstrap for styling;
* Back end: .NET 4.5 ASP.NET MVC, NuGet, Structure Map, Moq.

## Features

### Login

* User should be able to login or sign up;
* User should be able to change his profile;
* User should be able to change his password;
* User should be able to restore forgotten password.

### Articles

* Administrator can grant permission to write articles;
* Unauthorized user should be able to read articles;
* User should be able to add/edit/delete article if granted;
* Article might have image, title and text. Title is required.

### Article ranking

* Administrator can grant permission to rank articles;
* User should be able to vote "+" or "-" for article if granted.

### Comments

* Administrator can grant permission to write comments;
* Unauthorized user should be able to read comments;
* User should be able to add/edit/delete comment to article if granted.

### Blog summary

* Blog summary page (home page) should provide article search functionality;
* Blog home page should show last 5 articles by default;
* Blog home page should show top 3 articles ordered by rank;
* Blog home page should show 3 last commented articles.

### "Report this" for comments

* User can report complain about a comment;
* Reported comments can be blocked by administrator.