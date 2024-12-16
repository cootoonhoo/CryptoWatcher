using CryptoWatcher.Core.Entities;
using CryptoWatcher.Core.Interface.Repositories;
using CryptoWatcher.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWatcher.Infra.Repositories
{
    public class CryptoInfoRepository : ICryptoInfoRepository
    {
        private readonly CryptoWatcherContext context;
        public CryptoInfoRepository(CryptoWatcherContext context)
        {
            this.context = context;
        }
        public bool Delete(CryptoInfo entity)
        {
            if(!context.cryptosInfo.Contains(entity))
                return false;

            context.cryptosInfo.Remove(entity);
            context.SaveChanges();
            return true;
        }

        public CryptoInfo GetBySymbol(string symbol) =>
            context.cryptosInfo.FirstOrDefault(x => x.Symbol == symbol);

        public CryptoInfo Get(int id) =>
            context.cryptosInfo.FirstOrDefault(x => x.Id == id);


        public List<CryptoInfo> GetAll() =>
            context.cryptosInfo.ToList();

        public bool Save(CryptoInfo entity)
        {
            if (context.cryptosInfo.Any(e => e.Symbol == entity.Symbol))
                return false;

            context.cryptosInfo.Add(entity);
            return context.SaveChanges() > 0 ? true : false;
        }

        public bool Update(CryptoInfo entity)
        {
            context.Entry(entity).CurrentValues.SetValues(entity);
            return context.SaveChanges() > 0;
        }
    }
}
