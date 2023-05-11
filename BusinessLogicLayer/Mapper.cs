using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer;

public static class Mapper
{
    // Entities

    // DailyUserInfo
    public static DailyUserInfoModel ToModel(this DailyUserInfo dailyUserInfo)
        => new DailyUserInfoModel()
        {
            Date = dailyUserInfo.Date,
            KCalorieReal = dailyUserInfo.KCalorieReal,
            KCalorieGoal = dailyUserInfo.KCalorieGoal,
            User = dailyUserInfo.User,
            Dishes = dailyUserInfo.Dishes?.ToModelCollection()
        };

    public static ICollection<DailyUserInfoModel> ToModelCollection(this ICollection<DailyUserInfo> collection)
    {
        var modelCollection = new List<DailyUserInfoModel>();
        
        foreach (var dailyUserInfo in collection)
            modelCollection.Add(dailyUserInfo.ToModel());
        
        return modelCollection;
    }

    // User
    public static UserModel ToModel(this User user)
        => new UserModel()
        {
            Ip = user.Ip,
            RegistrationDate = user.RegistrationDate,
            UserInfo = user.DailyUsersInfo?.ToModelCollection(),
        };

    public static ICollection<UserModel> ToModelCollection(this ICollection<User> collection)
    {
        var modelCollection = new List<UserModel>();

        foreach (var user in collection)
            modelCollection.Add(user.ToModel());

        return modelCollection;
    }

    // Dish
    public static DishModel ToModel(this Dish dish)
        => new DishModel()
        {
            Name = dish.Name,
            KCalorie = dish.KCalorie,
            ServingSize = dish.ServingSize,
            TotalFat = dish.TotalFat,
            SaturatedFat = dish.SaturatedFat,
            Carbohydrates = dish.Carbohydrates,
            Protein = dish.Protein,
            Recipe = dish.Recipe,
        };

    public static ICollection<DishModel> ToModelCollection(this ICollection<Dish> collection)
    {
        var modelCollection = new List<DishModel>();

        foreach (var dish in collection)
            modelCollection.Add(dish.ToModel());

        return modelCollection;
    }

    // Models

    // DailyUserInfoModel
    public static DailyUserInfo ToEntity(this DailyUserInfoModel dailyUserInfo)
        => new DailyUserInfo()
        {
            Date = dailyUserInfo.Date,
            KCalorieReal = dailyUserInfo.KCalorieReal,
            KCalorieGoal = dailyUserInfo.KCalorieGoal,
            UserId = dailyUserInfo.User.Id,
            User = dailyUserInfo.User,
            Dishes = dailyUserInfo.Dishes?.ToEntityCollection(),
        };

    public static ICollection<DailyUserInfo> ToEntityCollection(this ICollection<DailyUserInfoModel> collection)
    {
        var entityCollection = new List<DailyUserInfo>();

        foreach (var dailyUserInfo in collection)
            entityCollection.Add(dailyUserInfo.ToEntity());

        return entityCollection;
    }

    // UserModel
    public static User ToEntity(this UserModel user)
        => new User()
        {
            Ip = user.Ip,
            RegistrationDate = user.RegistrationDate,
            DailyUsersInfo = user.UserInfo?.ToEntityCollection()
        };

    public static ICollection<User> ToEntityCollection(this ICollection<UserModel> collection)
    {
        var entityCollection = new List<User>();

        foreach (var user in collection)
            entityCollection.Add(user.ToEntity());

        return entityCollection;
    }

    // DishModel
    public static Dish ToEntity(this DishModel dish)
        => new Dish()
        {
            Name = dish.Name,
            KCalorie = dish.KCalorie,
            ServingSize = dish.ServingSize,
            TotalFat = dish.TotalFat,
            SaturatedFat = dish.SaturatedFat,
            Carbohydrates = dish.Carbohydrates,
            Protein = dish.Protein,
            Recipe = dish.Recipe,
        };

    public static ICollection<Dish> ToEntityCollection(this ICollection<DishModel> collection)
    {
        var entityCollection = new List<Dish>();

        foreach (var dish in collection)
            entityCollection.Add(dish.ToEntity());

        return entityCollection;
    }
}
