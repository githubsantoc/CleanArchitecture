using AutoMapper;
using Domains.Entities;
using Domains.repository;
using Infrastructure.context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.persistance
{
    public class UserRepo : IUserRepo
    {
        private readonly CQRSDbContext _context;
        private readonly IMapper _mapper;
        public UserRepo(CQRSDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<User>> ToListAs()
        {
            var user = await _context.users.ToListAsync();
            var appUser = _mapper.Map<List<User>>(user);
            return appUser;
        }
    }
}
