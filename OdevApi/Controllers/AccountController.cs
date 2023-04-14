using OdevApi.Dto;
using OdevApi.Service.Abstract;
using OdevApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace OdevApi.Web.Controllers;

[Route("Odev/api/v1.0/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService service;
    public AccountController(IAccountService service)
    {
        this.service = service;
    }


    [HttpGet("GetAll")]
    [Authorize]
    public BaseResponse<List<AccountDto>> GetAll()
    {
        Log.Debug("AccountController.GetAll");
        var response = service.GetAll();
        return response;
    }

    [HttpGet("GetUserDetail")]
    [Authorize]
    public BaseResponse<AccountDto> GetUserDetail()
    {
        Log.Debug("AccountController.GetUserDetail");
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var response = service.GetById(int.Parse(id));
        return response;
    }
    
    [HttpGet("GetById/{id}")]
    [Authorize]
    public BaseResponse<AccountDto> GetById(int id)
    {
        Log.Debug("AccountController.GetById");
        var response = service.GetById(id);
        return response;
    }

    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    public BaseResponse<bool> Post([FromBody] AccountDto request)
    {
        Log.Debug("AccountController.Post");
        var response = service.Insert(request);
        return response;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = $"{Role.Admin},{Role.Editor}")]
    public BaseResponse<bool> Put(int id, [FromBody] AccountDto request)
    {
        Log.Debug("AccountController.Put");
        request.Id = id;
        var response = service.Update(id, request);
        return response;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{Role.Admin},{Role.Editor}")]
    public BaseResponse<bool> Delete(int id)
    {
        Log.Debug("AccountController.Delete");
        var response = service.Remove(id);
        return response;
    }
}
