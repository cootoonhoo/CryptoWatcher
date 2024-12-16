using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoWatcher.Core.Entities
{
    public class Profile
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonPropertyName("profileName")]
        public string ProfileName { get; set; }
        [JsonPropertyName("cryptos")]
        public ICollection<CryptoInfo>? Cryptos { get; set; }
    }
}
