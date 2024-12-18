﻿using CryptoWatcher.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWatcher.Core.Interface.Services
{
    public interface ICryptoInfoJobService : IBaseJobService<CryptoInfo>
    {
        public Task UpdateCryptoCurrencyHourly();
    }
}
