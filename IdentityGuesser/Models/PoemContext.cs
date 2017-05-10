using System.Data.Entity;

namespace IdentityGuesser.Models
{
    public class PoemContext: DbContext
    {
        public PoemContext(): base("DefaultConnection"){ }

        public DbSet<PoemModel> PoemModels { get; set; }
    }
}