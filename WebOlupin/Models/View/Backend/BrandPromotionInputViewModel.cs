using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class BrandPromotionInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<BrandPromotion, System.Guid>
    {
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Brand")]
        public System.Guid BrandId { get; set; }

        public SelectListItem[] AvailableBrandSelectListItems { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile FeatureImageFile { get; set; }

        public System.Guid FeatureImageId { get; set; }

        public string FeatureImageExtension { get; set; }

        public BrandPromotion Export()
        {
            var entity = new BrandPromotion();

            Mergue(entity);

            return entity;
        }

        public void Import(BrandPromotion entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Description = entity.Description;
            BrandId = entity.BrandId;
            FeatureImageId = entity.FeatureImageId;
            FeatureImageExtension = MimeTypes.MimeTypeMap.GetExtension(entity.FeatureImage.ContentType);
        }

        public void Mergue(BrandPromotion entity)
        {
            entity.Title = Title;
            entity.Description = Description;
            entity.BrandId = BrandId;
        }
    }
}
