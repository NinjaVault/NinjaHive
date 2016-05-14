using System;
using NinjaHive.Core.Models;

namespace NinjaHive.Core
{
    public interface IUnitOfWork<TModel>
        where TModel : class
    {
        TModel GetById(Guid id);
        WorkResult Create(TModel model);
        WorkResult Update(TModel model);
        WorkResult Delete(Guid id);
    }
}
