using OdevApi.Base;

namespace OdevApi.Service.Base;

public interface IBaseService<Dto, TEntity>
{
    BaseResponse<Dto> GetById(int id);
    BaseResponse<bool> Insert(Dto insertRe
        );
    BaseResponse<bool> Remove(int id);
    BaseResponse<bool> Update(int id, Dto updateResource);
    BaseResponse<List<Dto>> GetAll();
}
