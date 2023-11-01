namespace Application.DataTransferObjects.Film.Responses;

public class FeedbackFilmResponse
{
    public long FilmId { get; set; }
    public double AverageRating { get; set; }
    public int CountOneStar { get; set; }
    public int CountTwoStar { get; set; }
    public int CountThreeStar { get; set; }
    public int CountFourStar { get; set; }
    public int CountFiveStar { get; set; }
    public int CountStart { get; set; }
}