using Microsoft.VisualStudio.TestTools.UnitTesting;
using Firebase.Database.Query;
using Firebase.Database.Offline;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Firebase.Database.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void Run()
        {
            var client = new FirebaseClient("https://settle-up-sandbox.firebaseio.com/");

            var usersDb = client
                .Child("users")
                .AsRealtimeDatabase<User>(streamChanges: false, initialPullStrategy: InitialPullStrategy.None);

            usersDb.Pull("dEic9IfsDtXzXGca7R410gLLAZd2");

            usersDb.AsObservable().Subscribe();

            var db = client.Child("users").AsRealtimeDatabase<User>(streamChanges: false, initialPullStrategy: InitialPullStrategy.None);

            //db.PutHandler = new UsersPutHandler();
            db.Put("kuDd8V8UQKZAq3PLegFDP3NH1hI2", u => u.ActiveGroupId, "-efgh");

            //db.Put("kuDd8V8UQKZAq3PLegFDP3NH1hI2", new User { ActiveGroupId = "-KgxhQrcJuaO1JDoOdno" });

            //child.Patch("changed_entity_key", cn => cn.Number.current_numbers["34"], "22", true, 1);

                //child.Pull("changed_entity_key"); 

                //child.Put("changed_entity_key", new Data_Structures { Number = new Current_Number { current_numbers = new Dictionary<string, int?>() { ["33"] = 22 } } }, true, 2);

                //child.Delete("changed_entity_key", cn => cn.Number.current_number, true, 1);

            while (true) { }
        }
    }

    public class Data_Structures
    {
        public Current_Number Number { get; set; }
    }

    public class Current_Number
    {
        public Current_Number()
        {
            this.random = "test 4";
        }

        public Dictionary<string, int> current_numbers { get; set; }

        public string random { get; set; }
    }

    public class Dummy
    {


        public string B { get; set; }
    }
    public class User
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the id of this user's active group.
        /// </summary>
        //[JsonProperty("currentTabId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ActiveGroupId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        [JsonProperty("email")]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Path to the picture of the user.
        /// </summary>
        [JsonProperty("photoUrl")]
        public string PictureUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of authentication provider this user chose to login. 
        /// </summary>
        [JsonProperty("authProvider")]
        public string AuthProviderType
        {
            get;
            set;
        }
    }

    public class UsersPutHandler : ISetHandler<User>
    {
        public Task SetAsync(ChildQuery query, string key, OfflineEntry entry)
        {
            var user = entry.Deserialize<User>();

            return query.Child(key).Child("currentTabId").PutAsync<string>(user.ActiveGroupId);
        }
    }
}
