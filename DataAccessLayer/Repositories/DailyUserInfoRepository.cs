using DataAccessLayer.DbStartUp;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class DailyUserInfoRepository : BaseRepository<DailyUserInfo>, IDailyUserInfoRepository
{
    public DailyUserInfoRepository(CalCalcContext calCalcContext) : base(calCalcContext) { }
}
