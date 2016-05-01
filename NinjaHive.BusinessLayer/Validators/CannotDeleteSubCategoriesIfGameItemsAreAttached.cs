using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;
using System;

namespace NinjaHive.BusinessLayer.Validators
{
    public class CannotDeleteSubCategoriesIfGameItemsAreAttached
        : IValidator<DeleteEntityCommand<SubCategoryModel>>
    {
        private readonly IRepository<SubCategoryEntity> subcategoryRepository;

        public CannotDeleteSubCategoriesIfGameItemsAreAttached(IRepository<SubCategoryEntity> subcategoryRepository)
        {
            this.subcategoryRepository = subcategoryRepository;
        }

        public IEnumerable<ValidationResult> Validate(DeleteEntityCommand<SubCategoryModel> instance)
        {
            var subcategory = this.subcategoryRepository.FindById(instance.Id);
            var gameItems = subcategory.GameItems;

            if (gameItems.Any())
            {
                var items = string.Join(Environment.NewLine,
                    from item in gameItems
                    select $" - {item.Name}");

                yield return new ValidationResult(
                    $"Cannot delete the subcategory '{subcategory.Name}', " +
                     "because the following gameitems are attached to it:" + 
                     Environment.NewLine + items);
            }
        }
    }
}
