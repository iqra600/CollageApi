using CollageApi.EntityConfigure;
using Microsoft.EntityFrameworkCore;

namespace CollageApi.Data
{
    public class CollageContext : DbContext
    {
        public CollageContext(DbContextOptions<CollageContext> options):base(options)
        {

                
        }
       public DbSet<Student> student { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePrivilege> RolePrivileges { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfigureSchema());
            modelBuilder.ApplyConfiguration(new DepartmentConfigurationSchema());
            modelBuilder.ApplyConfiguration(new UsersConfigureSchema());
            modelBuilder.ApplyConfiguration(new  RoleConfigurationSchema());
            modelBuilder.ApplyConfiguration(new RolePrivilegeConfigurationSchema());
            modelBuilder.ApplyConfiguration(new UserRoleMappingConfigurationScheme());
            modelBuilder.ApplyConfiguration(new UserTypeConfigurationSchema());



            //modelBuilder.Entity<Student>().HasData(new List<Student>()
            //{
            //    new Student(){ 
            //    ID=1,
            //    Name="iqra",
            //    Age=24,
            //    Email="iqra@gmail.com"

            //    },
            //      new Student(){
            //    ID=2,
            //    Name="hira",
            //    Age=25,
            //    Email="hira@gmail.com"

            //    },
            //        new Student(){
            //    ID=3,
            //    Name="hani",
            //    Age=20,
            //    Email="hani@gmail.com"

            //    }

            //});
            ////schema define constraint on student class 
            //modelBuilder.Entity<Student>(
            //    entity => {
            //        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
            //        entity.Property(x => x.Email).HasMaxLength(50).IsRequired(false);
            //        entity.Property(x => x.Age).IsRequired().IsRequired();


            //    } );

        }

    }
}
