using Microsoft.EntityFrameworkCore;
using Musicians.Entities.Models;

namespace Musicians.Entities;

public class RepositoryContext : DbContext
{
    public DbSet<Musician> Musicians { get; set; }
    public DbSet<MusicianTrack> MusicianTracks { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<MusicLabel> MusicLabels { get; set; }
    
    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Musician>(builder =>
        {
            builder.ToTable("Musician");
            builder.HasKey(e => e.IdMusician);
            
            builder.Property(e=> e.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(e=> e.LastName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Nickname).HasMaxLength(20);
            builder.HasData(
                new Musician
                {
                    IdMusician = 1,
                    FirstName = "Solange",
                    LastName = "Knowles",   
                    Nickname = "Solo"
                },
                new Musician
                {
                    IdMusician = 2,
                    FirstName = "John",
                    LastName = "Lennon",
                    Nickname = "The Beatle"
                },
                new Musician {
                    IdMusician = 3,
                    FirstName = "Paul",
                    LastName = "McCartney"
                }
            );
        });
        
        modelBuilder.Entity<MusicianTrack>(builder =>
        {
            builder.ToTable("Musician_Track");
            builder.HasKey(e => new {e.IdTrack, e.IdMusician});

            builder.HasOne(e => e.Track)
                .WithMany(e => e.MusicianTracks)
                .HasForeignKey(e => e.IdTrack);
            builder.HasOne(e => e.Musician)
                .WithMany(e => e.MusicianTracks)
                .HasForeignKey(e => e.IdMusician);

            builder.HasData(
                new MusicianTrack
                {
                    IdTrack = 1,
                    IdMusician = 2,
                },
                new MusicianTrack
                {
                    IdTrack = 2,
                    IdMusician = 1,
                }, 
                new MusicianTrack
                {
                    IdTrack = 3,
                    IdMusician = 2,
                },
                new MusicianTrack
                {
                    IdTrack = 3,
                    IdMusician = 3,
                },
                new MusicianTrack
                {
                    IdTrack = 4,
                    IdMusician = 1,
                },
                new MusicianTrack
                {
                    IdTrack = 5,
                    IdMusician = 1,
                },
                new MusicianTrack
                {
                    IdTrack = 3,
                    IdMusician = 1,
                },
                new MusicianTrack
                {
                    IdTrack = 6,
                    IdMusician = 3,
                }
            );    
        });
        
        modelBuilder.Entity<Track>(builder =>
        {
            builder.ToTable("Track");
            builder.HasKey(e => e.IdTrack);
            
            builder.Property(e=> e.TrackName).HasMaxLength(20).IsRequired();
            builder.Property(e=> e.Duration).IsRequired();
            
            builder.HasOne(e => e.Album)
                .WithMany(e => e.Tracks)
                .HasForeignKey(e => e.IdMusicAlbum);

            builder.HasData(
                new Track
                {
                    IdTrack = 1,
                    TrackName = "The Beatles",
                    Duration = 3.59,   
                    IdMusicAlbum = 1
                },
                new Track
                {
                    IdTrack = 2,
                    TrackName = "XoXo",
                    Duration = 2.14,   
                    IdMusicAlbum = 2
                },
                new Track
                {
                    IdTrack = 3,
                    TrackName = "The Bottles",
                    Duration = 1.00
                },
                new Track
                {
                    IdTrack = 4,
                    TrackName = "Sound of rain",
                    Duration = 2.37,   
                    IdMusicAlbum = 3
                },
                new Track
                {
                    IdTrack = 5,
                    TrackName = "Almeda",
                    Duration = 1.00,   
                    IdMusicAlbum = 3
                },
                new Track
                {
                    IdTrack = 6,
                    TrackName = "Trash",
                    Duration = 0.01
                }
            );
        });
        
        modelBuilder.Entity<Album>(builder =>
        {
            builder.ToTable("Album");
            builder.HasKey(e => e.IdAlbum);
            
            builder.Property(e=> e.AlbumName).HasMaxLength(30).IsRequired();
            builder.Property(e=> e.PublishDate);
            
            builder.HasOne(e => e.MusicLabel)
                .WithMany(e => e.Albums)
                .HasForeignKey(e => e.IdMusicLabel);

            builder.HasData(
                new Album 
                {
                    IdAlbum = 1,
                    AlbumName = "The Beatles",
                    PublishDate = new DateTime(1962, 6, 16),
                    IdMusicLabel = 1
                },
                new Album 
                {
                    IdAlbum = 2,
                    AlbumName = "Pussycats",
                    IdMusicLabel = 2
                },
                new Album 
                {
                    IdAlbum = 3,
                    AlbumName = "When I get home",
                    PublishDate = new DateTime(2019, 2, 1),
                    IdMusicLabel = 3
                }
            );    
        });
        
        modelBuilder.Entity<MusicLabel>(builder =>
        {
            builder.ToTable("MusicLabel");
            builder.HasKey(e => e.IdMusicLabel);
            
            builder.Property(e=> e.Name).HasMaxLength(50).IsRequired();
            
            builder.HasData(
                new MusicLabel
                {
                    IdMusicLabel = 1,
                    Name = "Beatles"
                },
                new MusicLabel
                {
                    IdMusicLabel = 2,
                    Name = "Kitties"
                },
                new MusicLabel
                {
                    IdMusicLabel = 3,
                    Name = "Saint Heron"
                }
            );
        });
    }
}