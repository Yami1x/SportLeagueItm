using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

public interface ISponsorRepository : IGenericRepository<Sponsor>
{
    Task DeleteAsync(Sponsor existingSponsor);
    Task<bool> ExistsByNameAsync(string name);
}

