﻿namespace DataAccessLayer.Entities;

public class User : BaseEntity
{
    public string Ip { get; set; }
    public DateTime RegistrationDate { get; set; }
    public virtual ICollection<DailyUserInfo> DailyUsersInfo { get; set; }
}
