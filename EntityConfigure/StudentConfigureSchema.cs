using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollageApi.Data
{
    public class StudentConfigureSchema : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.Age).IsRequired().IsRequired();


            builder.HasData(
                new List<Student>()
            {
                new Student(){
                ID=1,
                Name="iqra",
                Age=24,
                Email="iqra@gmail.com"

                },
                  new Student(){
                ID=2,
                Name="hira",
                Age=25,
                Email="hira@gmail.com"

                },
                    new Student(){
                ID=3,
                Name="hani",
                Age=20,
                Email="hani@gmail.com"

                } }



                );
        }
    }
}
