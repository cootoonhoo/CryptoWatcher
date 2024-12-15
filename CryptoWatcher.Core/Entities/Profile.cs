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
        [JsonIgnore]
        public int Id { get; set; }
        public string ProfileName { get; set; }
        public ICollection<CryptoInfo>? Cryptos { get; set; }
    }
}
