using Domain.Dto.User;
using Domain.Dto;
using Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Services
{
    public interface IAuthService
    {
        Task<BaseResult<UserDto>> Register(RegisterUserDto dto);

        Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
    }
}
