﻿using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<T>(FastFoodContext context) : IRepository<T> where T : class, IAggregatedRoot 
    {
        public virtual void Add(T entity) => context.Add(entity);

        public virtual void Delete(T entity) => context.Remove(entity);

        public virtual T? GetById(Guid id) => context.Find<T>(id);       

        public virtual void Update(T entity) => context.Entry(entity).State = EntityState.Modified;

    }
}
