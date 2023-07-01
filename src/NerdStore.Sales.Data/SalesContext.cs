using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Data;
using NerdStore.Core.Messages;
using NerdStore.Sales.Domain.Order;

namespace NerdStore.Sales.Data
{
    public class SalesContext : DbContext, IUnitOfWork
    { 
        private readonly IMediatorHandler _mediatorHandler;

        public SalesContext(DbContextOptions<SalesContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
        } 
        

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Coupon> Coupons { get; set; }


        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DateCreated") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateCreated").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DateCreated").IsModified = false;
                }
            }

           var sucesso = await base.SaveChangesAsync() > 0;
         
           if (sucesso) await _mediatorHandler.PublishEvents(this);

           return sucesso;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                         e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            // property.Relational().ColumnType = "varchar(100)"; // Old EF fix changed to SetColumnType.

            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesContext).Assembly);

            foreach (var relationship in modelBuilder.Model
                         .GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("MySequence").StartsAt(1001).IncrementsBy(1);
            base.OnModelCreating(modelBuilder);

        }


    }
}
