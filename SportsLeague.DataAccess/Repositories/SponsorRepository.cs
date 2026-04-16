using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.DataAccess.Repositories;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

public class SponsorRepository : GenericRepository<Sponsor>, ISponsorRepository
{
    public SponsorRepository(LeagueDbContext context) : base(context) { }

    public Task DeleteAsync(Sponsor existingSponsor)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbSet.AnyAsync(s => s.Name.ToLower() == name.ToLower());
    }
}