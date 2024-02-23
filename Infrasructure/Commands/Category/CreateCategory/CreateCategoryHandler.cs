using AutoMapper;
using Infrastructure.Database;
using Domain.Entities;
using Infrastructure.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace Infrastructure.Commands
{
	public class CreateCategoryHandler : BaseCreateCommandHandler<CreateCategoryCommand, Category>
	{
		private readonly IUserImageService _userImageService;

		public CreateCategoryHandler(ApplicationContext applicationContext, IMapper mapper, IUserImageService userImageService)
			: base(applicationContext, mapper)
		{
			_userImageService = userImageService;
		}

		protected override async Task<Category> AfterSave(Category result, CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			await _userImageService.AttachImage(result, request.UserId, request.Src, cancellationToken);

			return await base.AfterSave(result, request, cancellationToken);
		}
	}
}
