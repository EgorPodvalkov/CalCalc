using AutoMapper;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers
{
    public class TodayInfoController : Controller
    {
        private readonly IDailyUserInfoService _dailyUserInfoService;
        private readonly IUserService _userService;
        private readonly IDishService _dishService;
        private readonly IMapper _mapper;

        private readonly ILogger<TodayInfoController> _logger;

        public TodayInfoController(
            ILogger<TodayInfoController> logger,
            IDailyUserInfoService dailyUserInfoService,
            IUserService userService,
            IDishService dishService,
            IMapper mapper)
        {
            _logger = logger;
            _dailyUserInfoService = dailyUserInfoService;
            _userService = userService;
            _dishService = dishService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Info()
        {
            // Getting Ip
            var ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            
            // Getting User Info
            var user = await _userService.GetOrCreateUserByIpAsync(ip);
            var info = await _dailyUserInfoService.GetUserInfoAsync(user);
            var todayInfo = info.First(info => info.Date == DateTime.Today);

            // Rendering Page with Info
            return View(_mapper.Map<DailyUserInfoDTO>(todayInfo));
        }
        
        [HttpGet("/GetUserInfo")]
        public async Task<IActionResult> GetInfo()
        {
            // Getting Ip
            var ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            
            // Getting User Info
            var user = await _userService.GetOrCreateUserByIpAsync(ip);
            var info = await _dailyUserInfoService.GetUserInfoAsync(user);
            var todayInfo = info.First(info => info.Date == DateTime.Today);

            // Rendering Page with Info
            return Ok(_mapper.Map<DailyUserInfoDTO>(todayInfo));
        }

        [HttpPut("/SetGoal")]
        public async Task<IActionResult> SetGoal()
        {
            // Getting Goal
            var goal = int.Parse(Request.Headers["goal"]);
            
            // Getting Ip
            var ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString();

            // Getting User Info
            var user = await _userService.GetOrCreateUserByIpAsync(ip);
            await _dailyUserInfoService.ChangeTodayGoalAsync(user.Id, goal);

            return Ok(goal);
        }

        [HttpPost("/AddDish")]
        public async Task<IActionResult> AddDish()
        {
            // Getting dish Id
            var dishId = int.Parse(Request.Headers["dishId"]);

            // Getting User
            var ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            var user = await _userService.GetOrCreateUserByIpAsync(ip);

            // Adding dish
            await _dailyUserInfoService.AddDishAsync(user.Id, dishId);

            var dish = await _dishService.GetByIdAsync(dishId);
            return Ok(dish.Name);
        }

        [HttpDelete("/RemoveDish/{index}")]
        public async Task<IActionResult> RemoveDish(int index)
        {
            // Getting User
            var ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            var user = await _userService.GetOrCreateUserByIpAsync(ip);

            // Adding dish
            await _dailyUserInfoService.RemoveDishAsync(user.Id, index);

            return Ok();
        }
    }
}
