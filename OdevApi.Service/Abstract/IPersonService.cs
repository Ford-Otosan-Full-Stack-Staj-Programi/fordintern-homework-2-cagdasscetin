using OdevApi.Base;
using OdevApi.Data;
using OdevApi.Dto;
using OdevApi.Service.Base;

namespace OdevApi.Service.Abstract;

public interface IPersonService : IBaseService<PersonDto, Person>
{
    BaseResponse<List<PersonDto>> GetPeople(int accountId);
    int GetAccountId(int id);
}
