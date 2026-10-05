using CollageApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollageApi.EntityConfigure
{
    public class UserTypeConfigurationSchema : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("UserTypes");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.UserTypeName).IsRequired();

            builder.HasData(
                new UserType() { 
                
                ID=1,
                UserTypeName="Student",
                Description="for students"
                
                },
                   new UserType()
                   {

                       ID = 2,
                       UserTypeName = "Teacher",
                       Description = "for Teacher"

                   },
                      new UserType()
                      {

                          ID = 3,
                          UserTypeName = "HR",
                          Description = "for HR"

                      }





                );
            
        }
    }
}
