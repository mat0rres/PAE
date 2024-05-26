namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class RichTextBlockInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<PremierAutomotiveExport.Persistence.Domain.Entities.RichTextBlock, PremierAutomotiveExport.Persistence.Domain.Enumerations.RichTextBlockKind>
    {
        [System.ComponentModel.DataAnnotations.Display(Name = "Id")]
        public PremierAutomotiveExport.Persistence.Domain.Enumerations.RichTextBlockKind Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Content")]
        public string Content { get; set; }

        public PremierAutomotiveExport.Persistence.Domain.Entities.RichTextBlock Export()
        {
            throw new System.NotImplementedException();
        }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.RichTextBlock entity)
        {
            Id = entity.Id;
            Content = entity.Content;
        }

        public void Mergue(PremierAutomotiveExport.Persistence.Domain.Entities.RichTextBlock entity)
        {
            entity.Content = Content;
        }
    }
}
