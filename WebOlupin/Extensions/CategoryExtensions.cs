namespace MOROPO.PremierAutomotiveExport.WebOlupin.Extensions
{
    public static class CategoryExtensions
    {
        public static string GetTreeId(this PremierAutomotiveExport.Persistence.Domain.Entities.Category target)
        {
            return target.Parent != null ? string.Format("{0}_{1}", target.Parent.GetTreeId(), target.Id) : target.Id.ToString();
        }

        public static string GetTreeName(this PremierAutomotiveExport.Persistence.Domain.Entities.Category target)
        {
            return target.Parent != null ? string.Format("{0} | {1}", target.Parent.GetTreeName(), target.Name) : target.Name;
        }
    }
}
