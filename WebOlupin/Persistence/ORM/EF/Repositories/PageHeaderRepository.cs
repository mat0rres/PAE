using Microsoft.EntityFrameworkCore;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class PageHeaderRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<PageHeader, PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId>
    {
        public PageHeaderRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<PageHeader> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Image);
        }
    }
}
