using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class BrandInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Brand, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public bool IsFavorite { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid ImageId { get; set; }

        public string? ImageExtension { get; set; }

        public Brand Export()
        {
            var entity = new Brand();

            Mergue(entity);

            return entity;
        }

        public void Import(Brand entity)
        {
            Id = entity.Id;
            Name = entity.Identity.Name;
            IsFavorite = entity.IsFavorite;
            ImageId = entity.Identity.ImageId.Value;
            ImageExtension = MimeTypes.MimeTypeMap.GetExtension(entity.Identity.Image.ContentType);
        }

        public void Mergue(Brand entity)
        {
            entity.Identity.Name = Name;
            entity.IsFavorite = IsFavorite;
        }
    }
}
