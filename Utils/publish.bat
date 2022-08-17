@echo off

SET Paths=%1

SET ServerPath=%Paths%Server\Server.Api
SET PublishPath=%Paths%\publish

SET ViewPath=%Paths%View
SET Wwwroot=%PublishPath%\wwwroot

SET ViewAdminPath=%Paths%ViewAdmin
SET Wwwrootadmin=%PublishPath%\wwwroot\admin


echo.********************************************************************************
echo.**       1.����� 2.����˺�View 3.����˺�View_Admin 4.��ȫ��Ҫ 0.�˳�       **
echo.********************************************************************************

SET /p key_val=������Ҫ����������:
echo.

::/cִ����ɺ�ر�/kִ����󲻹ر�
::/d��ʾ�ı䵱ǰĿ¼
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
	echo.ָ�����
)

cd/d%Paths%

echo.
call %1start.bat