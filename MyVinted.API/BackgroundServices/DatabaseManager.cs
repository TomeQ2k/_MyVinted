using MyVinted.API.BackgroundServices.Interfaces;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using System.Linq;

namespace MyVinted.API.BackgroundServices
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IRolesManager rolesManager;
        private readonly ICategoryService categoryService;

        public DatabaseManager(IRolesManager rolesManager, ICategoryService categoryService)
        {
            this.rolesManager = rolesManager;
            this.categoryService = categoryService;
        }

        public void Seed()
        {
            InsertRoles();
            InsertCategories();
        }

        #region private

        private void InsertRoles()
        {
            Constants.RolesToSeed.ToList().ForEach((roleName) =>
            {
                rolesManager.CreateRole(roleName).Wait();
            });
        }

        private async void InsertCategories() => await categoryService.InsertCategoriesFromFile();

        #endregion
    }
}