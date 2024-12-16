using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace CryptoWatcher.Core.Entities
{
    public class CryptoInfo
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]

        public int Id { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonIgnore]
        public ICollection<Profile>? Profiles { get; set; }
    }
}
