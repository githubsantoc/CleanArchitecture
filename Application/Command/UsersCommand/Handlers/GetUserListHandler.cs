using Application.Wrapper;
using Domains.Entities;
using Domains.repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.UsersCommand.Handlers
{
    public class GetUserListHandler : IRequestHandler<GetUserListQuery, List<User>>
    {
        private readonly IUserWrapper _userManager;
        public GetUserListHandler(IUserWrapper userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<User>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.ToListAs();
        }
    }
}
