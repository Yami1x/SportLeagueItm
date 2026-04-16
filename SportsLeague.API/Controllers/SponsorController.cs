// SponsorController.cs
using Microsoft.AspNetCore.Mvc;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SponsorController : ControllerBase
{
    private readonly ISponsorService _sponsorService;
    private readonly ITournamentSponsorService _tournamentSponsorService;
    private readonly IMapper _mapper;

    public SponsorController(ISponsorService sponsorService, ITournamentSponsorService tournamentSponsorService, IMapper mapper)
    {
        _sponsorService = sponsorService;
        _tournamentSponsorService = tournamentSponsorService;
        _mapper = mapper;
    }

    // GET: api/Sponsor
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sponsors = await _sponsorService.GetAllAsync();
        var sponsorDtos = _mapper.Map<IEnumerable<SponsorResponseDTO>>(sponsors);
        return Ok(sponsorDtos);
    }

    // GET: api/Sponsor/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var sponsor = await _sponsorService.GetByIdAsync(id);
        if (sponsor == null) return NotFound();

        var sponsorDto = _mapper.Map<SponsorResponseDTO>(sponsor);
        return Ok(sponsorDto);
    }

    // POST: api/Sponsor
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SponsorRequestDTO sponsorDto)
    {
        var sponsor = _mapper.Map<Sponsor>(sponsorDto);
        var createdSponsor = await _sponsorService.CreateAsync(sponsor);

        var createdSponsorDto = _mapper.Map<SponsorResponseDTO>(createdSponsor);
        return CreatedAtAction(nameof(GetById), new { id = createdSponsorDto.Id }, createdSponsorDto);
    }

    // PUT: api/Sponsor/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SponsorRequestDTO sponsorDto)
    {
        var sponsor = _mapper.Map<Sponsor>(sponsorDto);
        try
        {
            var updatedSponsor = await _sponsorService.UpdateAsync(id, sponsor);
            var updatedSponsorDto = _mapper.Map<SponsorResponseDTO>(updatedSponsor);
            return Ok(updatedSponsorDto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    // DELETE: api/Sponsor/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _sponsorService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    // POST: api/Sponsor/{id}/tournaments
    [HttpPost("{id}/tournaments")]
    public async Task<IActionResult> RegisterSponsorToTournament(int id, [FromBody] TournamentSponsorRequestDTO requestDto)
    {
        try
        {
            var tournamentSponsor = await _tournamentSponsorService.RegisterSponsorAsync(requestDto.TournamentId, id, requestDto.ContractAmount);
            return CreatedAtAction(nameof(RegisterSponsorToTournament), new { tournamentId = tournamentSponsor.TournamentId, sponsorId = tournamentSponsor.SponsorId }, tournamentSponsor);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    // DELETE: api/Sponsor/{id}/tournaments/{tournamentId}
    [HttpDelete("{id}/tournaments/{tournamentId}")]
    public async Task<IActionResult> UnregisterSponsorFromTournament(int id, int tournamentId)
    {
        try
        {
            await _tournamentSponsorService.DeleteAsync(tournamentId, id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    // GET: api/Sponsor/tournaments/{tournamentId}
    [HttpGet("tournaments/{tournamentId}")]
    public async Task<IActionResult> GetSponsorsByTournament(int tournamentId)
    {
        var tournamentSponsors = await _tournamentSponsorService.GetByTournamentAsync(tournamentId);
        var dtos = _mapper.Map<IEnumerable<TournamentSponsorResponseDTO>>(tournamentSponsors);
        return Ok(dtos);
    }
}
