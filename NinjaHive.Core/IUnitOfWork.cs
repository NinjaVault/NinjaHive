using System;

namespace NinjaHive.Core
{
    public interface IUnitOfWork<TModel>
        where TModel : class
    {
        TModel GetById(Guid id);
        void Create(TModel model);
        void Update(TModel model);
        void Delete(Guid id);
    }
}
