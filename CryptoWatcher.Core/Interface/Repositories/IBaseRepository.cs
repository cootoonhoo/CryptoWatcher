using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWatcher.Core.Interface.Repositories
{
    public interface IBaseRepository<T>
    {
        public bool Delete(T entity);
        public T Get(int id);
        public List<T> GetAll();
        public bool Save(T entity);
        public bool Update(T entity);
    }
}
