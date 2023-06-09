﻿using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IDailyUserInfoService : IBaseService<DailyUserInfoModel>
{
    Task<ICollection<DailyUserInfoModel>> GetUserInfoAsync(UserModel user);
    Task ChangeTodayGoalAsync(int userId, int? kCaloryGoal);
    Task AddDishAsync(int userId, int dishId);
    Task RemoveDishAsync(int userId, int dishIndex);
    Task<ChartInfoModel> GetChartInfoAsync(int userId, int days);
}
