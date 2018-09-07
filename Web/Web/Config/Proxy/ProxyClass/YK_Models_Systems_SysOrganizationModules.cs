using System;

namespace YK.Common.Proxy.GenerateClass.YK_Models_Systems_SysOrganizationModules
{
    /// <summary>
    /// 生成代码类
    /// </summary>
    public class SysOrganizationModules:YK.Models.Systems.SysOrganizationModules
    {
		public override Int32 ID 
{
get { if (ChanageProperty.ContainsKey("ID") == false) { return default(Int32); } else { return (Int32)ChanageProperty["ID"]; } } 
set { ChanageProperty.Add("ID",value); } 
}
public override Int32 ModuleID 
{
get { if (ChanageProperty.ContainsKey("ModuleID") == false) { return default(Int32); } else { return (Int32)ChanageProperty["ModuleID"]; } } 
set { ChanageProperty.Add("ModuleID",value); } 
}
public override Int32 OrganizationID 
{
get { if (ChanageProperty.ContainsKey("OrganizationID") == false) { return default(Int32); } else { return (Int32)ChanageProperty["OrganizationID"]; } } 
set { ChanageProperty.Add("OrganizationID",value); } 
}
public override Int32 CreaterID 
{
get { if (ChanageProperty.ContainsKey("CreaterID") == false) { return default(Int32); } else { return (Int32)ChanageProperty["CreaterID"]; } } 
set { ChanageProperty.Add("CreaterID",value); } 
}
public override String Creater 
{
get { if (ChanageProperty.ContainsKey("Creater") == false) { return default(String); } else { return (String)ChanageProperty["Creater"]; } } 
set { ChanageProperty.Add("Creater",value); } 
}
public override Nullable<DateTime> CreatedOn 
{
get { if (ChanageProperty.ContainsKey("CreatedOn") == false) { return default(Nullable<DateTime>); } else { return (Nullable<DateTime>)ChanageProperty["CreatedOn"]; } } 
set { ChanageProperty.Add("CreatedOn",value); } 
}
public override Int32 ModifierID 
{
get { if (ChanageProperty.ContainsKey("ModifierID") == false) { return default(Int32); } else { return (Int32)ChanageProperty["ModifierID"]; } } 
set { ChanageProperty.Add("ModifierID",value); } 
}
public override String Modifier 
{
get { if (ChanageProperty.ContainsKey("Modifier") == false) { return default(String); } else { return (String)ChanageProperty["Modifier"]; } } 
set { ChanageProperty.Add("Modifier",value); } 
}
public override Nullable<DateTime> ModifyOn 
{
get { if (ChanageProperty.ContainsKey("ModifyOn") == false) { return default(Nullable<DateTime>); } else { return (Nullable<DateTime>)ChanageProperty["ModifyOn"]; } } 
set { ChanageProperty.Add("ModifyOn",value); } 
}

    }
}
