using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Media.Persistence.Entities;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Site
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("site")]
    public class CampaignsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractHabilitableEntityManagementController<Campaign, System.Guid, CampaignInputViewModel, CampaignDisplayViewModel>
    {
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public CampaignsController(IImageFileManagerFacade imageFileManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Campaign, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CampaignsController> localizer, ILogger<CampaignsController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Constants.Authorization.CampaignViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 1000)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Authorization.CampaignCreatorPermission)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Authorization.CampaignCreatorPermission)]
        public override IActionResult Create(CampaignInputViewModel modelInput)
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

        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        public override IActionResult Edit(CampaignInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.CampaignRemoverPermission)]
        public override IActionResult Delete(CampaignDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Campaign entity, CampaignInputViewModel model)
        {
            base.PreCreate(entity, model);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.ImageFile.CopyTo(memoryStream);

                entity.Image = new Image
                {
                    ContentType = model.ImageFile.ContentType,
                    Length = model.ImageFile.Length,
                    About = string.Format("Image for Showcase Item: {0}", model.Id)
                };
                entity.ImageId = entity.Image.Id;

                imageFileManager.SaveImageFileContent(entity.ImageId, memoryStream.ToArray(), entity.Image.ContentType);
            }
        }

        protected override void PostDelete(Campaign entity)
        {
            base.PostDelete(entity);

            var deprecatedImage = entity.Image;

            imageRepository.Delete(entity.Image, User.Identity.Name);
            imageFileManager.DeleteImageFile(deprecatedImage);
        }
    }
}
