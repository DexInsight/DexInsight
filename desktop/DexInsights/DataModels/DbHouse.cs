using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexInsights.DataModels {
    public class DbHouse {
        [JsonProperty("id")]
        private int id { get; set; }

        [JsonProperty("date_added")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        private DateTime date_added { get; set; }

        [JsonProperty("date_sold")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        private DateTime date_sold { get; set; }

        [JsonProperty("boughtBy")]
        private List<string> boughtBy { get; set; }

        [JsonProperty("price")]
        private int price { get; set; }

        [JsonProperty("size")]
        private int size { get; set; }

        [JsonProperty("county")]
        private string county { get; set; }

        [JsonProperty("city")]
        private string city { get; set; }

        [JsonProperty("address")]
        private List<string> address { get; set; }

        public DbHouse(int id, DateTime date_added, DateTime date_sold, List<string> boughtBy, int price, int size, string county, string city, List<string> address) {
            this.id = id;
            this.date_added = date_added;
            this.date_sold = date_sold;
            this.boughtBy = boughtBy;
            this.price = price;
            this.size = size;
            this.county = county;
            this.city = city;
            this.address = address;
        }

        public DateTime GetDateAdded() {
            return this.date_added;
        }
        public DateTime GetDateSold() {
            return this.date_sold;
        }

        public List<string> GetBoughtBy() {
            return this.boughtBy;
        }

        public int GetPrice() {
            return this.price;
        }

        public int GetSize() {
            return this.size;
        }

        public string GetCounty() {
            return this.county;
        }

        public string GetCity() {
            return this.city;
        }

        public List<string> GetAddress() {
            return this.address;
        }
        public int GetId() {
            return this.id;
        }
    }
}
