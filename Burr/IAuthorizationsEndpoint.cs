using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burr
{
    public interface IAuthorizationsEndpoint
    {
        Task<IEnumerable<Authorization>> GetAllAsync();
    }
}
