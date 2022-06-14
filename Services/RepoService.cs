using Microsoft.EntityFrameworkCore;
using Musicians.Entities;
using Musicians.Entities.Models;


namespace Musicians.Services;

public class RepoService : IRepoService
{
    private readonly RepositoryContext _repository;
    
    public RepoService(RepositoryContext repository)
    {
        _repository = repository;
    }
    
    public IQueryable<Musician> GetMusicianById(int id)
    {
        return _repository.Musicians
            .Where(m=>m.IdMusician == id)
            .Include(m=> m.MusicianTracks)
            .ThenInclude(mt=> mt.Track)
            .ThenInclude(t=>t.Album)
            .ThenInclude(a=> a.MusicLabel);
    }

    public IQueryable<Musician> DoesMusicianExist(int id)
    {
        return _repository.Musicians
            .Where(e=>e.IdMusician == id);
    }

    public async Task DeleteMusicianById(int id)
    {
        await using var transaction = await _repository.Database.BeginTransactionAsync();
        try
        {
            var musician = await _repository.Musicians
                .Where(e=>e.IdMusician == id)
                .FirstOrDefaultAsync();
            var musicianTracks = await _repository.MusicianTracks
                .Where(e=>e.IdMusician == id)
                .ToListAsync();
            
            if (musician != null)
            {
                foreach (var musicianTrack in musicianTracks)
                {
                    _repository.MusicianTracks.Remove(musicianTrack);
                    await SaveChangesAsync();
                }
                _repository.Musicians.Remove(musician);
                await SaveChangesAsync();
            }
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }
    
    public IQueryable<Musician> AreTracksOnAlbum(int id)
    {
        return _repository.Musicians
            .Where(e=>e.IdMusician == id)
            .Where(e=> e.MusicianTracks.Any(t=> t.Track.IdMusicAlbum != null));
    }
    
    public async Task SaveChangesAsync()
    {
        await _repository.SaveChangesAsync();
    }
}