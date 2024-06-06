using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class PageHeaderInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<PageHeader, PageId>
    {
        [Display(Name = "Page")]
        public PageId Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [FormFileContentTypes(ContentTypes = ["image/png", "image/jpeg"], ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailablePageIdSelectListItems { get; set; }

        public PageHeader Export()
        {
            var entity = new PageHeader
            {
                Id = Id
            };

            Mergue(entity);

            return entity;
        }

        public void Import(PageHeader entity)
        {
            Id = entity.Id;
            ImageId = entity.ImageId;
            ImageExtension = MimeTypes.MimeTypeMap.GetExtension(entity.Image.ContentType);
        }

        public void Mergue(PageHeader entity)
        {
        }
    }
}
