
USE [master]
GO
/****** Object:  Database [myblog]    Script Date: 2016/12/25 9:34:42 ******/
USE [master]
GO
/****** Object:  Database [myblog]    Script Date: 2016/12/25 9:34:42 ******/
CREATE DATABASE [myblog2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'myblog', FILENAME = N'c:\myblog2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'myblog_log', FILENAME = N'c:\myblog_log2.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
Collate Chinese_PRC_CI_AS

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [myblog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [myblog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [myblog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [myblog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [myblog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [myblog] SET ARITHABORT OFF 
GO
ALTER DATABASE [myblog] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [myblog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [myblog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [myblog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [myblog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [myblog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [myblog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [myblog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [myblog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [myblog] SET  DISABLE_BROKER 
GO
ALTER DATABASE [myblog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [myblog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [myblog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [myblog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [myblog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [myblog] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [myblog] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [myblog] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [myblog] SET  MULTI_USER 
GO
ALTER DATABASE [myblog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [myblog] SET DB_CHAINING OFF 
GO
ALTER DATABASE [myblog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [myblog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [myblog] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'myblog', N'ON'
GO
ALTER DATABASE [myblog] SET QUERY_STORE = OFF
GO
USE [myblog]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [myblog]
GO
/****** Object:  UserDefinedFunction [dbo].[GetSubstring]    Script Date: 2016/12/25 9:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/***********************************/
-- 功能：获取一段文字中指定数目的文字
-- 作者：张刚
-- 时间：2009-05-20
-- select dbo.GetSubstring('你好你是你哥',2)
/**********************************/
CREATE FUNCTION [dbo].[GetSubstring]  (@str as nvarchar(MAX),@length as INT)
RETURNS NVARCHAR(512)
AS
BEGIN 
	DECLARE @substr AS NVARCHAR(10)
	declare @i as INT
	DECLARE @returnstr AS NVARCHAR(MAX)
	SET @substr='...'
	IF LEN(@str) >= 5000
	SET @str = LEFT(@str,5000)
	SET @returnstr=dbo.regexReplace(@str,'((<\s*script[^>]*?>[\w\W]*?<\s*/script\s*>)|(<\s*style[^>]*?>[\w\W]*?<\s*/style\s*>)|(<!--[\w\W]*?-->)|(<\s*/?\s*((div)|(span)|(tbody)|(o:p)|(thead)|(span)|(table)|(tr)|(td)|(th)|(li)|(ul)|(ol)|(script)|(style)|(iframe)|(frame)|(form)|(input)|(link)|(meta)|(head)|(body)|(hr)|(h1)|(h2)|(h3)|(img)|(font)|(p)|(pre)|(a)|(strong)|(object)|(embed)|(br)|(b)|(wbr)|(param))((/?>)|(\s+[^>]*?>))))','',1,1)
	SET @returnstr=dbo.regexReplace(@returnstr,'[\s　]+',' ',1,1)
	SET @i = len(@returnstr)
	if(@i>@length)
	BEGIN
	  SET @returnstr=LTRIM(REPLACE(@returnstr,'&nbsp;',''))
	  SET @returnstr=LEFT(@returnstr,@length)+@substr
	end
	RETURN(@returnstr)
END


GO
/****** Object:  UserDefinedFunction [dbo].[regexReplace]    Script Date: 2016/12/25 9:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----正则替换函数
CREATE function [dbo].[regexReplace] 
( 
@source varchar(5000),    --原字符串 
@regexp varchar(1000),    --正则表达式 
@replace varchar(1000),   --替换值 
@globalReplace bit = 0,   --是否是全局替换 
@ignoreCase bit = 0       --是否忽略大小写 
) 
returnS varchar(1000) AS 
begin 
declare @hr integer 
declare @objRegExp integer 
declare @result varchar(5000) 

exec @hr = sp_OACreate 'VBScript.RegExp', @objRegExp OUTPUT 
IF @hr <> 0 begin 
exec @hr = sp_OADestroy @objRegExp 
return null 
end 
exec @hr = sp_OASetProperty @objRegExp, 'Pattern', @regexp 
IF @hr <> 0 begin 
exec @hr = sp_OADestroy @objRegExp 
return null 
end 
exec @hr = sp_OASetProperty @objRegExp, 'Global', @globalReplace 
IF @hr <> 0 begin 
exec @hr = sp_OADestroy @objRegExp 
return null 
end 
exec @hr = sp_OASetProperty @objRegExp, 'IgnoreCase', @ignoreCase 
IF @hr <> 0 begin 
exec @hr = sp_OADestroy @objRegExp 
return null 
end  
exec @hr = sp_OAMethod @objRegExp, 'Replace', @result OUTPUT, @source, @replace 
IF @hr <> 0 begin 
exec @hr = sp_OADestroy @objRegExp 
return null 
end 
exec @hr = sp_OADestroy @objRegExp 
IF @hr <> 0 begin 
return null 
end 

return @result 
end 


GO
/****** Object:  Table [dbo].[BlogInfo]    Script Date: 2016/12/25 9:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogInfo](
	[BlogId] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](128) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CommentInfo]    Script Date: 2016/12/25 9:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommentInfo](
	[CommentId] [bigint] IDENTITY(1,1) NOT NULL,
	[BlogId] [bigint] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Comment] [nvarchar](1024) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK__CommentI__3214EC074EEB6A0E] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2016/12/25 9:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](64) NULL,
	[Password] [nvarchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BlogInfo] ADD  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[CommentInfo] ADD  CONSTRAINT [DF_CommentInfo_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
USE [master]
GO
ALTER DATABASE [myblog] SET  READ_WRITE 
GO

use myblog
go
insert into UserInfo values('zhanggang@outlook.com','123456')
go

