using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Security.Test
{
    /**
     * Does nothing just trying to figure out how the policy handlers work.
     */
    public class TestRequirement : IAuthorizationRequirement
    {
        public string Test { get; }
        public TestRequirement(string test) => Test = test; 
    }
}
