using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class CategoryCommandHandler :
        ICommandHandler<CreateEntityCommand<CategoryModel>>,
        ICommandHandler<UpdateEntityCommand<CategoryModel>>,
        ICommandHandler<DeleteEntityCommand<CategoryModel>>
    {
        private readonly IRepository<CategoryEntity> repository;

        public CategoryCommandHandler(IRepository<CategoryEntity> repository)
        {
            this.repository = repository;
        }

        public void Handle(CreateEntityCommand<CategoryModel> command)
        {
            var entity = new CategoryEntity
            {
                Name = command.Model.Name,
            };

            this.repository.Add(entity);
        }

        public void Handle(UpdateEntityCommand<CategoryModel> command)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(DeleteEntityCommand<CategoryModel> command)
        {
            throw new System.NotImplementedException();
        }
    }
}
