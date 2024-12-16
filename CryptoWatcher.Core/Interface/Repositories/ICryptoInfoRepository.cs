using CryptoWatcher.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWatcher.Core.Interface.Repositories
{
    public interface ICryptoInfoRepository : IBaseRepository<CryptoInfo>
    {
        public CryptoInfo GetBySymbol(string symbol);
    }
}
