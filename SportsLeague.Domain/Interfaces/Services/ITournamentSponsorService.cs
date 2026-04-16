using SportsLeague.Domain.Entities;

public interface ITournamentSponsorService
{
    Task<TournamentSponsor> RegisterSponsorAsync(int tournamentId, int sponsorId, decimal contractAmount);
    Task DeleteAsync(int tournamentId, int sponsorId);
    Task<IEnumerable<TournamentSponsor>> GetByTournamentAsync(int tournamentId);
}