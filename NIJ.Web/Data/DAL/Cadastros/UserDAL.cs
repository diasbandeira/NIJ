using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIJ.Web.Data.DAL.Cadastros
{
    public class UserDAL
    {
        private IESContext _context;
        public UserDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetUsersByName()
        {
            return _context.Users.OrderBy(u => u.Name);
        }
    }
}
