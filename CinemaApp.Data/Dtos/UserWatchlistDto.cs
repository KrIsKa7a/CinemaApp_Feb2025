namespace CinemaApp.Data.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static Common.Constants.EntityConstants.ApplicationUser;

    [XmlType("User")]
    public class UserWatchlistDto
    {
        [Required]
        [MinLength(UsernameMinLength)]
        [MaxLength(UsernameMaxLength)]
        [XmlAttribute(nameof(Username))]
        public string Username { get; set; } = null!;

        [Required]
        [XmlArray(nameof(Movies))]
        public UserWatchlistMovieDto[] Movies { get; set; } = null!;
    }
}
