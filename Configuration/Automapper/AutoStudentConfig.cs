using AutoMapper;
using CollageApi.Data;
using CollageApi.ViewModels;

namespace CollageApi.Configuration.Automapper
{
    public class AutoStudentConfig:Profile
    {

        public AutoStudentConfig()
        {
            CreateMap<Student, ViewModelStudent>().ForMember(a => a.Date, x => x.MapFrom(a => DateTime.Now)).ForMember(x => x.isEligible, y => y.MapFrom(z => z.Age >= 18))
                .ForMember(x=>x.NameDto,y=>y.MapFrom(z=>z.Name))
                ;
            
        }

    }
}
