namespace BusinessLogicLayer.Models;

public class UserModel
{
    public string Ip { get; set; }
    public DateTime RegistrationDate { get; set; }
    public ICollection<DailyUserInfoModel>? UserInfo { get; set; }
}
