﻿using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IWriteRepository<T>:IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool>AddRangeAsync(List<T> models);
        bool Update(T model);
        

        Task<bool> RemoveAsync(string id);
        bool Remove(T model);
        bool RemoveRange(List<T> datas);
        Task<int> SaveAsync();
    }
}
