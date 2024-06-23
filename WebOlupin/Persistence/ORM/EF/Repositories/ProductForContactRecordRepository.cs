using Microsoft.EntityFrameworkCore;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class ProductForContactRecordRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<PremierAutomotiveExport.Persistence.Domain.Entities.ProductForContactRecord, long>
    {
        public ProductForContactRecordRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<ProductForContactRecord> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Product);
        }

        public override Func<ProductForContactRecord, object> GetPagingOrderSelector()
        {
            return entity => entity.CreatedAt;
        }

        public override bool IsPagingOrderDescending()
        {
            return false;
        }
    }
}
