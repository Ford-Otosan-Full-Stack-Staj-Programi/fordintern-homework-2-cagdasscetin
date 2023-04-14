using AutoMapper;
using OdevApi.Data;
using OdevApi.Dto;

namespace OdevApi.Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<PersonDto, Person>();

        CreateMap<Account, AccountDto>();
        CreateMap<AccountDto, Account>();
    }
}
