namespace Musicians.DTOs;

public class MusicianGet
{
    public int IdMusician { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Nickname { get; set; }
    public List<Tracks> Tracks { get; set; }
}

public class Tracks
{
    public int IdTrack { get; set; }
    public string TrackName { get; set; }
    public double Duration { get; set; }
    public int? IdMusicAlbum { get; set; }
    public string? AlbumName { get; set; }
    public string? MusicLabel { get; set; }
}