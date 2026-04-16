using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.DataAccess.Repositories;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

public class TournamentSponsorRepository : GenericRepository<TournamentSponsor>, ITournamentSponsorRepository
{
    public TournamentSponsorRepository(LeagueDbContext context) : base(context) { }

    public Task DeleteAsync(TournamentSponsor existingRegistration)
    {
        throw new NotImplementedException();
    }

    public async Task<TournamentSponsor?> GetByTournamentAndSponsorAsync(int tournamentId, int sponsorId)
    {
        return await _dbSet.FirstOrDefaultAsync(ts => ts.TournamentId == tournamentId && ts.SponsorId == sponsorId);
    }
}