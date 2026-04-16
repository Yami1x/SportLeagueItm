using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

public interface ITournamentSponsorRepository : IGenericRepository<TournamentSponsor>
{
    Task DeleteAsync(TournamentSponsor existingRegistration);
    Task<TournamentSponsor?> GetByTournamentAndSponsorAsync(int tournamentId, int sponsorId);
}