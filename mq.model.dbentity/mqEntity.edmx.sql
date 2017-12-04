
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/24/2017 16:29:08
-- Generated from EDMX file: F:\MQWebSite\MQWebSite.git\trunk\mq.model.dbentity\mqEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [POS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[T_BG_ActiveFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_BG_ActiveFile];
GO
IF OBJECT_ID(N'[dbo].[T_BG_Department]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_BG_Department];
GO
IF OBJECT_ID(N'[dbo].[T_BG_DisplayGuideFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_BG_DisplayGuideFile];
GO
IF OBJECT_ID(N'[dbo].[T_BG_LoginLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_BG_LoginLog];
GO
IF OBJECT_ID(N'[dbo].[T_BG_Menu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_BG_Menu];
GO
IF OBJECT_ID(N'[dbo].[T_BG_PublishFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_BG_PublishFile];
GO
IF OBJECT_ID(N'[dbo].[T_BG_Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_BG_Role];
GO
IF OBJECT_ID(N'[dbo].[T_BG_Role_Menu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_BG_Role_Menu];
GO
IF OBJECT_ID(N'[dbo].[T_BG_UpFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_BG_UpFiles];
GO
IF OBJECT_ID(N'[dbo].[T_BG_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_BG_User];
GO
IF OBJECT_ID(N'[dbo].[T_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[T_User];
GO
IF OBJECT_ID(N'[POSModelStoreContainer].[V_BG_ActiveFile_Department]', 'U') IS NOT NULL
    DROP TABLE [POSModelStoreContainer].[V_BG_ActiveFile_Department];
GO
IF OBJECT_ID(N'[POSModelStoreContainer].[V_BG_PublishFile_User]', 'U') IS NOT NULL
    DROP TABLE [POSModelStoreContainer].[V_BG_PublishFile_User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'T_User'
CREATE TABLE [dbo].[T_User] (
    [Code] nvarchar(50)  NOT NULL,
    [Type] int  NOT NULL,
    [Name] nvarchar(10)  NULL,
    [DepartmentId] nvarchar(10)  NULL,
    [Gender] nvarchar(10)  NULL,
    [Phone] nvarchar(11)  NULL,
    [Birthday] datetime  NULL,
    [IDCard] nchar(18)  NULL,
    [StatusCode] nvarchar(10)  NULL,
    [BasePay] decimal(18,0)  NULL,
    [OtherAllance] decimal(18,0)  NULL,
    [Post] nvarchar(10)  NULL,
    [Remark] nvarchar(50)  NULL,
    [AddTime] datetime  NULL,
    [Frozen] bit  NULL,
    [OperationLvl] int  NULL,
    [PassWord] nvarchar(10)  NULL,
    [IsSaler] bit  NOT NULL
);
GO

-- Creating table 'T_BG_User'
CREATE TABLE [dbo].[T_BG_User] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Phone] nvarchar(20)  NULL,
    [Name] nvarchar(20)  NULL,
    [RealName] nvarchar(50)  NULL,
    [PassWord] nvarchar(20)  NULL,
    [Email] nvarchar(30)  NULL,
    [RoleID] int  NULL,
    [ShopID] int  NULL,
    [Status] int  NULL,
    [ApproveTime] datetime  NULL,
    [ApproveName] nvarchar(20)  NULL,
    [AddTime] datetime  NULL,
    [IsDel] int  NULL
);
GO

-- Creating table 'T_BG_LoginLog'
CREATE TABLE [dbo].[T_BG_LoginLog] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [LogUserID] bigint  NULL,
    [LogTime] datetime  NULL,
    [IP] nvarchar(20)  NULL,
    [Address] nvarchar(50)  NULL,
    [IsLogIn] bit  NULL,
    [Remark] nvarchar(100)  NULL
);
GO

-- Creating table 'T_BG_Menu'
CREATE TABLE [dbo].[T_BG_Menu] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Description] nvarchar(50)  NULL,
    [Parent_ID] int  NULL,
    [Grade] int  NULL,
    [URL] nvarchar(500)  NULL,
    [Type] nvarchar(50)  NULL,
    [IsDel] int  NULL,
    [AddTime] datetime  NULL,
    [UpdateTime] datetime  NULL
);
GO

-- Creating table 'T_BG_Role'
CREATE TABLE [dbo].[T_BG_Role] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NULL,
    [Description] nvarchar(50)  NULL,
    [AddTime] datetime  NULL,
    [IsDel] int  NULL
);
GO

-- Creating table 'T_BG_Role_Menu'
CREATE TABLE [dbo].[T_BG_Role_Menu] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RoleID] int  NOT NULL,
    [MenuID] int  NOT NULL,
    [AddTime] datetime  NULL,
    [IsDel] int  NULL
);
GO

-- Creating table 'T_BG_UpFiles'
CREATE TABLE [dbo].[T_BG_UpFiles] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [filename] nvarchar(400)  NULL,
    [filehash] nvarchar(32)  NULL,
    [userid] bigint  NULL,
    [fileoriginname] nvarchar(400)  NULL,
    [filepath] nvarchar(800)  NULL,
    [ext] nvarchar(10)  NULL,
    [filetype] int  NULL,
    [proofreading] nvarchar(250)  NULL,
    [proofpath] nvarchar(800)  NULL,
    [addtime] datetime  NULL
);
GO

-- Creating table 'T_BG_PublishFile'
CREATE TABLE [dbo].[T_BG_PublishFile] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NULL,
    [EditorId] bigint  NULL,
    [PublishTime] datetime  NULL,
    [PublishState] bigint  NULL,
    [Lvl] bigint  NULL,
    [TextContent] varchar(max)  NULL,
    [HtmlContent] varchar(max)  NULL,
    [IsDel] int  NULL,
    [FileNewName] nvarchar(400)  NULL,
    [FileOriginName] nvarchar(400)  NULL,
    [FilePath] nvarchar(1000)  NULL,
    [FileType] nvarchar(20)  NULL
);
GO

-- Creating table 'V_BG_PublishFile_User'
CREATE TABLE [dbo].[V_BG_PublishFile_User] (
    [Id] bigint  NOT NULL,
    [Title] nvarchar(50)  NULL,
    [EditorId] bigint  NULL,
    [PublishTime] datetime  NULL,
    [PublishState] bigint  NULL,
    [Lvl] bigint  NULL,
    [TextContent] varchar(max)  NULL,
    [HtmlContent] varchar(max)  NULL,
    [IsDel] int  NULL,
    [FileNewName] nvarchar(400)  NULL,
    [FileOriginName] nvarchar(400)  NULL,
    [FilePath] nvarchar(1000)  NULL,
    [EditorName] nvarchar(20)  NULL,
    [EditorRealName] nvarchar(50)  NULL,
    [FileType] nvarchar(20)  NULL
);
GO

-- Creating table 'T_BG_ActiveFile'
CREATE TABLE [dbo].[T_BG_ActiveFile] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Type] int  NULL,
    [DeparementId] bigint  NULL,
    [PublicTime] datetime  NULL,
    [NatureType] int  NULL,
    [FileNewName] nvarchar(400)  NULL,
    [FileOriginName] nvarchar(400)  NULL,
    [FilePath] nvarchar(1000)  NULL,
    [FileType] nvarchar(20)  NULL,
    [Remark] varchar(max)  NULL,
    [IsDel] int  NULL,
    [AddTime] datetime  NULL
);
GO

-- Creating table 'T_BG_Department'
CREATE TABLE [dbo].[T_BG_Department] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [DepartmentName] nvarchar(200)  NULL,
    [AddTime] datetime  NULL,
    [IsDel] int  NULL,
    [Lvl] int  NULL
);
GO

-- Creating table 'T_BG_DisplayGuideFile'
CREATE TABLE [dbo].[T_BG_DisplayGuideFile] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(100)  NULL,
    [FileNewName] nvarchar(400)  NULL,
    [FileOriginName] nvarchar(400)  NULL,
    [FilePath] nvarchar(1000)  NULL,
    [FileType] nvarchar(20)  NULL,
    [UserId] bigint  NULL,
    [PublishTime] datetime  NULL,
    [AddTime] datetime  NULL,
    [IsDel] int  NULL
);
GO

-- Creating table 'V_BG_ActiveFile_Department'
CREATE TABLE [dbo].[V_BG_ActiveFile_Department] (
    [Id] bigint  NOT NULL,
    [Type] int  NULL,
    [DeparementId] bigint  NULL,
    [DepartmentName] nvarchar(200)  NULL,
    [PublicTime] datetime  NULL,
    [NatureType] int  NULL,
    [FileNewName] nvarchar(400)  NULL,
    [FileOriginName] nvarchar(400)  NULL,
    [FilePath] nvarchar(1000)  NULL,
    [FileType] nvarchar(20)  NULL,
    [Remark] varchar(max)  NULL,
    [Lvl] int  NULL,
    [IsDel] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Code], [Type] in table 'T_User'
ALTER TABLE [dbo].[T_User]
ADD CONSTRAINT [PK_T_User]
    PRIMARY KEY CLUSTERED ([Code], [Type] ASC);
GO

-- Creating primary key on [ID] in table 'T_BG_User'
ALTER TABLE [dbo].[T_BG_User]
ADD CONSTRAINT [PK_T_BG_User]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'T_BG_LoginLog'
ALTER TABLE [dbo].[T_BG_LoginLog]
ADD CONSTRAINT [PK_T_BG_LoginLog]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'T_BG_Menu'
ALTER TABLE [dbo].[T_BG_Menu]
ADD CONSTRAINT [PK_T_BG_Menu]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'T_BG_Role'
ALTER TABLE [dbo].[T_BG_Role]
ADD CONSTRAINT [PK_T_BG_Role]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'T_BG_Role_Menu'
ALTER TABLE [dbo].[T_BG_Role_Menu]
ADD CONSTRAINT [PK_T_BG_Role_Menu]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'T_BG_UpFiles'
ALTER TABLE [dbo].[T_BG_UpFiles]
ADD CONSTRAINT [PK_T_BG_UpFiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_BG_PublishFile'
ALTER TABLE [dbo].[T_BG_PublishFile]
ADD CONSTRAINT [PK_T_BG_PublishFile]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'V_BG_PublishFile_User'
ALTER TABLE [dbo].[V_BG_PublishFile_User]
ADD CONSTRAINT [PK_V_BG_PublishFile_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_BG_ActiveFile'
ALTER TABLE [dbo].[T_BG_ActiveFile]
ADD CONSTRAINT [PK_T_BG_ActiveFile]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_BG_Department'
ALTER TABLE [dbo].[T_BG_Department]
ADD CONSTRAINT [PK_T_BG_Department]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'T_BG_DisplayGuideFile'
ALTER TABLE [dbo].[T_BG_DisplayGuideFile]
ADD CONSTRAINT [PK_T_BG_DisplayGuideFile]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'V_BG_ActiveFile_Department'
ALTER TABLE [dbo].[V_BG_ActiveFile_Department]
ADD CONSTRAINT [PK_V_BG_ActiveFile_Department]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------