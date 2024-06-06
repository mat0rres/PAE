using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class RepresentativeInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Representative, System.Guid>
    {
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "Priority")]
        public int Priority { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Position")]
        public string Position { get; set; }

        public Representative Export()
        {
            var entity = new Representative
            {
                Identity = new Framework.Guama.Identity.Persistence.Entities.PersonIdentity()
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Representative entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Priority = entity.Priority;
            Position = entity.Position;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            About = entity.Identity.About;
            ImageId = entity.Identity.ImageId.Value;
            ImageExtension = MimeTypes.MimeTypeMap.GetExtension(entity.Identity.Image.ContentType);
        }

        public void Mergue(Representative entity)
        {
            entity.Enabled = Enabled;
            entity.Priority = Priority;
            entity.Position = Position;
            entity.Identity.Name = Name;
            entity.Identity.LastName = LastName;
            entity.Identity.About = About;
        }
    }
}
