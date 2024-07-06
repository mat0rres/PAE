namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class CommercialDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.Commercial, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public bool NotifyProductForContact { get; set; }

        public bool NotifyCommonContact { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.Commercial entity)
        {
            Id = entity.Id;
            FullName = entity.FullName;
            EmailAddress = entity.EmailAddress;
            NotifyProductForContact = entity.NotifyProductForContact;
            NotifyCommonContact = entity.NotifyCommonContact;
        }
    }
}
