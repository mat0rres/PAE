using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class CommonContactRecordInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<CommonContactRecord, long>
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Phone]
        [Display(Name = "Número de Teléfono")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Especificaciones")]
        public string Message { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Asunto")]
        public string Subject { get; set; }

        public CommonContactRecord Export()
        {
            return new CommonContactRecord
            {
                Name = Name,
                EmailAddress = EmailAddress,
                PhoneNumber = PhoneNumber,
                Subject = Subject,
                Message = Message
            };
        }

        public void Import(CommonContactRecord entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(CommonContactRecord entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
