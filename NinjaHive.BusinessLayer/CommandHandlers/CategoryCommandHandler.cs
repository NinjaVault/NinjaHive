using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class CategoryCommandHandler :
        ICommandHandler<CreateEntityCommand<MainCategoryModel>>,
        ICommandHandler<UpdateEntityCommand<MainCategoryModel>>,
        ICommandHandler<DeleteEntityCommand<MainCategoryModel>>,
        ICommandHandler<CreateEntityCommand<SubCategoryModel>>,
        ICommandHandler<UpdateEntityCommand<SubCategoryModel>>,
        ICommandHandler<DeleteEntityCommand<SubCategoryModel>>
    {
        private readonly IRepository<MainCategoryEntity> mainCategoryRepository;
        private readonly IRepository<SubCategoryEntity> subCategoryRepository;

        public CategoryCommandHandler(
            IRepository<MainCategoryEntity> mainCategoryRepository,
            IRepository<SubCategoryEntity> subCategoryRepository)
        {
            this.mainCategoryRepository = mainCategoryRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public void Handle(CreateEntityCommand<MainCategoryModel> command)
        {
            var entity = new MainCategoryEntity
            {
                Name = command.Model.Name,
            };

            this.mainCategoryRepository.Add(entity);
        }

        public void Handle(UpdateEntityCommand<MainCategoryModel> command)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(DeleteEntityCommand<MainCategoryModel> command)
        {
            this.mainCategoryRepository.RemoveById(command.Id);
        }

        public void Handle(CreateEntityCommand<SubCategoryModel> command)
        {
            var mainCategory = this.mainCategoryRepository.FindById(command.Model.MainCategoryId);
            var entity = new SubCategoryEntity
            {
                Name = command.Model.Name,
                MainCategory = mainCategory,
            };

            this.subCategoryRepository.Add(entity);
        }

        public void Handle(UpdateEntityCommand<SubCategoryModel> command)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(DeleteEntityCommand<SubCategoryModel> command)
        {
            this.subCategoryRepository.RemoveById(command.Id);
        }
    }
}
