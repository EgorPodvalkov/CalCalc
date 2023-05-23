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

        [HttpPut]
        [Route("/SetGoal")]
        public async Task<IActionResult> SetGoal()
        {
            // Getting Goal
            var goal = int.Parse(Request.Headers["goal"]);
            
            // Getting Ip
            var ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString();

            // Getting User Info
            var user = await _userService.GetOrCreateUserByIpAsync(ip);
            await _dailyUserInfoService.ChangeTodayGoalAsync(user.Id, goal);
            return StatusCode(200);
        }
    }
}
