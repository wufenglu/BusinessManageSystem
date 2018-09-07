IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_Modules]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
drop table Sys_Modules;
END

CREATE TABLE Sys_Modules
(
ID int identity(1,1) primary key,--编号
Name varchar(50),--名称
Code varchar(50),--编码
ParentID INT,--父级ID
[Level] INT,--层级
IsEnable bit,--是否启用
OrderBy int,--排序
CreaterID INT,--创建人ID
Creater varchar(50),--创建人
CreatedOn datetime  default getDate(),--创建日期
ModifierID INT,--修改人ID
Modifier varchar(50),--修改人
ModifyOn datetime  default getDate()--修改日期
)


IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_Pages]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
drop table Sys_Pages;
END

CREATE TABLE Sys_Pages
(
ID int identity(1,1) primary key,--编号
ModuleID INT,--模块ID
Name varchar(50),--名称
Code varchar(50),--编码
Url VARCHAR(256),--页面地址
OrderBy int,--排序
CreaterID INT,--创建人ID
Creater varchar(50),--创建人
CreatedOn datetime  default getDate(),--创建日期
ModifierID INT,--修改人ID
Modifier varchar(50),--修改人
ModifyOn datetime  default getDate()--修改日期
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_Acions]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
drop table Sys_Acions;
END

CREATE TABLE Sys_Acions
(
ID int identity(1,1) primary key,--编号
PageID INT,--页面ID
Name varchar(50),--名称
Code varchar(50),--编码
OrderBy int,--排序
CreaterID INT,--创建人ID
Creater varchar(50),--创建人
CreatedOn datetime  default getDate(),--创建日期
ModifierID INT,--修改人ID
Modifier varchar(50),--修改人
ModifyOn datetime  default getDate()--修改日期
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_Organizations]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
DROP table Sys_Organizations;
END

create table Sys_Organizations
(
ID int identity(1,1) primary key,--编号
Name varchar(50),--名称
Code varchar(50),--编码
IsEnable bit,--是否启用
CreaterID INT,--创建人ID
Creater varchar(50),--创建人
CreatedOn datetime  default getDate(),--创建日期
ModifierID INT,--修改人ID
Modifier varchar(50),--修改人
ModifyOn datetime  default getDate()--修改日期
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_OrganizationDataBase]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
drop table Sys_OrganizationDataBase;

create table Sys_OrganizationDataBase
(
ID int identity(1,1) primary key,--编号
OrganizationID INT,--租户ID
DbType varchar(50),--数据库类型
[Server] varchar(256),--数据库地址
DatabaseName varchar(50),--数据库名称
UserName varchar(50),--用户名
[Password] varchar(50),--密码
Port varchar(50),--端口
IsEnable bit,--是否启用
IsMaster bit,--是否主库
Weight int,--权重
CreaterID INT,--创建人ID
Creater varchar(50),--创建人
CreatedOn datetime  default getDate(),--创建日期
ModifierID INT,--修改人ID
Modifier varchar(50),--修改人
ModifyOn datetime  default getDate()--修改日期
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_OrganizationModules]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
drop table Sys_OrganizationModules;

create table Sys_OrganizationModules
(
ID int identity(1,1) primary key,--编号
OrganizationID INT,--租户ID
ModuleID INT,--模块ID
CreaterID INT,--创建人ID
Creater varchar(50),--创建人
CreatedOn datetime  default getDate(),--创建日期
ModifierID INT,--修改人ID
Modifier varchar(50),--修改人
ModifyOn datetime  default getDate()--修改日期
)