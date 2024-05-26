using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Extensions;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Catalog
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("catalog")]
    public class CategoriesController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<Category, int, CategoryInputViewModel, CategoryDisplayViewModel>
    {
        public CategoriesController(IEntityRepository<Category, int> repository, Microsoft.Extensions.Localization.IStringLocalizer<CategoriesController> localizer, ILogger<CategoriesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Constants.Authorization.CategoryViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Authorization.CategoryCreatorPermission)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Authorization.CategoryCreatorPermission)]
        public override IActionResult Create(CategoryInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Constants.Authorization.CategoryEditorPermission)]
        public override IActionResult Edit(int key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Authorization.CategoryEditorPermission)]
        public override IActionResult Edit(CategoryInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        public override IActionResult Delete(int key)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.CategoryRemoverPermission)]
        public override IActionResult Delete(CategoryDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(CategoryInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateModelInputForAvailableParents(modelInput);
        }

        protected override void ConfigureEdit(Category entity, CategoryInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateModelInputForAvailableParents(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(CategoryInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateModelInputForAvailableParents(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(Category entity, CategoryInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(entity, modelInput);

            PopulateModelInputForAvailableParents(modelInput);
        }

        private void PopulateModelInputForAvailableParents(CategoryInputViewModel inputModel)
        {
            var entities = Repository.GetAll().ToList().Where(e => !e.GetTreeId().Contains(inputModel.Id.ToString())).OrderBy(e => e.GetTreeId()).ToArray();

            foreach (var entity in entities)
            {
                inputModel.AvailableCategorySelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.GetTreeName() });
            }
        }
    }
}
