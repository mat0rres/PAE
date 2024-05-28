using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
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
        private readonly IEntityRepository<CustomAttribute, System.Guid> customAttributeRepository;

        public ProductsController(IEntityRepository<CustomAttribute, System.Guid> customAttributeRepository, IImageFileManagerFacade imageFileManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Brand, System.Guid> brandRepository, IEntityRepository<Category, int> categoryRepository, IEntityRepository<Product, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ProductsController> localizer, ILogger<ProductsController> logger) : base(repository, localizer, logger)
        {
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
            this.customAttributeRepository = customAttributeRepository;
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

                entity.FeatureImage = imageRepository.Add(new Image
                {
                    ContentType = model.FeatureImageFile.ContentType,
                    Length = model.FeatureImageFile.Length,
                    About = string.Format("Image for Product: {0}", model.Id)
                }, User.Identity.Name, false);
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

        #region Custom Attributes

        [Route("edit/{key}/attributes")]
        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public IActionResult EditAttributes(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var availableCustomAttributes = customAttributeRepository.GetAll().ToList();

                foreach (var bridge in entity.CustomAttributeBridges)
                {
                    var customAtributte = availableCustomAttributes.First(item => item.Id == bridge.SternId);

                    availableCustomAttributes.Remove(customAtributte);
                }

                var model = new ProductCustomAttributesManagementViewModel
                {
                    Product = new ProductDisplayViewModel(),
                    Items = entity.CustomAttributeBridges.Select(bridge => new ProductCustomAttributeViewModel
                    {
                        CustomAttributeId = bridge.SternId,
                        CustomAttributeName = bridge.Stern.Name,
                        Value = bridge.Value
                    }).ToArray(),
                    AvailableCustomAttributeSelectListItems = availableCustomAttributes.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToArray()
                };

                model.Product.Import(entity);

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/{key}/attributes/add")]
        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAttribute(System.Guid key, System.Guid? customAttributeId, string value)
        {
            try
            {
                var entity = Repository.Get(key);

                if (customAttributeId == null)
                {
                    ModelState.AddModelError("customAttributeId", string.Format(localizer["RequiredValidationErrorMessage"], localizer["CustomAttribute"]));
                }

                if (string.IsNullOrEmpty(value))
                {
                    ModelState.AddModelError("value", string.Format(localizer["RequiredValidationErrorMessage"], localizer["Value"]));
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        entity.CustomAttributeBridges.Add(new ProductCustomAttributeBridge { ProwId = entity.Id, SternId = customAttributeId.Value, Value = value });

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("EditAttributes", new { key });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }                

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("EditAttributes", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/attributes/edit")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public IActionResult EditAttribute(System.Guid key, System.Guid customAttributeId, string value)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.CustomAttributeBridges.First(b => b.SternId == customAttributeId);

                bridge.Value = value;

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

                return RedirectToAction("EditCustomAttributes", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/attributes/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public IActionResult DeleteAttribute(System.Guid key, System.Guid customAttributeId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.CustomAttributeBridges.First(b => b.SternId == customAttributeId);

                entity.CustomAttributeBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("EditAttributes", new { key });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("EditAttributes", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion

        #region Features

        [Route("edit/{key}/features")]
        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public IActionResult EditFeatures(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var model = new ProductFeaturesManagementViewModel
                {
                    Product = new ProductDisplayViewModel(),
                    Items = entity.Features.Select(_entity =>
                    {
                        var _model = new ProductFeatureDisplayViewModel();

                        _model.Import(_entity);

                        return _model;
                    }).ToArray()
                };

                model.Product.Import(entity);

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/{key}/features/add")]
        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFeature(System.Guid key, string description)
        {
            try
            {
                var entity = Repository.Get(key);

                if (string.IsNullOrEmpty(description))
                {
                    ModelState.AddModelError("description", string.Format(localizer["RequiredValidationErrorMessage"], localizer["Description"]));
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        entity.Features.Add(new ProductFeature { ProductId = entity.Id, Description = description });

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("EditFeatures", new { key });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("EditFeatures", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/features/edit")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public IActionResult EditFeature(System.Guid key, long featureId, string description)
        {
            try
            {
                var entity = Repository.Get(key);

                var feature = entity.Features.First(b => b.Id == featureId);

                feature.Description = description;

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

                return RedirectToAction("EditFeatures", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/features/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public IActionResult DeleteFeature(System.Guid key, long featureId)
        {
            try
            {
                var entity = Repository.Get(key);

                var feature = entity.Features.First(b => b.Id == featureId);

                entity.Features.Remove(feature);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("EditFeatures", new { key });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("EditFeatures", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion

        #region Images

        [HttpGet]
        [Route("edit/{key}/images")]
        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public ActionResult EditImages(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ProductImagesManagementViewModel
            {
                Product = new ProductDisplayViewModel(),
                Items = entity.ExtraImageBridges.Select(bridge =>
                {
                    var result = new ImageDisplayViewModel();

                    result.Import(bridge.Stern);

                    return result;
                }).ToArray()
            };

            model.Product.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/images/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public IActionResult AddImage(System.Guid key)
        {
            try
            {
                var imageFile = Request.Form.Files[0];

                var entity = Repository.Get(key);

                // TODO Validate imageFile

                if (ModelState.IsValid)
                {
                    try
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            imageFile.CopyTo(memoryStream);

                            var image = new Image { ContentType = imageFile.ContentType, Length = imageFile.Length, About = "For Product " + entity.Id };

                            imageRepository.Add(image, User.Identity.Name, false);

                            imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), imageFile.ContentType);

                            entity.ExtraImageBridges.Add(new ProductImageBridge
                            {
                                ProwId = entity.Id,
                                SternId = image.Id
                            });
                        }

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editimages", new { key });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editimages", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/images/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Authorization.ProductEditorPermission)]
        public IActionResult DeleteImage(System.Guid key, System.Guid imageId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.ExtraImageBridges.First(b => b.SternId == imageId);

                entity.ExtraImageBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                imageRepository.Delete(bridge.Stern, User.Identity.Name);

                imageFileManager.DeleteImageFile(bridge.Stern);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editimages", new { key });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editimages", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion
    }
}
