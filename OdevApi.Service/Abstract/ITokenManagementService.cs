using OdevApi.Dto;
using OdevApi.Base;

namespace OdevApi.Service.Abstract;

public interface ITokenManagementService
{
    BaseResponse<TokenResponse> GenerateToken(TokenRequest request);
}
