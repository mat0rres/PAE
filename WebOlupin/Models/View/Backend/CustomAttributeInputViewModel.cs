using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class CustomAttributeInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<CustomAttribute, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public CustomAttribute Export()
        {
            var entity = new CustomAttribute();

            Mergue(entity);

            return entity;
        }

        public void Import(CustomAttribute entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public void Mergue(CustomAttribute entity)
        {
            entity.Name = Name;
        }
    }
}
