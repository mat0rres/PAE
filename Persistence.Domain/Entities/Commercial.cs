namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class Commercial : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        [System.ComponentModel.DataAnnotations.Required]
        public string FullName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        public string EmailAddress { get; set; }

        public bool NotifyProductForContact { get; set; }

        public bool NotifyCommonContact { get; set; }
    }
}
