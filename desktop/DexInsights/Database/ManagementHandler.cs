using DexInsights.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexInsights.Database {
    internal class ManagementHandler {
        public static List<DbUser> GetUsers() {
            string json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Database\\Users.json"));
            JArray usersArray = JArray.Parse(json);
            List<DbUser> usersList = usersArray.ToObject<List<DbUser>>();

            return usersList;
        }

        public static void SaveUsers(List<DbUser> users) {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Database\\Users.json"), json);
        }

        public static List<DbHouse> GetHouses() {
            string json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Database\\Houses.json"));
            JArray housesArray = JArray.Parse(json);
            List<DbHouse> housesList = housesArray.ToObject<List<DbHouse>>();

            return housesList;
        }

        public static void SaveHouses(List<DbHouse> houses) {
            string json = JsonConvert.SerializeObject(houses, Formatting.Indented);
            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Database\\Houses.json"), json);
        }
    }
}
