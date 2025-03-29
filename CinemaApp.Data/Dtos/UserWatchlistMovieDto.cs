namespace CinemaApp.Data.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static Common.Constants.EntityConstants;
    using static Common.Constants.EntityConstants.Movie;

    [XmlType(nameof(Movie))]
    public class UserWatchlistMovieDto
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        [XmlAttribute(nameof(Title))]
        public string Title { get; set; } = null!;
    }
}
