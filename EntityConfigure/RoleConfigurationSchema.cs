using CollageApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollageApi.EntityConfigure
{
    public class RoleConfigurationSchema : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.ToTable("Roles");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.RoleName).IsRequired();
            builder.Property(x => x.isActive).IsRequired();
            builder.Property(x => x.isDeleted).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
        }
    }
}
