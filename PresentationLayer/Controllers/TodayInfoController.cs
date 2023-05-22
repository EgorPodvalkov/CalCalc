using AutoMapper;
using Azure.Core;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
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
            var dishes = _mapper.Map<ICollection<DishDTO>>(await _dishService.GetAllAsync());

            if (dishes.Count == 0)
            {
                await _dishService.AddDishesAsync("Cheesecake pasta French fries boiled potato salad steak cutlet burger mushroom risotto bread");
            }


            var ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            var user = await _userService.GetOrCreateUserByIpAsync(ip);

            //await _dailyUserInfoService.AddDishAsync(user.Id, 3);

            //await _dailyUserInfoService.RemoveDishAsync(user.Id, 0);

            var info = await _dailyUserInfoService.GetUserInfoAsync(user);
            
            return View(_mapper.Map<ICollection<DailyUserInfoDTO>>(info));
        }
    }
}
