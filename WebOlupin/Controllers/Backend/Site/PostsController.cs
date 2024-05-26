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
    public class PostsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractHabilitableEntityManagementController<Post, long, PostInputViewModel, PostDisplayViewModel>
    {
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public PostsController(IImageFileManagerFacade imageFileManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Post, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<PostsController> localizer, ILogger<PostsController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Constants.Authorization.PostViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Authorization.PostCreatorPermission)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Authorization.PostCreatorPermission)]
        public override IActionResult Create(PostInputViewModel modelInput)
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

        [RequirePermissions(Constants.Authorization.PostEditorPermission)]
        public override IActionResult Edit(long key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Authorization.PostEditorPermission)]
        public override IActionResult Edit(PostInputViewModel modelInput)
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

        [RequirePermissions(Constants.Authorization.PostEditorPermission)]
        public override IActionResult Enable(long key)
        {
            return base.Enable(key);
        }

        [RequirePermissions(Constants.Authorization.PostEditorPermission)]
        public override IActionResult Disable(long key)
        {
            return base.Disable(key);
        }

        public override IActionResult Delete(long key)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.PostRemoverPermission)]
        public override IActionResult Delete(PostDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Post entity, PostInputViewModel model)
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
                    About = string.Format("Image for Post: {0}", model.Id)
                };
                entity.FeatureImageId = entity.FeatureImage.Id;

                imageFileManager.SaveImageFileContent(entity.FeatureImageId.Value, memoryStream.ToArray(), entity.FeatureImage.ContentType);
            }
        }

        protected override void PreEdit(Post entity, PostInputViewModel model)
        {
            base.PreEdit(entity, model);

            if (model.FeatureImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.FeatureImageFile.CopyTo(memoryStream);

                entity.FeatureImage = imageRepository.Add(new Image
                {
                    ContentType = model.FeatureImageFile.ContentType,
                    Length = model.FeatureImageFile.Length,
                    About = string.Format("Image for Post: {0}", model.Id)
                }, User.Identity.Name, false);
                entity.FeatureImageId = entity.FeatureImage.Id;

                imageFileManager.SaveImageFileContent(entity.FeatureImageId.Value, memoryStream.ToArray(), entity.FeatureImage.ContentType);
            }
        }

        protected override void PostEdit(Post entity, PostInputViewModel model)
        {
            base.PostEdit(entity, model);

            if (model.FeatureImageFile != null && model.FeatureImageId != null)
            {
                var deprecatedImage = imageRepository.Get(model.FeatureImageId.Value);

                imageRepository.Delete(deprecatedImage, User.Identity.Name);

                imageFileManager.DeleteImageFile(deprecatedImage);
            }
        }

        protected override void PostDelete(Post entity)
        {
            base.PostDelete(entity);

            var deprecatedImage = entity.FeatureImage;

            imageRepository.Delete(entity.FeatureImage, User.Identity.Name);

            imageFileManager.DeleteImageFile(deprecatedImage);
        }
    }
}
