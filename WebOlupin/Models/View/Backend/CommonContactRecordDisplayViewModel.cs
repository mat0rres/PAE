namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class CommonContactRecordDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.CommonContactRecord, long>
    {
        public long Id { get; set; }

        public System.DateTime CreatedAt { get; set; } = System.DateTime.Now;

        public System.DateTime? ReadAt { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Message { get; set; }

        public string Subject { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.CommonContactRecord entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            ReadAt = entity.ReadAt;
            EmailAddress = entity.EmailAddress;
            PhoneNumber = entity.PhoneNumber;
            Name = entity.Name;
            Subject = entity.Subject;
            Message = entity.Message;
        }
    }
}
