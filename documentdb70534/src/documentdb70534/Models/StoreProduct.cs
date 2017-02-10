using System;
using Newtonsoft.Json;

namespace documentdb70534.Models
{
    public class StoreProduct
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public bool AgeRestricted { get; set; }
        public DateTime DateFirstOffered { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
