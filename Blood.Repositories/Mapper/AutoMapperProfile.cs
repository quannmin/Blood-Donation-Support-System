using AutoMapper;
using Blood.Contract.Repositories.Entity;
using Blood.ModelViews.AuthModelViews.Response;
using Blood.ModelViews.BlogPostModelViews;
using Blood.ModelViews.BloodCompatibilityModelViews;
using Blood.ModelViews.BloodGroupModelViews;
using Blood.ModelViews.BloodRequestModelViews;
using Blood.ModelViews.BloodUnitModelViews;
using Blood.ModelViews.DonationModelViews;
using Blood.ModelViews.DonorAvailabilityModelViews;
using Blood.ModelViews.RoleModelViews;
using Blood.ModelViews.UserModelViews.Request;
using Blood.ModelViews.UserModelViews.Response;
using Blood.Repositories.Entity;


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

            //BloodGroup
            CreateMap<BloodGroup, BloodGroupModelView>().ReverseMap();
            CreateMap<BloodGroup, CreateBloodGroupModelView>().ReverseMap();
            CreateMap<BloodGroup, UpdateBloodGroupModelView>().ReverseMap();

            //BloodCompatibility
            CreateMap<BloodCompatibility, BloodCompatibilityModelView>().ReverseMap();
            CreateMap<BloodCompatibility, CreateBloodCompatibilityModelView>().ReverseMap();
            CreateMap<BloodCompatibility, UpdateBloodCompatibilityModelView>().ReverseMap();

            //User
            CreateMap<ApplicationUser, UserLoginResponseModel>().ReverseMap();
            CreateMap<ApplicationUser, UserResponseModel>().ReverseMap();
            CreateMap<ApplicationUser, UpdateUserProfileRequest>().ReverseMap();

            //Employee
            CreateMap<EmployeeLoginResponseModel, ApplicationUser>().ReverseMap();
            CreateMap<CreateEmployeeRequest, ApplicationUser>().ReverseMap();
            CreateMap<UpdateEmployeeProfileRequest, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, EmployeeResponseModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role))
                .ReverseMap()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore());

            //BloodUnit
            CreateMap<BloodUnit, BloodUnitModelView>().ReverseMap();
            CreateMap<BloodUnit, CreateBloodUnitModelView>().ReverseMap();
            CreateMap<BloodUnit, UpdateBloodUnitModelView>().ReverseMap();

            //BlogPost
            CreateMap<BlogPost, BlogPostModelView>().ReverseMap();
            CreateMap<BlogPost, BlogPostCreateModelView>().ReverseMap();
            CreateMap<BlogPost, BlogPostUpdateModelView>().ReverseMap();

            //DonorAvailability
            CreateMap<DonorAvailability, DonorAvailabilityModelView>().ReverseMap();
            CreateMap<DonorAvailability, CreateDonorAvailabilityModelView>().ReverseMap();
            CreateMap<DonorAvailability, UpdateDonorAvailabilityModelView>().ReverseMap();

            //BloodRequest
            CreateMap<BloodRequest, BloodRequestModelView>().ReverseMap();
            CreateMap<BloodRequest, CreateBloodRequestModelView>().ReverseMap();
            CreateMap<BloodRequest, UpdateBloodRequestModelView>().ReverseMap();

            //Donation
            CreateMap<Donation, DonationModelView>().ReverseMap();
            CreateMap<Donation, CreateDonationModelView>().ReverseMap();
            CreateMap<Donation, UpdateDonationModelView>().ReverseMap();

        }
    }
}
