using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeChat.Domain.Abstractions.Services.User;
public interface IAccountOpeningPolicy
{
    Task CheckCanOpenAsync(string email, string username);
}

