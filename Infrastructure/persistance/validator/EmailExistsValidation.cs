using Domains.repository;
using Infrastructure.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.persistance.validator
{
    public class EmailExistsValidation : IEmailExistsValidator
    {
        private readonly CQRSDbContext _context;
        public EmailExistsValidation(CQRSDbContext context)
        {
            _context = context;
        }
        public bool UserExists(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }
    }
}
