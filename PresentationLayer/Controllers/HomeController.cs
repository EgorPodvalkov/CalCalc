using BusinessLogicLayer.Services;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Diagnostics;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly DailyUserInfoService _dailyUserInfoService;
        private readonly UserService _userService;
        private readonly DishService _dishService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger,
            DailyUserInfoService dailyUserInfoService,
            UserService userService,
            DishService dishService)
        {
            _logger = logger;
            _dailyUserInfoService = dailyUserInfoService;
            _userService = userService;
            _dishService = dishService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {

            // User Service
            await _userService.AddUser(new UserModel()
            {
                Ip = "123411234"
            });

            var users = await _userService.GetUsers();
            Console.WriteLine($"Count: {users.Count}");

            var user = await _userService.GetUserById(1);
            Console.WriteLine($"Ip: {user.Ip}");

            // Dish Service
            var cheesecake = new DishModel()
            {
                Name = "cheesecake",
                KCalorie = 314,
                ServingSize = "100g",
                TotalFat = "22.6g",
                SaturatedFat = "9.9g",
                Protein = "5.5g",
                Carbohydrates = "25.5g",
            };
            await _dishService.AddDish(cheesecake);
            var dishes = await _dishService.GetDishes();
            Console.WriteLine($"Dishes - {dishes.Count}");

            // DailyUserInfo
            //await _dailyUserInfoService.CreateDailyUserInfo(2, DateTime.Today);
            await _dailyUserInfoService.CreateDailyUserInfo(1, DateTime.Today);

            var info = await _dailyUserInfoService.GetUserInfo(1);
            Console.WriteLine($"Info - {info.Count}");

            await _dailyUserInfoService.AddDishToUser(1, cheesecake);

            var todayInfo = await _dailyUserInfoService.GetTodayUserInfo(1);
            Console.WriteLine($"Calorie - {todayInfo.KCalorieReal}");

            Console.WriteLine(await _dailyUserInfoService.RemoveDishFromUser(1, 0));

            todayInfo = await _dailyUserInfoService.GetTodayUserInfo(1);
            Console.WriteLine($"Calorie - {todayInfo.KCalorieReal}");


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}