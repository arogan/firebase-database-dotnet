using Firebase.Database;
using Firebase.Database.Offline;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new FirebaseClient("https://settle-up-sandbox.firebaseio.com/");

            var usersDb = client
                .Child("users")
                .AsRealtimeDatabase<User>(streamChanges: false, initialPullStrategy: InitialPullStrategy.None);

            usersDb.Pull("dEic9IfsDtXzXGca7R410gLLAZd2");

            usersDb.AsObservable().Subscribe();


            while (true) { }
        }
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
        [JsonConverter(typeof(StringEnumConverter))]
        public FirebaseAuthType AuthProviderType
        {
            get;
            set;
        }
    }

    public enum FirebaseAuthType
    {
        /// <summary>
        /// The facebook auth.
        /// </summary>
        [EnumMember(Value = "facebook.com")]
        Facebook,

        /// <summary>
        /// The google auth.
        /// </summary>
        [EnumMember(Value = "google.com")]
        Google,

        /// <summary>
        /// The github auth.
        /// </summary>
        [EnumMember(Value = "github.com")]
        Github,

        /// <summary>
        /// The twitter auth. 
        /// </summary> 
        [EnumMember(Value = "twitter.com")]
        Twitter,

        /// <summary>
        /// Auth using email and password.
        /// </summary>
        [EnumMember(Value = "email")]
        EmailAndPassword
    }
}