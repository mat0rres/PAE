using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class PostInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Post, long>
    {
        public long Id { get; set; }

        [Display(Name = "Enabled")]
        public bool Enabled { get; set; } = true;

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Date")]
        public System.DateTime CreatedAt { get; set; } = System.DateTime.Now;

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Label")]
        public string Label { get; set; }

        [Display(Name = "Context")]
        public string Context { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile? FeatureImageFile { get; set; }

        public System.Guid? FeatureImageId { get; set; }

        public string? FeatureImageExtension { get; set; }

        public Post Export()
        {
            var entity = new Post();

            Mergue(entity);

            return entity;
        }

        public void Import(Post entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Title = entity.Title;
            Label = entity.Label;
            Context = entity.Context;
            Content = entity.Content;
            CreatedAt = entity.CreatedAt;
            FeatureImageId = entity.FeatureImageId;
            FeatureImageExtension = entity.FeatureImageId == null ? null : MimeTypes.MimeTypeMap.GetExtension(entity.FeatureImage.ContentType);
        }

        public void Mergue(Post entity)
        {
            entity.Enabled = Enabled;
            entity.CreatedAt = CreatedAt;
            entity.Title = Title;
            entity.Label = Label;
            entity.Context = Context;
            entity.Content = Content;
        }
    }
}
