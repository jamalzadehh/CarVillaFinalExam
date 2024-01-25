using Carvilla.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carvilla.Configurations
{
    public class CarvillaConfigurations:IEntityTypeConfiguration<Clients>
    {
        public void Configure(EntityTypeBuilder<Clients> builder)
        {
            builder.Property(x=>x.FullName).IsRequired().HasMaxLength(64);
            builder.Property(x=>x.City).IsRequired().HasMaxLength(64);
            builder.Property(x=>x.Description).IsRequired().HasMaxLength(1000);
        }
    }
}
