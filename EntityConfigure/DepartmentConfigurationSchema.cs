using CollageApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollageApi.EntityConfigure
{
    public class DepartmentConfigurationSchema : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //configuration of department table schema constraint.
            builder.ToTable("Department");
            builder.HasKey(x=>x.ID);
            builder.Property(x=>x.DepartmentName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(250);



            //insert Default data into database when application run.
            builder.HasData(
                
               new List<Department>() { 
               new Department()
               {
                   ID=1,
                   DepartmentName="IT",
                   Description="IT Department",
                   
               },
                new Department()
               {
                   ID=2,
                   DepartmentName="CS",
                   Description="IT CS",

               }




               }
                
                
                
                );

        }
    }
}
