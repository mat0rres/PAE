using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class CampaignInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Campaign, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public string Name { get; set; }

        public bool Enabled { get; set; }

        public int Priority { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Url]
        public string Url { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        public Campaign Export()
        {
            var entity = new Campaign();

            Mergue(entity);

            return entity;
        }

        public void Import(Campaign entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Enabled = entity.Enabled;
            Priority = entity.Priority;
            Url = entity.Url;
            ImageId = entity.ImageId;
            ImageExtension = MimeTypes.MimeTypeMap.GetExtension(entity.Image.ContentType);
        }

        public void Mergue(Campaign entity)
        {
            entity.Name = Name;
            entity.Enabled = Enabled;
            entity.Priority = Priority;
            entity.Url = Url;
        }
    }
}
