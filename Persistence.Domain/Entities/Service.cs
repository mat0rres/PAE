namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class Service : Framework.Guama.Core.Persistence.Entities.Contract.IHabilitableEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        public bool Enabled { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(FeatureImageId))]
        public Framework.Guama.Media.Persistence.Entities.Image FeatureImage { get; set; }
        public System.Guid FeatureImageId { get; set; }

        public int Priority { get; set; }
    }
}
