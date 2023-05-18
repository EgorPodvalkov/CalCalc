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

        public async Task<IActionResult> Info()
        {
            var dishes = _mapper.Map<ICollection<DishDTO>>(await _dishService.GetDishes());
            return View(dishes);
        }
    }
}
