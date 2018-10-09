IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_User]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
drop table Sys_User;
END

CREATE TABLE Sys_User
(
ID int identity(1,1) primary key,--���
UserName varchar(50),--����
UserCode varchar(50),--����
Password varchar(50),--����
IsEnable bit,--�Ƿ�����
CreaterID INT,--������ID
Creater varchar(50),--������
CreatedOn datetime  default getDate(),--��������
ModifierID INT,--�޸���ID
Modifier varchar(50),--�޸���
ModifyOn datetime  default getDate()--�޸�����
)


IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_Modules]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
drop table Sys_Modules;
END

CREATE TABLE Sys_Modules
(
ID int identity(1,1) primary key,--���
Name varchar(50),--����
Code varchar(50),--����
ParentID INT,--����ID
[Level] INT,--�㼶
IsEnable bit,--�Ƿ�����
OrderBy int,--����
CreaterID INT,--������ID
Creater varchar(50),--������
CreatedOn datetime  default getDate(),--��������
ModifierID INT,--�޸���ID
Modifier varchar(50),--�޸���
ModifyOn datetime  default getDate()--�޸�����
)


IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_Pages]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
drop table Sys_Pages;
END

CREATE TABLE Sys_Pages
(
ID int identity(1,1) primary key,--���
ModuleID INT,--ģ��ID
Name varchar(50),--����
Code varchar(50),--����
Url VARCHAR(256),--ҳ���ַ
OrderBy int,--����
CreaterID INT,--������ID
Creater varchar(50),--������
CreatedOn datetime  default getDate(),--��������
ModifierID INT,--�޸���ID
Modifier varchar(50),--�޸���
ModifyOn datetime  default getDate()--�޸�����
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_Acions]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
drop table Sys_Acions;
END

CREATE TABLE Sys_Acions
(
ID int identity(1,1) primary key,--���
PageID INT,--ҳ��ID
Name varchar(50),--����
Code varchar(50),--����
OrderBy int,--����
CreaterID INT,--������ID
Creater varchar(50),--������
CreatedOn datetime  default getDate(),--��������
ModifierID INT,--�޸���ID
Modifier varchar(50),--�޸���
ModifyOn datetime  default getDate()--�޸�����
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_Organizations]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
DROP table Sys_Organizations;
END

create table Sys_Organizations
(
ID int identity(1,1) primary key,--���
Name varchar(50),--����
Code varchar(50),--����
IsEnable bit,--�Ƿ�����
CreaterID INT,--������ID
Creater varchar(50),--������
CreatedOn datetime  default getDate(),--��������
ModifierID INT,--�޸���ID
Modifier varchar(50),--�޸���
ModifyOn datetime  default getDate()--�޸�����
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_OrganizationDataBase]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
drop table Sys_OrganizationDataBase;

create table Sys_OrganizationDataBase
(
ID int identity(1,1) primary key,--���
OrganizationID INT,--�⻧ID
DbType varchar(50),--���ݿ�����
[Server] varchar(256),--���ݿ��ַ
DatabaseName varchar(50),--���ݿ�����
UserName varchar(50),--�û���
[Password] varchar(50),--����
Port varchar(50),--�˿�
IsEnable bit,--�Ƿ�����
IsMaster bit,--�Ƿ�����
Weight int,--Ȩ��
CreaterID INT,--������ID
Creater varchar(50),--������
CreatedOn datetime  default getDate(),--��������
ModifierID INT,--�޸���ID
Modifier varchar(50),--�޸���
ModifyOn datetime  default getDate()--�޸�����
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Sys_OrganizationModules]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
drop table Sys_OrganizationModules;

create table Sys_OrganizationModules
(
ID int identity(1,1) primary key,--���
OrganizationID INT,--�⻧ID
ModuleID INT,--ģ��ID
CreaterID INT,--������ID
Creater varchar(50),--������
CreatedOn datetime  default getDate(),--��������
ModifierID INT,--�޸���ID
Modifier varchar(50),--�޸���
ModifyOn datetime  default getDate()--�޸�����
)