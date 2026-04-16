using SportsLeague.Domain.Entities;

public interface ISponsorService
{
    Task<Sponsor> CreateAsync(Sponsor sponsor);
    Task<Sponsor> UpdateAsync(int id, Sponsor sponsor);
    Task DeleteAsync(int id);
    Task<Sponsor> GetByIdAsync(int id);
    Task<IEnumerable<Sponsor>> GetAllAsync();
}