namespace BusinessLogicLayer.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Ip { get; set; }
    public DateTime RegistrationDate { get; set; }
    public ICollection<DailyUserInfoModel>? DailyUserInfos { get; set; } = new List<DailyUserInfoModel>();
}
