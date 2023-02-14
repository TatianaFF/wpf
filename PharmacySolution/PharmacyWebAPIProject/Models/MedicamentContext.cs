using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyWebAPIProject.Models
{
    public partial class MedicamentContext : DbContext
    {
        public MedicamentContext()
        {
        }

        public MedicamentContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<MedicamentModel> Medicaments { get; set; }
    }
}
