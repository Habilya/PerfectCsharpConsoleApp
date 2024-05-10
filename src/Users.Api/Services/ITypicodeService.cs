using Users.Api.Models.Typicode;

namespace Users.Api.Services
{
	public interface ITypicodeService
	{
		Task<IEnumerable<User>> GetAllUsersAsync();
	}
}