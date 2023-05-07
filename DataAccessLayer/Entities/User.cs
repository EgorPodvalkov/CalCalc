namespace DataAccessLayer.Entities;

public class User : BaseEntity
{
    public int Ip { get; set; }

    public virtual ICollection<DailyUserInfo> DailyUsersInfo { get; set; }
}
