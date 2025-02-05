namespace Repositories;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories.Config;
using System.Reflection;

public class RepositoryContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }


        public RepositoryContext(DbContextOptions<RepositoryContext> options)
        :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        //     modelBuilder.ApplyConfiguration(new ProductConfig());
        //     modelBuilder.ApplyConfiguration(new CategoryConfig());


        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());




        }
}
