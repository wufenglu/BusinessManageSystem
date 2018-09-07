using System;

namespace YK.Common.Proxy.GenerateClass.YK_Models_Systems_SysOrganizations
{
    /// <summary>
    /// 生成代码类
    /// </summary>
    public class SysOrganizations:YK.Models.Systems.SysOrganizations
    {
		public override Int32 ID 
{
get { if (ChanageProperty.ContainsKey("ID") == false) { return default(Int32); } else { return (Int32)ChanageProperty["ID"]; } } 
set { ChanageProperty.Add("ID",value); } 
}
public override String Name 
{
get { if (ChanageProperty.ContainsKey("Name") == false) { return default(String); } else { return (String)ChanageProperty["Name"]; } } 
set { ChanageProperty.Add("Name",value); } 
}
public override String Code 
{
get { if (ChanageProperty.ContainsKey("Code") == false) { return default(String); } else { return (String)ChanageProperty["Code"]; } } 
set { ChanageProperty.Add("Code",value); } 
}
public override Boolean IsEnable 
{
get { if (ChanageProperty.ContainsKey("IsEnable") == false) { return default(Boolean); } else { return (Boolean)ChanageProperty["IsEnable"]; } } 
set { ChanageProperty.Add("IsEnable",value); } 
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
