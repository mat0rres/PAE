namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class RichTextBlock : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<Enumerations.RichTextBlockId>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Enumerations.RichTextBlockId Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Content { get; set; }
    }
}
