﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Sixgram.Auth.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace Sixgram.Auth.Database.Repository.Base
{
    public abstract class BaseRepository<TModel>
        where TModel : BaseModel
    {
        private readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public TModel GetOne (Func<TModel, bool> predicate)
            => _context.Set<TModel>().AsNoTracking().FirstOrDefault(predicate);
        

        public async Task<TModel> Create (TModel item)
        {
            item.DateCreated = DateTime.Now;
            await _context.Set<TModel>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<TModel> Update (TModel item)
        {
            item.DateUpdated = DateTime.Now;
            _context.Set<TModel>().Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<TModel> GetById(Guid id)
            => await _context.Set<TModel>().FindAsync(id);
        

        public async Task<TModel> Delete(Guid id)
        {
            var item = await _context.Set<TModel>().FindAsync(id);
            if (item == null)
                return null;

            _context.Set<TModel>().Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}