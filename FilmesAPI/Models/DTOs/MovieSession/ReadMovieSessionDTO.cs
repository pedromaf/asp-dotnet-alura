namespace FilmesAPI.Models.DTOs
{
    public class ReadMovieSessionDTO
    {
        public int Id { get; set; }
        public object Movie { get; set; }
        public object MovieTheater { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
