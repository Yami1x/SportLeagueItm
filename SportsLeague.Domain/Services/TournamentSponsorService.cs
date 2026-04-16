using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TournamentSponsorService : ITournamentSponsorService
{
    private readonly ITournamentSponsorRepository _tournamentSponsorRepository;
    private readonly ISponsorRepository _sponsorRepository;

    public TournamentSponsorService(ITournamentSponsorRepository tournamentSponsorRepository, ISponsorRepository sponsorRepository)
    {
        _tournamentSponsorRepository = tournamentSponsorRepository;
        _sponsorRepository = sponsorRepository;
    }

    public async Task<TournamentSponsor> RegisterSponsorAsync(int tournamentId, int sponsorId, decimal contractAmount)
    {
        // Validaciones
        var sponsorExists = await _sponsorRepository.GetByIdAsync(sponsorId);
        if (sponsorExists == null)
        {
            throw new KeyNotFoundException("El patrocinador no existe.");
        }

        var existingRegistration = await _tournamentSponsorRepository.GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
        if (existingRegistration != null)
        {
            throw new InvalidOperationException("El patrocinador ya está vinculado a este torneo.");
        }

        var tournamentSponsor = new TournamentSponsor
        {
            TournamentId = tournamentId,
            SponsorId = sponsorId,
            ContractAmount = contractAmount,
            JoinedAt = DateTime.UtcNow
        };

        await _tournamentSponsorRepository.CreateAsync(tournamentSponsor);
        return tournamentSponsor;
    }

    public async Task DeleteAsync(int tournamentId, int sponsorId)
    {
        var existingRegistration = await _tournamentSponsorRepository.GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
        if (existingRegistration == null)
        {
            throw new KeyNotFoundException("La vinculación entre el torneo y el patrocinador no existe.");
        }
        await _tournamentSponsorRepository.DeleteAsync(existingRegistration);
    }

    public async Task<IEnumerable<TournamentSponsor>> GetByTournamentAsync(int tournamentId)
    {
        return await _tournamentSponsorRepository.GetAllAsync(); 
    }
}