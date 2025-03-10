﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Management.Application.Persistence.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<bool> Exist(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

    }
}
