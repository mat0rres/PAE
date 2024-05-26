namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class CategoryDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.Category, int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryDisplayViewModel Parent { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.Category entity)
        {
            Id = entity.Id;
            Name = entity.Name;

            if (entity.Parent != null)
            {
                Parent = new CategoryDisplayViewModel();

                Parent.Import(entity.Parent);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
