using Domain.Entities;
using Infrastructure.BaseRequest;
using Infrastructure.Mapper;

namespace Infrastructure.Commands
{
	public class RegistrationUserCommand : BaseRequest<User>, IViewModel
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }

		public void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<User, RegistrationUserCommand>().ReverseMap();
		}
	}
}
