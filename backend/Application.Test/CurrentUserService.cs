using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test
{
    public class CurrentUserService : ICurrentUserService
    {
        private string _userId;
        public CurrentUserService(string userId)
        {
            this._userId = userId;
        }
        public string UserId => _userId;
    }
}
