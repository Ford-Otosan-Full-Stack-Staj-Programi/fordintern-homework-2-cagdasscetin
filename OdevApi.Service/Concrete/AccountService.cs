using AutoMapper;
using OdevApi.Data;
using OdevApi.Dto;
using OdevApi.Service.Abstract;
using OdevApi.Service.Base;
using OdevApi.Base;

namespace OdevApi.Service.Concrete;

public class AccountService : BaseService<AccountDto, Account>, IAccountService
{
    private readonly IMapper mapper;
    private readonly IGenericRepository<Account> genericRepository;
    public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Account> genericRepository) : base(unitOfWork, mapper, genericRepository)
    {
        this.mapper = mapper;
        this.genericRepository = genericRepository;
    }

    public BaseResponse<AccountDto> GetByUsername(string username)
    {
        var account = genericRepository.Where(x => x.UserName == username).FirstOrDefault();
        var mapped = mapper.Map<Account, AccountDto>(account);
        return new BaseResponse<AccountDto>(mapped);
    }

}
