using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Sales.Domain.Order;

namespace NerdStore.Sales.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           builder.HasKey(x => x.Id);

           builder.Property(c => c.OrderId).HasDefaultValueSql("NEXT VALUE FOR MySequence");

           // 1 : N -> Order : OrderItems
           builder.HasMany(c => c.OrderItems)
               .WithOne(c => c.Order)
               .HasForeignKey(c => c.OrderItemId);

           builder.ToTable("Orders");

        }
    }
}
