using SC.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SC.Repository
{
    public class SCDbContext : SCDbEntities
    {
        public override int SaveChanges()
        {

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {

            return await base.SaveChangesAsync();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
