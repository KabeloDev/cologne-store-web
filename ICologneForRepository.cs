using CologneStore.Models;

namespace CologneStore.Repositories
{
	public interface ICologneForRepository
	{
		Task<IEnumerable<CologneFor>> GetAllColognesFor();
	}
}
