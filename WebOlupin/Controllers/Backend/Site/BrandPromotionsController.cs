using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Media.Persistence.Entities;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Site
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("site")]
    public class BrandPromotionsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<BrandPromotion, System.Guid, BrandPromotionInputViewModel, BrandPromotionDisplayViewModel>
    {
        private readonly IEntityRepository<Brand, System.Guid> brandRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public BrandPromotionsController(IImageFileManagerFacade imageFileManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Brand, System.Guid> brandRepository, IEntityRepository<BrandPromotion, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<BrandPromotionsController> localizer, ILogger<BrandPromotionsController> logger) : base(repository, localizer, logger)
        {
            this.brandRepository = brandRepository;
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Constants.Authorization.BrandPromotionViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Authorization.BrandPromotionCreatorPermission)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Authorization.BrandPromotionCreatorPermission)]
        public override IActionResult Create(BrandPromotionInputViewModel modelInput)
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

        [RequirePermissions(Constants.Authorization.BrandPromotionEditorPermission)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Authorization.BrandPromotionEditorPermission)]
        public override IActionResult Edit(BrandPromotionInputViewModel modelInput)
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

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.BrandPromotionRemoverPermission)]
        public override IActionResult Delete(BrandPromotionDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(BrandPromotion entity, BrandPromotionInputViewModel model)
        {
            base.PreCreate(entity, model);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.FeatureImageFile.CopyTo(memoryStream);

                entity.FeatureImage = new Image
                {
                    ContentType = model.FeatureImageFile.ContentType,
                    Length = model.FeatureImageFile.Length,
                    About = string.Format("Image for Brand Promotion: {0}", model.Id)
                };
                entity.FeatureImageId = entity.FeatureImage.Id;

                imageFileManager.SaveImageFileContent(entity.FeatureImageId, memoryStream.ToArray(), entity.FeatureImage.ContentType);
            }
        }

        protected override void PreEdit(BrandPromotion entity, BrandPromotionInputViewModel model)
        {
            base.PreEdit(entity, model);

            if (entity.BrandId != model.BrandId)
            {
                entity.Brand = null;
            }

            if (model.FeatureImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.FeatureImageFile.CopyTo(memoryStream);

                entity.FeatureImage = imageRepository.Add(new Image
                {
                    ContentType = model.FeatureImageFile.ContentType,
                    Length = model.FeatureImageFile.Length,
                    About = string.Format("Image for Brand Promotion: {0}", model.Id)
                }, User.Identity.Name, false);
                entity.FeatureImageId = entity.FeatureImage.Id;

                imageFileManager.SaveImageFileContent(entity.FeatureImageId, memoryStream.ToArray(), entity.FeatureImage.ContentType);
            }
        }

        protected override void PostEdit(BrandPromotion entity, BrandPromotionInputViewModel model)
        {
            base.PostEdit(entity, model);

            if (model.FeatureImageFile != null)
            {
                var deprecatedImage = imageRepository.Get(model.FeatureImageId);

                imageRepository.Delete(deprecatedImage, User.Identity.Name);

                imageFileManager.DeleteImageFile(deprecatedImage);
            }
        }

        protected override void PostDelete(BrandPromotion entity)
        {
            base.PostDelete(entity);

            var deprecatedImage = entity.FeatureImage;

            imageRepository.Delete(entity.FeatureImage, User.Identity.Name);
            imageFileManager.DeleteImageFile(deprecatedImage);
        }

        protected override void ConfigureCreate(BrandPromotionInputViewModel model)
        {
            PopulateModelInput(model);

            base.ConfigureCreate(model);
        }

        protected override void ConfigurePostFailCreateValidation(BrandPromotionInputViewModel model)
        {
            PopulateModelInput(model);

            base.ConfigurePostFailCreateValidation(model);
        }

        protected override void ConfigureEdit(BrandPromotion entity, BrandPromotionInputViewModel model)
        {
            PopulateModelInput(model);

            base.ConfigureEdit(entity, model);
        }

        protected override void ConfigurePostFailEditValidation(BrandPromotion entity, BrandPromotionInputViewModel model)
        {
            PopulateModelInput(model);

            base.ConfigurePostFailEditValidation(entity, model);
        }

        private void PopulateModelInput(BrandPromotionInputViewModel model)
        {
            model.AvailableBrandSelectListItems = brandRepository.GetAll().ToArray().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Identity.Name }).ToArray();
        }
    }
}
