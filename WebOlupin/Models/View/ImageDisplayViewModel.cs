namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class ImageDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Media.Persistence.Entities.Image, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Extension { get; set; }

        public long Length { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public void Import(Framework.Guama.Media.Persistence.Entities.Image entity)
        {
            Id = entity.Id;
            Extension = MimeTypes.MimeTypeMap.GetExtension(entity.ContentType);
            Length = entity.Length;
            Height = entity.Height;
            Width = entity.Width;
        }
    }
}

