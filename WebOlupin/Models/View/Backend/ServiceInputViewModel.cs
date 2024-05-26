using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class ServiceInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Service, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile FeatureImageFile { get; set; }

        public System.Guid FeatureImageId { get; set; }

        public string? FeatureImageExtension { get; set; }

        public Service Export()
        {
            var entity = new Service();

            Mergue(entity);

            return entity;
        }

        public void Import(Service entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            FeatureImageId = entity.FeatureImageId;
            FeatureImageExtension = MimeTypes.MimeTypeMap.GetExtension(entity.FeatureImage.ContentType);
        }

        public void Mergue(Service entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
