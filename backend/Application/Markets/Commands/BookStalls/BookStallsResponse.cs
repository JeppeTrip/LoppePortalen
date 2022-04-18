using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Markets.Commands.BookStalls
{
    public class BookStallsResponse : Result
    {
        internal BookStallsResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }
    }
}
