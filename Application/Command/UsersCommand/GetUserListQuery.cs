using Domains.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.UsersCommand
{
    public class GetUserListQuery : IRequest<List<User>>
    {
    }
}
