using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YK.Core;
using YK.Interfaces.Systems;
using YK.Core;

namespace YK.Services.Systems
{
    internal static class Main
    {
        public static void Init()
        {
            ServiceContainer.Register<SysOrganizationsService, ISysOrganizations>();
            ServiceContainer.Register<SysOrganizationsService, ISysOrganizationModules>();
            ServiceContainer.Register<SysOrganizationsService, ISysOrganizationDataBase>();
            ServiceContainer.Register<SysOrganizationsService, ISysModules>();
            ServiceContainer.Register<SysOrganizationsService, ISysAcions>();
            ServiceContainer.Register<SysOrganizationsService, ISysUser>();
            ServiceContainer.Register<SysOrganizationsService, ISysPages>();
        }
    }
}
