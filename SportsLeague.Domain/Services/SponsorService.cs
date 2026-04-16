using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SponsorService : ISponsorService
{
    private readonly ISponsorRepository _sponsorRepository;

    public SponsorService(ISponsorRepository sponsorRepository)
    {
        _sponsorRepository = sponsorRepository;
    }

    public async Task<Sponsor> CreateAsync(Sponsor sponsor)
    {
        if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
        {
            throw new InvalidOperationException("El nombre del patrocinador ya existe.");
        }
        await _sponsorRepository.CreateAsync(sponsor);
        return sponsor;
    }

    public async Task<Sponsor> UpdateAsync(int id, Sponsor sponsor)
    {
        var existingSponsor = await _sponsorRepository.GetByIdAsync(id);
        if (existingSponsor == null)
        {
            throw new KeyNotFoundException("El patrocinador no existe.");
        }

        existingSponsor.Name = sponsor.Name;
        existingSponsor.ContactEmail = sponsor.ContactEmail;
        existingSponsor.Phone = sponsor.Phone;
        existingSponsor.WebsiteUrl = sponsor.WebsiteUrl;
        existingSponsor.Category = sponsor.Category;
        existingSponsor.UpdatedAt = DateTime.UtcNow;

        await _sponsorRepository.UpdateAsync(existingSponsor);
        return existingSponsor;
    }

    public async Task DeleteAsync(int id)
    {
        var existingSponsor = await _sponsorRepository.GetByIdAsync(id);
        if (existingSponsor == null)
        {
            throw new KeyNotFoundException("El patrocinador no existe.");
        }
        await _sponsorRepository.DeleteAsync(existingSponsor);
    }

    public async Task<Sponsor> GetByIdAsync(int id)
    {
        var sponsor = await _sponsorRepository.GetByIdAsync(id);
        if (sponsor == null)
        {
            throw new KeyNotFoundException("El patrocinador no existe.");
        }
        return sponsor;
    }

    public async Task<IEnumerable<Sponsor>> GetAllAsync()
    {
        return await _sponsorRepository.GetAllAsync();
    }
}