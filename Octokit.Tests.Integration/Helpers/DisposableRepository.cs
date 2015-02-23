using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    public class DisposableRepository : Repository, IDisposable
    {
        public static DisposableRepository InitFromRepository(Repository repository)
        {
            DisposableRepository result = new DisposableRepository();
            foreach (System.Reflection.PropertyInfo prop in repository.GetType().GetProperties())
            {
                var value = prop.GetValue(repository);
                prop.SetValue(result, value);
            }
            return result;
        }

        public void Dispose()
        {
            Helper.DeleteRepo(this as Repository);
        }
    }
}
