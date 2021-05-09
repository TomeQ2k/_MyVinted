using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Domain.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReadOnlyFilesManager filesManager;

        private const string CategoriesFilePath = "/data/categories.json";
        private const string OtherCategoryName = "OTHER";

        public CategoryService(IUnitOfWork unitOfWork, IReadOnlyFilesManager filesManager)
        {
            this.unitOfWork = unitOfWork;
            this.filesManager = filesManager;
        }

        public async Task<bool> InsertCategoriesFromFile()
        {
            var jsonCategories = await filesManager.ReadFile(CategoriesFilePath);
            var categories = jsonCategories.FromJSON<IEnumerable<Category>>();

            if ((await unitOfWork.CategoryRepository.Find(c => c != null) == null))
                unitOfWork.CategoryRepository.AddRange(categories);

            return await unitOfWork.Complete();
        }

        public async Task<IEnumerable<Category>> FetchCategories()
            => (await unitOfWork.CategoryRepository.GetWhere(c => c.Name.ToUpper() != OtherCategoryName))
                .OrderBy(c => c.Name)
                .Append(await unitOfWork.CategoryRepository.Find(c => c.Name.ToUpper() == OtherCategoryName));
    }
}