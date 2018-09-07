using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YK.Common
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class ControllerAssemblyAttribute : Attribute
    {
    }
}