namespace Musicians.Entities.Models;

public sealed class Track
{
    public int IdTrack { get; set; }
    public string TrackName { get; set; }
    public double Duration { get; set; }
    public int? IdMusicAlbum { get; set; }
    public ICollection<MusicianTrack> MusicianTracks { get; set; }
    public Album Album { get; set; }
}