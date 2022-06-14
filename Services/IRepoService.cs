using Musicians.Entities.Models;

namespace Musicians.Services;

public interface IRepoService
{
    IQueryable<Musician> GetMusicianById(int id);
    IQueryable<Musician> DoesMusicianExist(int id);
    Task DeleteMusicianById(int id);
    IQueryable<Musician> AreTracksOnAlbum(int id);
    Task SaveChangesAsync();
}