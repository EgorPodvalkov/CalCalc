namespace PresentationLayer.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string Ip { get; set; }
    public DateTime RegistrationDate { get; set; }
    public ICollection<DailyUserInfoDTO>? DailyUserInfos { get; set; } = new List<DailyUserInfoDTO>();
}
