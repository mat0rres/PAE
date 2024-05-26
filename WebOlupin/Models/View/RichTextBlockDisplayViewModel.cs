namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class RichTextBlockDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.RichTextBlock, PremierAutomotiveExport.Persistence.Domain.Enumerations.RichTextBlockKind>
    {
        public PremierAutomotiveExport.Persistence.Domain.Enumerations.RichTextBlockKind Id { get; set; }

        public string Content { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.RichTextBlock entity)
        {
            Id = entity.Id;
            Content = entity.Content;
        }
    }
}
