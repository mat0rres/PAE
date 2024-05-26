namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class Post : Framework.Guama.Core.Persistence.Entities.Contract.IHabilitableEntity<long>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        public bool Enabled { get; set; }

        public System.DateTime CreatedAt { get; set; } = System.DateTime.Now;

        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Summary { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Content { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(FeatureImageId))]
        public Framework.Guama.Media.Persistence.Entities.Image FeatureImage { get; set; }
        public System.Guid? FeatureImageId { get; set; }
    }
}
