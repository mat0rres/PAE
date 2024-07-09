using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class BrandSupportOfferInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<BrandSupportOffer, System.Guid>
    {
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        public bool Enabled { get; set; } = true;

        public int Priority { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Brand")]
        public System.Guid BrandId { get; set; }

        public SelectListItem[] AvailableBrandSelectListItems { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile FeatureImageFile { get; set; }

        public System.Guid? FeatureImageId { get; set; }

        public string FeatureImageExtension { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public BrandSupportOffer Export()
        {
            var entity = new BrandSupportOffer();

            Mergue(entity);

            return entity;
        }

        public void Import(BrandSupportOffer entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Priority = entity.Priority;
            BrandId = entity.BrandId;
            Title = entity.Title;
            Description = entity.Description;
            FeatureImageId = entity.FeatureImageId;
            FeatureImageExtension = entity.FeatureImageId == null ? null : MimeTypes.MimeTypeMap.GetExtension(entity.FeatureImage.ContentType);
        }

        public void Mergue(BrandSupportOffer entity)
        {
            entity.Enabled = Enabled;
            entity.Priority = Priority;
            entity.BrandId = BrandId;
            entity.Title = Title;
            entity.Description = Description;
        }
    }
}
