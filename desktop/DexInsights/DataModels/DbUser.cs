using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexInsights.DataModels {
    public class DbUser {
        [JsonProperty("name")]
        private string name { get; set; }

        [JsonProperty("role")]
        private string role { get; set; }

        [JsonProperty("date")]
        private DateTime date { get; set; }
        public DbUser(string name, string role, DateTime date) {
            this.name = name;
            this.role = role;
            this.date = date;
        }

        public string GetName() {
            return this.name;
        }
        public string GetRole() {
            return this.role;
        }

        public DateTime GetDate() {
            return this.date;
        }
    }
}
