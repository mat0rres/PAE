using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class PageVisitRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<PageVisit, long>
    {
        public PageVisitRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
