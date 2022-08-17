@echo off

SET Paths=%1

SET ServerPath=%Paths%Server\Server.Api
SET PublishPath=%Paths%\publish

SET ViewPath=%Paths%View
SET Wwwroot=%PublishPath%\wwwroot

SET ViewAdminPath=%Paths%ViewAdmin
SET Wwwrootadmin=%PublishPath%\wwwroot\admin


echo.********************************************************************************
echo.**       1.服务端 2.服务端和View 3.服务端和View_Admin 4.我全都要 0.退出       **
echo.********************************************************************************

SET /p key_val=请输入要发布的内容:
echo.

::/c执行完成后关闭/k执行完后不关闭
::/d表示改变当前目录
cd/d%ServerPath%

if "%key_val%"=="1" (
	dotnet publish -c Release -o %PublishPath%
) else if "%key_val%"=="2" (
	dotnet publish -c Release -o %PublishPath%
	start cmd /k "cd/d"%ViewPath%" && npm run build && xcopy dist "%Wwwroot%" /e /y"
) else if "%key_val%"=="3" (
	dotnet publish -c Release -o %PublishPath%
	start cmd /k "cd/d"%ViewAdminPath%" && npm run build && xcopy dist "%Wwwrootadmin%" /e /y"
) else if "%key_val%"=="4" (
	dotnet publish -c Release -o %PublishPath%
	start cmd /k "cd/d"%ViewPath%" && npm run build && xcopy dist "%Wwwroot%" /e /y"
	start cmd /k "cd/d"%ViewAdminPath%" && npm run build && xcopy dist "%Wwwrootadmin%" /e /y"
) else if "%key_val%"=="0" (
	exit
) else (
	echo.指令错误
)

cd/d%Paths%

echo.
call %1start.bat