using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using documentdb70534.Models;

namespace documentdb70534.Controllers
{
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

        public async Task<IActionResult> Index()
        {
            //create database and collection if they don't exist
            await _client.CreateDatabaseIfNotExistsAsync(new Database {Id = DbName});
            await _client.CreateDocumentCollectionIfNotExistsAsync($"/dbs/{DbName}",
                new DocumentCollection {Id = CollectionName});

            //get and display a list of existing DocumentDB documents
            var queryOptions = new FeedOptions {MaxItemCount = -1};
            var list =
                _client.CreateDocumentQuery<StoreProduct>(
                    UriFactory.CreateDocumentCollectionUri(DbName, CollectionName), queryOptions).ToList();

            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new StoreProduct();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StoreProduct model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DbName, CollectionName), model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var query = await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DbName, CollectionName, id));
            var model = (StoreProduct)(dynamic)query.Resource;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StoreProduct model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DbName, CollectionName, model.Id), model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DbName, CollectionName, id));
            }
            catch (DocumentClientException) { }
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
