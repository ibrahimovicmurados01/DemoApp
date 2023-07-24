using AutoMapper;
using DemoApp.Entities.Models;
using DemoApp.Web.Models;

namespace DemoApp.Web.Mappers
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<ContactModel, Contact>().ReverseMap();
            CreateMap<UserModel, User>().ReverseMap();

        }
    }
}
