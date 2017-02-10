# Microsoft Azure Exam 70-534 Prep Course
## DocumentDB Demo

### What's in this repository
Here is the code for the DocumentDB demonstration video, of the Microsoft Azure Exam 70-534 prep course. 

You can use this code to replicate that demonstration.

_Important note: Azure services are **not** included as part of your Linux Academy subscription._ To use this code, you must have your own Azure account, and you may be billed for the services you use in Azure. If so, you will be billed for those services directly by Microsoft. Again, Azure services are _not_ included as part of your Linux Academy subscription.

There are two folders:

**Template**: This folder contains two files: _template.json_ is an ARM template that will deploy an Azure DocumentDB account, along with two free Web Apps: One in US East, the other in US West.

**documentdb70534**: This folder contains a ASP.NET MVC application written in C# using .NET Core 1.0. You can use this web application to CRUD DocumentDB records in your DocumentDB account.

To edit and compile this application, you must have the .NET Core 1.0.1 with SDK Preview 2 build 3133 installed on your machine, along with Visual Studio Code or Visual Studio 2015 or later.

Alternatively, you can install this web application on any server that supports NET Core 1.0.1 with SDK Preview 2 build 3133, or by publishing to the Azure Web Apps created as part of the template included in this project.

_Prior to buidling, deploying and using this application, you must change the EndpointURI and PrimaryKey values in HomeController.cs_ to be the correct values for your DocumentDB account. If you do not change these values the web application will not work. 

```
    public class HomeController : Controller
    {
        /* Change these values to match your DocumentDB ednpoint URI and primary access key. */
        private const string EndpointUri = "https://CHANGEME.documents.azure.com:443/";  
        private const string PrimaryKey =
            "YourPrimaryKeyHere==";  

        /* Don't change anything below this line */
        private const string DbName = "StoreDb";
        private const string CollectionName = "ProductCollection";
        private static DocumentClient _client;

        public HomeController()
        {
            //initialize the DocumentClient, which interacts with DocumentDB
            _client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
        }
```