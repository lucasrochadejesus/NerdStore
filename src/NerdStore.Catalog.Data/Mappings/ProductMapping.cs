using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    { 
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.HasKey(x => x.Id);

           builder.Property(p => p.Name)
               .IsRequired()
               .HasColumnType("varchar(250)");

            builder.Property(p => p.Description)
               .IsRequired()
               .HasColumnType("varchar(500)");

            builder.Property(p => p.Image)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.ModelNumber)
                .IsRequired()
                .HasColumnType("varchar(50)");


            builder.OwnsOne(p => p.Dimensions, pd =>
            {
                pd.Property(p => p.Height)
                    .HasColumnName("height")
                    .HasColumnType("decimal");

                pd.Property(p => p.Width)
                    .HasColumnName("width")
                    .HasColumnType("decimal");

                pd.Property(p => p.Length)
                    .HasColumnName("weight")
                    .HasColumnType("decimal");
            });

            builder.ToTable("Products");

        }
    }
}
