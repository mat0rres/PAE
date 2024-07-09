namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class Campaign : Framework.Guama.Core.Persistence.Entities.Contract.IHabilitableEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public bool Enabled { get; set; }

        public int Priority { get; set; } = 1;

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Url]
        public string Url { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ImageId))]
        public Framework.Guama.Media.Persistence.Entities.Image Image { get; set; }
        public System.Guid ImageId { get; set; }
    }
}
