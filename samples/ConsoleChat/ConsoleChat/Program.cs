namespace Firebase.ConsoleChat
{
    using System;
    using System.Threading.Tasks;

    using Firebase.Database;
    using Firebase.Database.Extensions;
    using System.Reactive.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Database.Offline;
    using Database.Query;
    using System.Linq;
    using Firebase.Database.Streaming;

    public class Program
    {
        public static void Main(string[] args)
        {
            Run().Wait();
        }


        private static IObservable<FirebaseEvent<Permission>> Get(RealtimeDatabase<Permission> db)
        {
            return db
                .AsObservable()
                .Where(g => !string.IsNullOrEmpty(g.Key))
                .Catch<FirebaseEvent<Permission>, Exception>(e => Get(db).DelaySubscription(TimeSpan.FromSeconds(2)));
        }

        private static async Task Run()
        {
            //Console.WriteLine("How many items should be deleted?");

            //var count = int.Parse(Console.ReadLine());

            Console.WriteLine("*******************************************************");
            var client = new FirebaseClient(
              "https://settle-up-sandbox.firebaseio.com/",
              new FirebaseOptions
              {
                  SyncPeriod = TimeSpan.FromSeconds(1),
                  AuthTokenAsyncFactory = () => Task.FromResult("zxrXYieD4doI9nRtIbIY85NydClHAc0XrU4h7xUZ")
              });

            while (true)
            {
                Console.WriteLine("Enter counter: ");
                var counter = Console.ReadLine();

                var child = client
                            .Child("serverTasks")
                            .Child("migrateLegacyGroups")
                            .Child("yoyoyo");

                await child.DeleteAsync();
                await child.Child("request").PutAsync(new
                {
                    uid = "STSiOTXGpwPjbPyIn3299VAX6792",
                    appengineId = "latest",
                    pendingChanges = new[]
                    {
                        new
                        {
                            offline = true,
                            appengineId = "test-id" + counter,
                            groupData = new
                            {
                                name = "Test sync" + counter,
                                currency = new
                                {
                                    code = "USD",
                                    symbol = "$"
                                }
                            },
                            changes = new
                            {
                                last_sync = 0,
                                inserts = new []
                                {
                                    new
                                    {
                                        client_time = 1495454765376,
                                        data_table = "members",
                                        id = "member_id_" + counter,
                                        data = "{\"group_shared\":false,\"email\":\"bezytom@gmail.com\",\"shared_to_email\":\"bezytom@gmail.com\",\"name\":\"A\",\"bank_account\":\"\",\"people\":1.0,\"disabled\":false}"
                                    },
                                    new
                                    {
                                        client_time = 1495454765375,
                                        data_table = "members",
                                        id = "member_id_2" + counter,
                                        data = "{\"group_shared\":false,\"name\":\"B\",\"bank_account\":\"\",\"people\":1.0,\"disabled\":false}"
                                    },
                                    new
                                    {
                                        client_time = 1495454765375,
                                        data_table = "members",
                                        id = "member_id_3" + counter,
                                        data = "{\"group_shared\":false,\"name\":\"C\",\"bank_account\":\"\",\"people\":1.0,\"disabled\":false}"
                                    },
                                    new
                                    {
                                        client_time = 1495454765375,
                                        data_table = "payments",
                                        id = "payment_id" + counter,
                                        data = $"{{\"amount\":\"22\",\"transfer\":false,\"who_paid\":\"member_id_{counter}\",\"created\":1495485176195,\"for_who\":\"member_id_{counter} member_id_2{counter}\",\"purpose\":\"Android\",\"currency\":\"USD\"}}"
                                    }
                                },
                                deletes = new [] 
                                {
                                    new
                                    {
                                        client_time = 1495454765375,
                                        data_table = "members",
                                        id = "member_id_3" + counter,
                                        data = ""
                                    }
                                }
                            }
                        }
                    }
                });



                Console.WriteLine("Done");

            }








            //await client.Child("serverTasks").DeleteAsync();
            //await client.Child("serverTasksInternal").DeleteAsync();
            //await client.Child("groups").DeleteAsync();
            //await client.Child("members").DeleteAsync();
            //await client.Child("transactions").DeleteAsync();
            //await client.Child("changes").DeleteAsync();
            //await client.Child("permissions").DeleteAsync();
            //await client.Child("users").DeleteAsync();
            //await client.Child("userGroups").DeleteAsync();
            //await client.Child("pushRegistrations").DeleteAsync();
            //await client.Child("subscriptions").DeleteAsync();

            //await client.Child("serverTasks/deleteGroup/abcd").DeleteAsync();
            //await client.Child("serverTasks/deleteGroup/abcd/request/groupId").PutAsync("--d9923476981f1715b0e3");

            //client.Child("serverTasks/deleteTransactions/abcd/response")

            //var ug = await client.Child("groups").OrderByKey().LimitToFirst(count).BuildUrlAsync();

            //var g = await client.Child("groups").OrderByKey().LimitToFirst(count).OnceAsync<Group>();
            //var keys = g.Select(fb => fb.Key).ToList();

            //foreach (var key in keys)
            //{
            //    Console.WriteLine("Deleting " + key);

            //    await Task.WhenAll(
            //        client.Child("groups").Child(key).DeleteAsync(),
            //        client.Child("members").Child(key).DeleteAsync(),
            //        client.Child("transactions").Child(key).DeleteAsync(),
            //        client.Child("permissions").Child(key).DeleteAsync());

            //    await Task.Delay(1000);
            //    await client.Child("changes").Child(key).DeleteAsync();
            //}

            Console.WriteLine("Finished!");
            //var db = client
            //    .Child("exchangeRatesToUsd/latest")
            //    .AsRealtimeDatabase<Dictionary<string, double>>(filenameModifier: "fxRates", elementRoot: "latest", streamChanges: false, initialPullStrategy: InitialPullStrategy.Everything)
            //    ;
            //db.AsObservable()
            //    .Subscribe(fb => Console.Write(JsonConvert.SerializeObject(fb)));
            //db.SyncExceptionThrown += (s, e) => Console.WriteLine(e.Exception);

            //while (true) { }
            //var res = await client.Child("serverTasks/currencyChange").PostAsync(new
            //{
            //    request = new
            //    {
            //        groupId = "-Kaw29nnuYhX2gMY2Kkk",
            //        targetCurrency = "CZK"
            //    }
            //});

            //Console.WriteLine(res.Key);

            /*
            var child = client.Child("messages");
            
            var observable = child.AsObservable<InboundMessage>();
            
            // delete entire conversation list
            await child.DeleteAsync();

            // subscribe to messages comming in, ignoring the ones that are from me
            var subscription = observable
                .Where(f => !string.IsNullOrEmpty(f.Key)) // you get empty Key when there are no data on the server for specified node
                .Where(f => f.Object?.Author != name)
                .Subscribe(f => Console.WriteLine($"{f.Object.Author}: {f.Object.Content}"));

            while (true)
            {
                var message = Console.ReadLine();

                if (message?.ToLower() == "q")
                {
                    break;
                }

                await child.PostAsync(new OutboundMessage { Author = name, Content = message });
            }

            subscription.Dispose();*/
        }
    }

    internal class Permission
    {
        /// <summary>
        /// Gets or sets the permission level.
        /// </summary>
        [JsonProperty("level")]
        public string PermissionLevel
        {
            get;
            set;
        }
    }

    class Group
    {
        public string Name { get; set; }
    }

    class Change
    {
        public string EntityName { get; set; }
    }

    class User
    {
        public string currentTabId { get; set; }
    }
}