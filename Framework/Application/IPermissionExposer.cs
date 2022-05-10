using System.Collections.Generic;

namespace Framework.Application
{
    public interface IPermissionExposer
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }
}
