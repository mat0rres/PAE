using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class ProductForContactRecordInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<ProductForContactRecord, long>
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Nombre(s)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Phone]
        [Display(Name = "Número de Teléfono")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Especificaciones")]
        public string Message { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [RegularExpression("^[0-9]{11}$")]
        [Display(Name = "Número de Identidad")]
        public string CI { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Dirección Postal")]
        public string PostalAddress { get; set; }

        public System.Guid ProductId { get; set; }

        [Display(Name = "Marca")]
        public string ProductBrandName { get; set; }

        [Display(Name = "Modelo")]
        public string ProductModel { get; set; }

        public ImageDisplayViewModel ProductFeatureImage { get; set; }

        [Display(Name = "Color")]
        public System.Guid ColorId { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableColorSelectListItems { get; set; }

        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }

        public ProductForContactRecord Export()
        {
            return new ProductForContactRecord
            {
                Name = Name,
                LastName = LastName,
                EmailAddress = EmailAddress,
                PhoneNumber = PhoneNumber,
                CI = CI,
                PostalAddress = PostalAddress,
                City = City,
                Message = Message,
                ProductId = ProductId,
                ColorId = ColorId,
                Quantity = Quantity
            };
        }

        public void Import(ProductForContactRecord entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(ProductForContactRecord entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
