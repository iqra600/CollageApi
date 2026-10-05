using CollageApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollageApi.EntityConfigure
{
    public class RolePrivilegeConfigurationSchema : IEntityTypeConfiguration<RolePrivilege>
    {
        public void Configure(EntityTypeBuilder<RolePrivilege> builder)
        {


            builder.ToTable("RolePrivileges");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.RolePrivilegeName).IsRequired();
            builder.Property(x=>x.RoleId).IsRequired();
            builder.Property(x => x.isActive).IsRequired();
            builder.Property(x => x.isDeleted).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();


            builder.HasOne(x => x.Role).WithMany(x => x.RolePrivileges).HasForeignKey(x => x.RoleId);

        }
    }
}
