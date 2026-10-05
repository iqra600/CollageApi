using CollageApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollageApi.EntityConfigure
{
    public class UsersConfigureSchema : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x=>x.ID);
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x=>x.Password).IsRequired();
            builder.Property(x => x.UserTypeId).IsRequired();
            builder.Property(x => x.isActive).IsRequired();
            builder.Property(x=>x.isDeleted).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x=>x.PasswordSalt).IsRequired();

            builder.HasOne(x => x.UserType).WithMany(x => x.Users).HasForeignKey(x => x.UserTypeId).HasConstraintName("FK_UserType_User");

           
        }
    }
}
