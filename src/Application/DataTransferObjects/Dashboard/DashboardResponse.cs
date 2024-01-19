namespace Application.DataTransferObjects.Dashboard;

public class DashboardResponse
{
    public int CountFilm { get; set; }
    public int CountCustomer { get; set; }
    public int CountSeatSell { get; set; }
    public double TotalPrice { get; set; }
}