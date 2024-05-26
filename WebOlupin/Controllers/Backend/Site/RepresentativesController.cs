using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using MOROPO.Framework.Guama.Media.Persistence.Entities;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Site
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("site")]
    public class RepresentativesController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<Representative, System.Guid, RepresentativeInputViewModel, RepresentativeDisplayViewModel>
    {
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public RepresentativesController(IImageFileManagerFacade imageFileManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Representative, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<RepresentativesController> localizer, ILogger<RepresentativesController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Constants.Authorization.RepresentativeViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Authorization.RepresentativeCreatorPermission)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Authorization.RepresentativeCreatorPermission)]
        public override IActionResult Create(RepresentativeInputViewModel modelInput)
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

        [RequirePermissions(Constants.Authorization.RepresentativeEditorPermission)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Authorization.RepresentativeEditorPermission)]
        public override IActionResult Edit(RepresentativeInputViewModel modelInput)
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

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.RepresentativeRemoverPermission)]
        public override IActionResult Delete(RepresentativeDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Representative entity, RepresentativeInputViewModel model)
        {
            base.PreCreate(entity, model);

            if (model.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.ImageFile.CopyTo(memoryStream);

                entity.Identity.Image = new Image
                {
                    ContentType = model.ImageFile.ContentType,
                    Length = model.ImageFile.Length,
                    About = string.Format("Image for Representative: {0}", model.Id)
                };
                entity.Identity.ImageId = entity.Identity.Image.Id;

                imageFileManager.SaveImageFileContent(entity.Identity.Image.Id, memoryStream.ToArray(), entity.Identity.Image.ContentType);
            }
        }

        protected override void PreEdit(Representative entity, RepresentativeInputViewModel model)
        {
            base.PreEdit(entity, model);

            if (model.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.ImageFile.CopyTo(memoryStream);

                entity.Identity.Image = imageRepository.Add(new Image
                {
                    ContentType = model.ImageFile.ContentType,
                    Length = model.ImageFile.Length,
                    About = string.Format("Image for Representative: {0}", model.Id)
                }, User.Identity.Name, false);
                entity.Identity.ImageId = entity.Identity.Image.Id;

                imageFileManager.SaveImageFileContent(entity.Identity.Image.Id, memoryStream.ToArray(), entity.Identity.Image.ContentType);
            }
        }

        protected override void PostEdit(Representative entity, RepresentativeInputViewModel model)
        {
            base.PostEdit(entity, model);

            if (model.ImageFile != null)
            {
                var deprecatedImage = imageRepository.Get(model.ImageId);

                imageRepository.Delete(deprecatedImage, User.Identity.Name);

                imageFileManager.DeleteImageFile(deprecatedImage);
            }
        }

        protected override void PostDelete(Representative entity)
        {
            base.PostDelete(entity);

            var deprecatedImage = entity.Identity.Image;

            imageRepository.Delete(entity.Identity.Image, User.Identity.Name);

            imageFileManager.DeleteImageFile(deprecatedImage);
        }
    }
}
