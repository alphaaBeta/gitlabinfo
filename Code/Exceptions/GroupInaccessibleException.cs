using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Code.Exceptions
{
    public class GroupInaccessibleException : Exception
    {
        public GroupInaccessibleException(string msg) : base(msg)
        {
        }
    }
}
