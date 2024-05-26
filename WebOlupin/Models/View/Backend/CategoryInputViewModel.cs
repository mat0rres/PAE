using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class CategoryInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Category, int>
    {
        public CategoryInputViewModel()
        {
            AvailableCategorySelectListItems = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Parent")]
        public int? ParentId { get; set; }

        public bool Enabled { get; set; }

        public bool IsOnNavigation { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableCategorySelectListItems { get; set; }

        public Category Export()
        {
            var entity = new Category();

            Mergue(entity);

            return entity;
        }

        // TODO Fill AvailableCategorySelectListItems at Controller
        public void Import(Category entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ParentId = entity.ParentId;
            Enabled = entity.Enabled;
            IsOnNavigation = entity.IsOnNavigation;
        }

        public void Mergue(Category entity)
        {
            entity.Name = Name;
            entity.ParentId = ParentId;
            entity.Enabled = Enabled;
            entity.IsOnNavigation = IsOnNavigation;
        }
    }
}
