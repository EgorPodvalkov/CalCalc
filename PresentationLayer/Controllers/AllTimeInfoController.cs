using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers;

public class AllTimeInfoController : Controller
{
    private readonly IDailyUserInfoService _dailyUserInfoService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AllTimeInfoController(
        IDailyUserInfoService dailyUserInfoService, 
        IUserService userService,
        IMapper mapper)
    {
        _dailyUserInfoService = dailyUserInfoService;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Chart()
    {
        return View();
    }

    [HttpGet("/GetChartInfo/{days?}")]
    public async Task<IActionResult> GetChartInfo(int days = 30)
    {
        // Getting Ip
        var ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString();

        // Getting User Info
        var user = await _userService.GetOrCreateUserByIpAsync(ip);

        // Getting ChartModel
        var chartModel = await _dailyUserInfoService.GetChartInfoAsync(user.Id, days);

        return Ok(_mapper.Map<ChartInfoDTO>(chartModel));
    }
}
