using CollageApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollageApi.EntityConfigure
{
    public class UserRoleMappingConfigurationScheme : IEntityTypeConfiguration<UserRoleMaping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMaping> builder)
        {
            builder.ToTable("UserRoleMap");
            builder.HasKey(x => x.ID);
            builder.Property(x=>x.ID).UseIdentityColumn();
            builder.Property(x=>x.UserId).IsRequired();
            builder.Property(x=>x.RoleId).IsRequired();
            builder.HasIndex(x => new { x.UserId, x.RoleId }, "UK_UserRoleMapping").IsUnique();


            //foregn key of user
            builder.HasOne(x => x.User).WithMany(x => x.UserRolemap).HasForeignKey(x => x.UserId).HasConstraintName("FK_UserRoleMap_User");

            //foregn key of Role
            builder.HasOne(x => x.Role).WithMany(x => x.UserRoleMap).HasForeignKey(x => x.RoleId).HasConstraintName("FK_UserRoleMap_Role"); ;


        }
    }
}
