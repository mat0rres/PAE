namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class PageHeader : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<Enumerations.PageId>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Enumerations.PageId Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ImageId))]
        public Framework.Guama.Media.Persistence.Entities.Image Image { get; set; }
        public System.Guid ImageId { get; set; }
    }
}
