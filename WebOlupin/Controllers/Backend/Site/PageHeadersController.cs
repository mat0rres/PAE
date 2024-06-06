using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.Framework.Guama.Media.Persistence.Entities;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Site
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("site")]
    public class PageHeadersController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<PageHeader, PageId, PageHeaderInputViewModel, PageHeaderDisplayViewModel>
    {
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public PageHeadersController(IImageFileManagerFacade imageFileManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<PageHeader, PageId> repository, Microsoft.Extensions.Localization.IStringLocalizer<PageHeadersController> localizer, ILogger<PageHeadersController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Constants.Authorization.PageHeaderViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 1000)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Authorization.PageHeaderCreatorPermission)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Authorization.PageHeaderCreatorPermission)]
        public override IActionResult Create(PageHeaderInputViewModel modelInput)
        {
            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    ModelState.ClearValidationState("ImageFile");

                    modelInput.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Create(modelInput);
        }

        [RequirePermissions(Constants.Authorization.PageHeaderEditorPermission)]
        public override IActionResult Edit(PageId key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Authorization.PageHeaderEditorPermission)]
        public override IActionResult Edit(PageHeaderInputViewModel modelInput)
        {
            ModelState.ClearValidationState("ImageFile");
            ModelState.MarkFieldValid("ImageFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    modelInput.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Edit(modelInput);
        }

        public override IActionResult Delete(PageId key)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.PageHeaderRemoverPermission)]
        public override IActionResult Delete(PageHeaderDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(PageHeader entity, PageHeaderInputViewModel model)
        {
            base.PreCreate(entity, model);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.ImageFile.CopyTo(memoryStream);

                entity.Image = new Image
                {
                    ContentType = model.ImageFile.ContentType,
                    Length = model.ImageFile.Length,
                    About = string.Format("Image for Page Header: {0}", model.Id)
                };
                entity.ImageId = entity.Image.Id;

                imageFileManager.SaveImageFileContent(entity.ImageId, memoryStream.ToArray(), entity.Image.ContentType);
            }
        }

        protected override void PreEdit(PageHeader entity, PageHeaderInputViewModel model)
        {
            base.PreEdit(entity, model);

            if (model.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.ImageFile.CopyTo(memoryStream);

                entity.Image = imageRepository.Add(new Image
                {
                    ContentType = model.ImageFile.ContentType,
                    Length = model.ImageFile.Length,
                    About = string.Format("Image for Page Header: {0}", model.Id)
                }, User.Identity.Name, false);
                entity.ImageId = entity.Image.Id;

                imageFileManager.SaveImageFileContent(entity.ImageId, memoryStream.ToArray(), entity.Image.ContentType);
            }
        }

        protected override void PostEdit(PageHeader entity, PageHeaderInputViewModel model)
        {
            base.PostEdit(entity, model);

            if (model.ImageFile != null)
            {
                var deprecatedImage = imageRepository.Get(model.ImageId);

                imageRepository.Delete(deprecatedImage, User.Identity.Name);

                imageFileManager.DeleteImageFile(deprecatedImage);
            }
        }

        protected override void PostDelete(PageHeader entity)
        {
            base.PostDelete(entity);

            var deprecatedImage = entity.Image;

            imageRepository.Delete(entity.Image, User.Identity.Name);
            imageFileManager.DeleteImageFile(deprecatedImage);
        }

        protected override void ConfigureCreate(PageHeaderInputViewModel model)
        {
            base.ConfigureCreate(model);

            PopulateInputViewModel(model);
        }

        protected override void ConfigurePostFailCreateValidation(PageHeaderInputViewModel model)
        {
            base.ConfigurePostFailCreateValidation(model);

            PopulateInputViewModel(model);
        }

        private void PopulateInputViewModel(PageHeaderInputViewModel model)
        {
            model.AvailablePageIdSelectListItems = ((PageId[])System.Enum.GetValues(typeof(PageId))).Except(Repository.GetAll().Select(_entity => _entity.Id).ToArray()).Select(_item => new SelectListItem
            {
                Value = ((int)_item).ToString(),
                Text = _item.ToString()
            }).ToArray();
        }
    }
}
