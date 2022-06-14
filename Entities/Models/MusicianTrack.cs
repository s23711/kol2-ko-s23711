namespace Musicians.Entities.Models;

public sealed class MusicianTrack
{
    public int IdTrack { get; set; }
    public int IdMusician { get; set; }
    public Musician Musician { get; set; }
    public Track Track { get; set; }
}