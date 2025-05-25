using AutoMapper;
using Blood.Contract.Repositories.Entity;
using Blood.ModelViews.DonorProfileViews;
using Blood.ModelViews.RoleModelViews;


namespace Blood.Repositories.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Role
			CreateMap<ApplicationRole, RoleModelView>().ReverseMap();
			CreateMap<ApplicationRole, CreateRoleModelView>().ReverseMap();
			CreateMap<ApplicationRole, UpdatedRoleModelView>().ReverseMap();
            CreateMap<DonorProfile, DonorProfileModelView>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.BloodTypeName, opt => opt.MapFrom(src => src.BloodType.Name));

            CreateMap<CreateDonorProfileModelView, DonorProfile>();
            CreateMap<UpdateDonorProfileModelView, DonorProfile>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
