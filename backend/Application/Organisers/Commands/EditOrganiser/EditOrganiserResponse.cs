using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Organisers.Commands.EditOrganiser
{
    public class EditOrganiserResponse : Result
    {
        public EditOrganiserResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }
    }
}
