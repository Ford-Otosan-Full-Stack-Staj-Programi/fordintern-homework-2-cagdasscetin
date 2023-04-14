using AutoMapper;
using OdevApi.Data;
using OdevApi.Dto;
using OdevApi.Service.Abstract;
using OdevApi.Service.Base;
using OdevApi.Base;

namespace OdevApi.Service.Concrete;

public class PersonService : BaseService<PersonDto, Person>, IPersonService
{
    private readonly IMapper mapper;
    private readonly IGenericRepository<Person> genericRepository;
    public PersonService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Person> genericRepository) : base(unitOfWork, mapper, genericRepository)
    {
        this.genericRepository = genericRepository;
        this.mapper = mapper;
    }

    public override BaseResponse<bool> Insert(PersonDto insertResource)
    {
        if(insertResource.DateOfBirth.AddYears(18) > DateTime.UtcNow)
        {
            return new BaseResponse<bool>("Date of birth was incorrect.");
        }

        return base.Insert(insertResource);
    }

    public BaseResponse<List<PersonDto>> GetPeople(int accountId)
    {
        var people = genericRepository.Where(x => x.AccountId == accountId).ToList();
        var mapped = mapper.Map<List<Person>, List<PersonDto>>(people);
        return new BaseResponse<List<PersonDto>>(mapped);
    }

    public int GetAccountId(int id)
    {
        var entity = genericRepository.GetById(id);
        return entity.AccountId;
    }
}
