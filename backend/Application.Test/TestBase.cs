using Application.Common.Interfaces;
using Application.Common.Models;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test
{
    public class TestBase : IDisposable
    {
        public ApplicationDbContext Context { get; }

        public TestBase()
        {
            Context = ApplicationDbContextFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(Context);
        }
    }
}
