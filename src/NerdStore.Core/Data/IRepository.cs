﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Core.Data
{
    public interface IRepository<T> : IDisposable where T: IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }


    }

    

}
