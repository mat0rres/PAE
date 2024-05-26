using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;

namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class SectionView : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<long>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        public System.DateTime CreatedAt { get; set; } = System.DateTime.Now;

        [System.ComponentModel.DataAnnotations.Required]
        public string Carrier { get; set; }

        public SectionKind SectionKind { get; set; }

        public string Context { get; set; }

        public string NetworkIdentifier { get; set; }
    }
}
