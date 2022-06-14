using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Musicians.DTOs;
using Musicians.Services;

namespace Musicians.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MusicianController : ControllerBase
{
    private readonly IRepoService _service;
    
    public MusicianController(IRepoService service)
    {
        _service = service;
    }
    
    [HttpGet("{idMusician:int}")]
    public async Task<IActionResult> GetMusician(int idMusician)
    {
        if (await _service.GetMusicianById(idMusician).FirstOrDefaultAsync() is null)
            return NotFound("Nie znaleziono muzyka o podanym ID.");
        
        return Ok(
        await _service.GetMusicianById(idMusician)
            .Select(e =>
                new MusicianGet
                {
                    IdMusician = e.IdMusician,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Nickname = e.Nickname,
                    Tracks = e.MusicianTracks.Select(t=> new Tracks
                    {
                        IdTrack = t.IdTrack,
                        TrackName = t.Track.TrackName,
                        Duration = t.Track.Duration,
                        IdMusicAlbum = t.Track.IdMusicAlbum,
                        AlbumName = t.Track.Album.AlbumName,
                        MusicLabel = t.Track.Album.MusicLabel.Name
                    }).OrderBy(t => t.Duration).ToList()}).ToListAsync()
        );
    }

    [HttpDelete("{idMusician:int}")]
    public async Task<IActionResult> DeleteMusician(int idMusician)
    {
        if (await _service.DoesMusicianExist(idMusician).FirstOrDefaultAsync() is null)
            return NotFound("Nie znaleziono muzyka o podanym ID.");
        if (await _service.AreTracksOnAlbum(idMusician).FirstOrDefaultAsync() is not null)
            return BadRequest("Nie można usunąć muzyka, ponieważ utwory, które tworzy są na albumie.");
        
        try
        {
            await _service.DeleteMusicianById(idMusician);
        }
        catch (Exception)
        {
            return Problem("Nieoczekiwany błąd serwera");
        }
        
        await _service.SaveChangesAsync();
        return NoContent();
    }

}