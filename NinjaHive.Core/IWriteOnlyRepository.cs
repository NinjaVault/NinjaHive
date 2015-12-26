using System;

namespace NinjaHive.Core
{
    public interface IWriteOnlyRepository<TModel>
        where TModel : class
    {
        void Create(TModel model);
        void Update(TModel model);
        void Delete(Guid id);
    }
}
