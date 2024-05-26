using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Media.Persistence.Entities;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Extensions;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Catalog
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("catalog")]
    public class ProductsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractHabilitableEntityManagementController<Product, System.Guid, ProductInputViewModel, ProductDisplayViewModel>
    {
        private readonly IEntityRepository<Category, int> categoryRepository;
        private readonly IEntityRepository<Brand, System.Guid> brandRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public ProductsController(IImageFileManagerFacade imageFileManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Brand, System.Guid> brandRepository, IEntityRepository<Category, int> categoryRepository, IEntityRepository<Product, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ProductsController> localizer, ILogger<ProductsController> logger) : base(repository, localizer, logger)
        {
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Constants.Authorization.ProductViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Authorization.ProductCreatorPermission)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Authorization.ProductCreatorPermission)]
        public override IActionResult Create(ProductInputViewModel modelInput)
        {
            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    ModelState.ClearValidationState("FeatureImageFile");

                    modelInput.FeatureImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Create(modelInput);
        }

        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public override IActionResult Edit(ProductInputViewModel modelInput)
        {
            ModelState.ClearValidationState("FeatureImageFile");
            ModelState.MarkFieldValid("FeatureImageFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    modelInput.FeatureImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Edit(modelInput);
        }

        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public override IActionResult Enable(System.Guid key)
        {
            return base.Enable(key);
        }

        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public override IActionResult Disable(System.Guid key)
        {
            return base.Disable(key);
        }

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.ProductRemoverPermission)]
        public override IActionResult Delete(ProductDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Product entity, ProductInputViewModel model)
        {
            base.PreCreate(entity, model);

            if (model.FeatureImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.FeatureImageFile.CopyTo(memoryStream);

                entity.FeatureImage = new Image
                {
                    ContentType = model.FeatureImageFile.ContentType,
                    Length = model.FeatureImageFile.Length,
                    About = string.Format("Image for Product: {0}", model.Id)
                };
                entity.FeatureImageId = entity.FeatureImage.Id;

                imageFileManager.SaveImageFileContent(entity.FeatureImageId, memoryStream.ToArray(), entity.FeatureImage.ContentType);
            }
        }

        protected override void PreEdit(Product entity, ProductInputViewModel model)
        {
            base.PreEdit(entity, model);

            if (entity.BrandId != model.BrandId)
            {
                entity.Brand = null;
            }

            if (entity.CategoryId != model.CategoryId)
            {
                entity.Category = null;
            }

            if (model.FeatureImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.FeatureImageFile.CopyTo(memoryStream);

                entity.FeatureImage = new Image
                {
                    ContentType = model.FeatureImageFile.ContentType,
                    Length = model.FeatureImageFile.Length,
                    About = string.Format("Image for Product: {0}", model.Id)
                };
                entity.FeatureImageId = entity.FeatureImage.Id;

                imageFileManager.SaveImageFileContent(entity.FeatureImageId, memoryStream.ToArray(), entity.FeatureImage.ContentType);
            }
        }

        protected override void PostEdit(Product entity, ProductInputViewModel model)
        {
            base.PostEdit(entity, model);

            if (model.FeatureImageFile != null)
            {
                var deprecatedImage = imageRepository.Get(model.FeatureImageId);

                imageRepository.Delete(deprecatedImage, User.Identity.Name);

                imageFileManager.DeleteImageFile(deprecatedImage);
            }
        }

        protected override void PostDelete(Product entity)
        {
            base.PostDelete(entity);

            var deprecatedImage = entity.FeatureImage;

            imageRepository.Delete(entity.FeatureImage, User.Identity.Name);

            imageFileManager.DeleteImageFile(deprecatedImage);
        }

        protected override void ConfigureCreate(ProductInputViewModel model)
        {
            PopulateModelInput(model);

            base.ConfigureCreate(model);
        }

        protected override void ConfigurePostFailCreateValidation(ProductInputViewModel model)
        {
            PopulateModelInput(model);

            base.ConfigurePostFailCreateValidation(model);
        }

        protected override void ConfigureEdit(Product entity, ProductInputViewModel model)
        {
            PopulateModelInput(model);

            base.ConfigureEdit(entity, model);
        }

        protected override void ConfigurePostFailEditValidation(Product entity, ProductInputViewModel model)
        {
            PopulateModelInput(model);

            base.ConfigurePostFailEditValidation(entity, model);
        }

        protected override void PopulateViewModelDisplayExtras(ProductDisplayViewModel model, Product entity)
        {
            base.PopulateViewModelDisplayExtras(model, entity);

            entity.Category = categoryRepository.Get(entity.CategoryId);

            var currentParent = entity.Category.Parent;

            while (currentParent != null && currentParent.ParentId != null)
            {
                currentParent.Parent = categoryRepository.Get(currentParent.ParentId.Value);
                currentParent = currentParent.Parent.Parent;
            }

            model.Category = new CategoryDisplayViewModel();

            model.Category.Import(entity.Category);
        }

        private void PopulateModelInput(ProductInputViewModel model)
        {
            model.AvailableBrandSelectListItems = brandRepository.GetAll().ToArray().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Identity.Name }).ToArray();

            var categotiyEntities = categoryRepository.GetAll().ToArray().OrderBy(e => e.GetTreeId()).ToArray();

            var modelAvailableCategorySelectListItems = new List<SelectListItem>();

            foreach (var entity in categotiyEntities)
            {
                modelAvailableCategorySelectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.GetTreeName() });
            }

            model.AvailableCategorySelectListItems = modelAvailableCategorySelectListItems.ToArray();
        }
    }
}
