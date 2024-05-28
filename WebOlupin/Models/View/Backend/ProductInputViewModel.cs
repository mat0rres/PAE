using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class ProductInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Product, System.Guid>
    {
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; } = true;

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Brand")]
        public System.Guid BrandId { get; set; }

        public SelectListItem[]? AvailableBrandSelectListItems { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public SelectListItem[]? AvailableCategorySelectListItems { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile FeatureImageFile { get; set; }

        public System.Guid FeatureImageId { get; set; }

        public string? FeatureImageExtension { get; set; }

        public Product Export()
        {
            var entity = new Product();

            Mergue(entity);

            return entity;
        }

        public void Import(Product entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Description = entity.Description;
            FeatureImageId = entity.FeatureImageId;
            FeatureImageExtension = MimeTypes.MimeTypeMap.GetExtension(entity.FeatureImage.ContentType);
            BrandId = entity.BrandId;
            Model = entity.Model;
            CategoryId = entity.CategoryId;
        }

        public void Mergue(Product entity)
        {
            entity.Enabled = Enabled;
            entity.Description = Description;
            entity.CategoryId = CategoryId;
            entity.BrandId = BrandId;
            entity.Model = Model;
        }
    }
}
