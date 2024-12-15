﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWatcher.Core.Interface.Services
{
    public interface IBaseJobService<T>
    {
        public void UpsertJob (T entity);
    }
}
