namespace Firebase.Database.Tests
{
    using Firebase.Database.Tests.Entities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Threading.Tasks;

    using Firebase.Database.Query;
    using Firebase.Database.Offline;
    using System;
    using Streaming;

    [TestClass]
    public class OfflineTests
    {
        public const string BasePath = "http://base.path.net";

        [TestMethod]
        public void Test()
        {
            var client = new FirebaseClient("https://settle-up-sandbox.firebaseio.com/", new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult("eyJhbGciOiJSUzI1NiIsImtpZCI6ImRiOWRiMmIwNmY4YjU0ZjZiNjRhYTM3ZjY2ODVmMzViYTRlODY1ODQifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vc2V0dGxlLXVwLXNhbmRib3giLCJuYW1lIjoiVG9tw6HFoSBCZXp5IEJlem91xaFrYSIsInBpY3R1cmUiOiJodHRwczovL3Njb250ZW50Lnh4LmZiY2RuLm5ldC92L3QxLjAtMS9wMTAweDEwMC8xMjIwODY1MV8xMDE1MzI3NDY1MjkwMTY5M183NjQzMDY4MDQ1OTY3MjExMDU1X24uanBnP29oPWY1MTg4YzcxMmZiMjkyMjg2YmI4MTA3MTYwNDM1YzZhJm9lPTU4MzBEMDIxIiwiYXVkIjoic2V0dGxlLXVwLXNhbmRib3giLCJhdXRoX3RpbWUiOjE0ODM4ODA2NDcsInVzZXJfaWQiOiJrdURkOFY4VVFLWkFxM1BMZWdGRFAzTkgxaEkyIiwic3ViIjoia3VEZDhWOFVRS1pBcTNQTGVnRkRQM05IMWhJMiIsImlhdCI6MTQ4Mzg4MDY0NywiZXhwIjoxNDgzODg0MjQ3LCJlbWFpbCI6ImIuZS56LnlAY2VudHJ1bS5jeiIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6eyJmYWNlYm9vay5jb20iOlsiNTM5ODAxNjkyIl0sImVtYWlsIjpbImIuZS56LnlAY2VudHJ1bS5jeiJdfSwic2lnbl9pbl9wcm92aWRlciI6ImZhY2Vib29rLmNvbSJ9fQ.nK5y93oZ5IdMVzjeBqnsftpqOy3gajFADbzB2U3cZLHd8SdaXdGa5j27ehNOJuuntQvkeyLaqVlO5A0iwNovZjv-I0gMxor1-kDtkoqug3gkDDragFlkR9UEszyxAXMn6UITWbdVY4Ub8x301gnrHh_U7eHvqiDWHSeXe6XSXFCYsd_ox4ivh5Sk5Mn5sRE1Cv3bpGhCwtPvTCcrfgWbrwcQfFIpi4hG5fTz5Upqqh7doEm-0YBaB9SqJE2WZvlbOMqQwttoxJ1Ys7OFmI8aMVu84sBo06RqxX_Dim0mxrNCU_ii-rL2Y918Nip4PRiqHWd7sIJ93_hOBcIEOYiNWA")
            });

            var changesDb = client
                .Child("changes")
                .Child("group_id_5")
                .AsRealtimeDatabase<Change>("group_id_5")
                .AsObservable()
                .Subscribe(new Observer<FirebaseEvent<Change>>());

            while (true) { }
        }
    }

    public class Observer<T> : IObserver<T>
    {
        public void OnCompleted()
        {
         }

        public void OnError(Exception error)
        {
         }

        public void OnNext(T value)
        {
        }
    }

    public class Change
    {
        /// <summary>
        /// Gets or sets the type of the action which created this change. 
        /// </summary>
        [JsonProperty("action")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ChangeType ActionType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of entity which changed.
        /// </summary>
        [JsonProperty("entity")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityType EntityType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the id of the entity which changed.
        /// </summary>
        [JsonProperty("entityId")]
        public string EntityId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the server timestamp when the change occurred.
        /// </summary>
        public long ServerTimestamp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets id of the user who was behind this change.
        /// </summary>
        [JsonProperty("by")]
        public string AuthorId
        {
            get;
            set;
        }
    }
    public enum EntityType
    {
        /// <summary>
        /// Refers to <see cref="Transaction"/>.
        /// </summary>
        Transaction,

        /// <summary>
        /// Refers to <see cref="User"/>
        /// </summary>
        Permission,

        /// <summary>
        /// Refers to <see cref="Member"/>.
        /// </summary>
        Member,

        /// <summary>
        /// Refers to <see cref="Group"/>.
        /// </summary>
        Group,

        /// <summary>
        /// Refers to <see cref="Change"/>.
        /// </summary>
        Change
    }
    public enum ChangeType
    {
        /// <summary>
        /// New item was inserted.
        /// </summary>
        Insert = 1,

        /// <summary>
        /// Existing item was updated.
        /// </summary>
        Update = 2,

        /// <summary>
        /// Existing item was deleted.
        /// </summary>
        Delete = 3,

        /// <summary>
        /// Convert to currency changed.
        /// </summary>
        CurrencyChange = 4
    }
}
