namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class PageVisit : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<long>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        public Enumerations.PageId PageId { get; set; }

        public System.DateTime CreatedAt { get; set; } = System.DateTime.Now;

        [System.ComponentModel.DataAnnotations.Required]
        public string Carrier { get; set; }

        public string Context { get; set; }

        public string NetworkIdentifier { get; set; }
    }
}
