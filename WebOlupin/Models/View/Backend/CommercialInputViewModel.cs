using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class CommercialInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Commercial, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public bool NotifyProductForContact { get; set; }

        public bool NotifyCommonContact { get; set; }

        public Commercial Export()
        {
            var entity = new Commercial();

            Mergue(entity);

            return entity;
        }

        public void Import(Commercial entity)
        {
            Id = entity.Id;
            FullName = entity.FullName;
            EmailAddress = entity.EmailAddress;
            NotifyProductForContact = entity.NotifyProductForContact;
            NotifyCommonContact = entity.NotifyCommonContact;
        }

        public void Mergue(Commercial entity)
        {
            entity.FullName = FullName;
            entity.EmailAddress = EmailAddress;
            entity.NotifyProductForContact = NotifyProductForContact;
            entity.NotifyCommonContact = NotifyCommonContact;   
        }
    }
}
