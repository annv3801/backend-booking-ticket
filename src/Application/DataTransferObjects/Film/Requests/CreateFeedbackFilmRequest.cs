using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.DataTransferObjects.Film.Requests;

public class CreateFeedbackFilmRequest
{
    public long FilmId { get; set; }
    public int Rating { get; set; }
}