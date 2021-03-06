﻿

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[regexReplace]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'----正则替换函数
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

exec @hr = sp_OACreate ''VBScript.RegExp'', @objRegExp OUTPUT 
IF @hr <> 0 begin 
exec @hr = sp_OADestroy @objRegExp 
return null 
end 
exec @hr = sp_OASetProperty @objRegExp, ''Pattern'', @regexp 
IF @hr <> 0 begin 
exec @hr = sp_OADestroy @objRegExp 
return null 
end 
exec @hr = sp_OASetProperty @objRegExp, ''Global'', @globalReplace 
IF @hr <> 0 begin 
exec @hr = sp_OADestroy @objRegExp 
return null 
end 
exec @hr = sp_OASetProperty @objRegExp, ''IgnoreCase'', @ignoreCase 
IF @hr <> 0 begin 
exec @hr = sp_OADestroy @objRegExp 
return null 
end  
exec @hr = sp_OAMethod @objRegExp, ''Replace'', @result OUTPUT, @source, @replace 
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

' 
END
GO








sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
sp_configure 'Ole Automation Procedures', 1;
GO
RECONFIGURE;
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSubstring]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'

/***********************************/
-- 功能：获取一段文字中指定数目的文字
-- 作者：张刚
-- 时间：2009-05-20
-- select dbo.GetSubstring(''你好你是你哥'',2)
/**********************************/
CREATE FUNCTION [dbo].[GetSubstring]  (@str as nvarchar(MAX),@length as INT)
RETURNS NVARCHAR(512)
AS
BEGIN 
	DECLARE @substr AS NVARCHAR(10)
	declare @i as INT
	DECLARE @returnstr AS NVARCHAR(MAX)
	SET @substr=''...''
	IF LEN(@str) >= 5000
	SET @str = LEFT(@str,5000)
	SET @returnstr=dbo.regexReplace(@str,''((<\s*script[^>]*?>[\w\W]*?<\s*/script\s*>)|(<\s*style[^>]*?>[\w\W]*?<\s*/style\s*>)|(<!--[\w\W]*?-->)|(<\s*/?\s*((div)|(span)|(tbody)|(o:p)|(thead)|(span)|(table)|(tr)|(td)|(th)|(li)|(ul)|(ol)|(script)|(style)|(iframe)|(frame)|(form)|(input)|(link)|(meta)|(head)|(body)|(hr)|(h1)|(h2)|(h3)|(img)|(font)|(p)|(pre)|(a)|(strong)|(object)|(embed)|(br)|(b)|(wbr)|(param))((/?>)|(\s+[^>]*?>))))'','''',1,1)
	SET @returnstr=dbo.regexReplace(@returnstr,''[\s　]+'','' '',1,1)
	SET @i = len(@returnstr)
	if(@i>@length)
	BEGIN
	  SET @returnstr=LTRIM(REPLACE(@returnstr,''&nbsp;'',''''))
	  SET @returnstr=LEFT(@returnstr,@length)+@substr
	end
	RETURN(@returnstr)
END

' 
END
GO