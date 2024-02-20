using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Interfaces
{
    public interface IAdminLogErrores
    {
        void LogError(Exception ex, string className, [CallerMemberName] string methodName = "");
    }
}
