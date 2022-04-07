using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sixgram.Auth.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace Sixgram.Auth.Database.Repository.Base
{
    public abstract class BaseRepository<TModel>
        where TModel : BaseModel
    {
        protected readonly AppDbContext Context;

        protected BaseRepository(AppDbContext context)
        {
            Context = context;
        }

        public TModel GetOne(Func<TModel, bool> predicate)
            => Context.Set<TModel>().AsNoTracking().FirstOrDefault(predicate);

        public IEnumerable<TModel> GetRange(Func<TModel, bool> predicate)
            => Context.Set<TModel>().AsNoTracking().Where(predicate);


        public async Task<TModel> Create(TModel item)
        {
            item.DateCreated = DateTime.Now;
            await Context.Set<TModel>().AddAsync(item);
            await Context.SaveChangesAsync();
            return item;
        }

        public async Task<TModel> Update(TModel item)
        {
            item.DateUpdated = DateTime.Now;
            Context.Set<TModel>().Update(item);
            await Context.SaveChangesAsync();
            return item;
        }

        public async Task<TModel> GetById(Guid id)
            => await Context.Set<TModel>().FindAsync(id);


        public async Task<TModel> Delete(Guid id)
        {
            var item = await Context.Set<TModel>().FindAsync(id);
            if (item == null)
                return null;

            Context.Set<TModel>().Remove(item);
            await Context.SaveChangesAsync();
            return item;
        }
    }
}