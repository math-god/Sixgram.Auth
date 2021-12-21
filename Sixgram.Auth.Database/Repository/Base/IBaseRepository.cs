using System;
using System.Threading.Tasks;
using Sixgram.Auth.Common.Base;

namespace Sixgram.Auth.Database.Repository.Base
{
    public interface IBaseRepository<TModel>
        where TModel : BaseModel
    {
        Task<TModel> Create(TModel data);
        Task<TModel> GetById(Guid id);
        TModel GetOne(Func<TModel, bool> predicate);
        Task<TModel> Update(TModel item);
        Task<TModel> Delete(Guid id);
    }
}