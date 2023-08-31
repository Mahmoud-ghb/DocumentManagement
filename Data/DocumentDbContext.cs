using Microsoft.EntityFrameworkCore;
using DocumentManagement.Models;
using DocumentManagement.Data;

namespace DocumentManagement.Data
{
    public class DocumentDbContext : DbContext
    {
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
    }
}
