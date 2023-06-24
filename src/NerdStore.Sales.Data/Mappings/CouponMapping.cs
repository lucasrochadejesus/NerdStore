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
    public class CouponMapping : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasColumnType("varchar(100)");

            // 1 : N => Coupon : Pedidos
            builder.HasMany(c => c.Orders)
                .WithOne(c => c.Coupon)
                .HasForeignKey(c => c.CouponId);

            builder.ToTable("Coupons");
        }
    }
}
