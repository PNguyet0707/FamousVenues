**# Environment Setup**



1\. Visual Studio 2022: select ASP.NET and web development.

2\. SQL Server \& SSMS 20



**# Project Setup**



**1.Clone the repository:**



&nbsp;  git clone https://github.com/PNguyet0707/FamousVenues.git



**2. Configure Database Connection**



In FamousVenues project: open appsettings.json and update:

"ConnectionStrings": {

"DefaultConnection": "Server=YOUR\_SERVER;Database=YOUR\_DB;User Id=USER;Password=PASSWORD;TrustServerCertificate=True;"

}

For LocalDB:

&nbsp;"ConnectionStrings": {

&nbsp;"DefaultConnection": "Server=(localdb)\\\\mssqllocaldb;Database=YourDbName;Trusted\_Connection=True;"

}



**3. JWT Token Configuration**

* Edit  appsettings.json file in FamousVenues and ServiceLayer projects

&nbsp;    "AppSettings": {

&nbsp;      "Token": "YourKey",

&nbsp;      "Issuer": "YourProject",

&nbsp;      "Audience": "YourUsers",      

&nbsp;     }

* Use PowerShell to run the following command to generate your key for JWT: 

&nbsp;    \[Convert]::ToBase64String((New-Object System.Security.Cryptography.HMACSHA512).Key)



**4.Initial Database**



* Open Package Manager Console (PMC)
* In the Default project dropdown, choose DataLayer
* Run Update-Database in PMC of DataLayer project



**# Running the Project**



* In Visual Studio, set the FamousVenues project as the Startup Project.
* Press F5 or the Run button to launch the application.
