using MyVinted.API.BackgroundServices.Interfaces;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using Serilog;
using System.Threading.Tasks;

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

        public async Task Seed()
        {
            await InsertRoles();
            await InsertCategories();

            Log.Information("Database seed completed");
        }

        #region private

        private async Task InsertRoles()
        {
            foreach (var roleName in Constants.RolesToSeed)
                await rolesManager.CreateRole(roleName);
        }

        private async Task InsertCategories() => await categoryService.InsertCategoriesFromFile();

        #endregion
    }
}