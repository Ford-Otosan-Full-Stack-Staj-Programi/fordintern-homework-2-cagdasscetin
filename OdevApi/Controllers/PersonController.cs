using OdevApi.Dto;
using OdevApi.Service.Abstract;
using OdevApi.Base;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;
using OdevApi.Data;

namespace OdevApi.Web.Controllers;

[Route("Odev/api/v1.0/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService service;
    private readonly IMapper mapper;
    public PersonController(IPersonService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }


    [HttpGet("GetAll")]
    [Authorize]
    public BaseResponse<List<PersonDto>> GetAll()
    {
        Log.Debug("PersonController.GetAll");
        var response = service.GetAll();
        return response;
    }

    [HttpGet("{id}")]
    [Authorize]
    public BaseResponse<PersonDto> GetById(int id)
    {
        Log.Debug("PersonController.GetById");
        var personAccountId = service.GetAccountId(id);
        var accountId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
        if (personAccountId == int.Parse(accountId))
        {
            var response = service.GetById(id);
            return response;
        }
        return new BaseResponse<PersonDto>(false);
    }
    
    [HttpGet("GetPeople")]
    [Authorize]
    public BaseResponse<List<PersonDto>> GetPeople()
    {
        Log.Debug("PersonController.GetPeople");
        var accountId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
        var response = service.GetPeople(int.Parse(accountId));
        return response;
    }

    [HttpPost]
    [Authorize]
    public BaseResponse<bool> Post([FromBody] PersonDto request)
    {
        Log.Debug("PersonController.Post");
        var accountId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
        request.AccountId = int.Parse(accountId);
        var response = service.Insert(request);
        return response;
    }

    [HttpPut("{id}")]
    [Authorize]
    public BaseResponse<bool> Put(int id, [FromBody] PersonDto request)
    {
        Log.Debug("PersonController.Put");

        var personAccountId = service.GetAccountId(id);
        var accountId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
        if (personAccountId == int.Parse(accountId))
        {
            request.Id = id;
            request.AccountId = int.Parse(accountId);
            var response = service.Update(id, request);
            return response;
        }
        return new BaseResponse<bool>(false);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public BaseResponse<bool> Delete(int id)
    {
        //Person person = this.service.PersonRepository.GetById(id);
        Log.Debug("PersonController.Delete");
        var personAccountId = service.GetAccountId(id);
        var accountId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
        if(personAccountId == int.Parse(accountId))
        {
            var response = service.Remove(id);
            return response;
        }
        return new BaseResponse<bool>(false);
    }
}
