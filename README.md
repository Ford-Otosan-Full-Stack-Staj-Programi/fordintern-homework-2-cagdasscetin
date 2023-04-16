# Patika .Net Homework



### TokenController

Token Controller ile Token Management Service'e ulaşıp giriş yaptğımız kullanıcımız ile bir token oluşturuyoruz.

```c#
using OdevApi.Dto;
using OdevApi.Service.Abstract;
using OdevApi.Base;
using Microsoft.AspNetCore.Mvc;

namespace OdevApi.Web;

[Route("Odev/api/v1.0/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenManagementService tokenManagementService;
    public TokenController(ITokenManagementService tokenManagementService)
    {
        this.tokenManagementService = tokenManagementService;
    }

    [HttpPost]
    public BaseResponse<TokenResponse> Login([FromBody] TokenRequest request)
    {
        var response = tokenManagementService.GenerateToken(request);
        return response;
    }
}
```

Controller'larımızdaki hiçbir metodumuz yetkisiz çağırılmadığı için token almak zorundayız. Yetkisiz çağırılabilen tek method Token oluşturma metodudur.



### AccountController

Account controller üzerindeki Post metodu sadece Admin rolündeki kullanıcılar tarafından kullanılabilir.

```c#
[HttpPost]
[Authorize(Roles = Role.Admin)]
public BaseResponse<bool> Post([FromBody] AccountDto request)
{
    Log.Debug("AccountController.Post");
    var response = service.Insert(request);
    return response;
}
```

Bunun dışındaki metodlar aşağıda görülebileceği üzere ya herkese açıktır ya da admin ve editöre birlikte açıktır. Bu şekilde tasarlanmasının sebebi normal user olanların veriler üzerinde değiştirme veya silme yapmasını engellemektir.

```c#
[HttpGet("GetById/{id}")]
[Authorize]
public BaseResponse<AccountDto> GetById(int id)
{
    Log.Debug("AccountController.GetById");
    var response = service.GetById(id);
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
```



### PersonController

Kullanıcı tokeniyle giriş yaptıktan sonra PersonController içerisinde GetAll metodu dışındaki metotlarda sadece kendi accountId sine sahip olanlara erişebilir. Yani sadece kendi post ettiği personları görebilir.

```c#
[HttpGet("GetPeople")]
[Authorize]
public BaseResponse<List<PersonDto>> GetPeople()
{
    Log.Debug("PersonController.GetPeople");
    var accountId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
    var response = service.GetPeople(int.Parse(accountId));
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
```

Bütün kullanıcıların yani accountların sadece kendi personlarına erişimi vardır. Giriş yapmış olan kullanıcı buradaki GetPeople metodunda sadece kendi accountId sine sahip olan personları listeleyebilir. Put metodunda ise id sini girmiş olduğu personun accountId si ile kendi id si aynı ise person üzerinde değişiklik yapabilir, farklı ise yapamaz. Böylelikle kullanıcının id sine göre filtreleme yapılmış olur.
