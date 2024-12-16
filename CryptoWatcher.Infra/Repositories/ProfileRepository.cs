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
    public class ProfileRepository : IProfileRepository
    {
        private readonly CryptoWatcherContext context;
        public ProfileRepository(CryptoWatcherContext context)
        {
            this.context = context;
        }
        public bool Delete(Profile entity)
        {
            if (!context.profiles.Contains(entity))
                return false;

            context.profiles.Remove(entity);
            context.SaveChanges();
            return true;
        }

        public Profile Get(int id) =>
            context.profiles.FirstOrDefault(x => x.Id == id);


        public List<Profile> GetAll() =>
            context.profiles.ToList();

        public Profile GetByName(string name) =>
            context.profiles.FirstOrDefault(x => x.ProfileName == name);

        public bool Save(Profile entity)
        {
            if (context.profiles.Any(e => e.ProfileName == entity.ProfileName))
                return false;

            context.profiles.Add(entity);
            return context.SaveChanges() > 0 ? true : false;
        }

        public bool Update(Profile entity)
        {
            context.Entry(entity).CurrentValues.SetValues(entity);
            return context.SaveChanges() > 0;
        }
    }
}
