@echo off

SET Paths=%1

SET ViewPath=%Paths%View
SET ViewAdminPath=%Paths%ViewAdmin

echo.********************************************************************************
echo.**                 1.���View 2.���View_Admin 3.��������� 0.�˳�            **
echo.********************************************************************************

SET /p key_val=������Ҫ���������Ŀ:
echo.

::/cִ����ɺ�ر�/kִ����󲻹ر�
::/d��ʾ�ı䵱ǰĿ¼

if "%key_val%"=="1" (
	start cmd /k "cd/d"%ViewPath%" && npm run build"
) else if "%key_val%"=="2" (
	start cmd /k "cd/d"%ViewAdminPath%" && npm run build"
) else if "%key_val%"=="3" (
	start cmd /k "cd/d"%ViewPath%" && npm run build"
	start cmd /k "cd/d"%ViewAdminPath%" && npm run build"
) else if "%key_val%"=="0" (
	exit
) else (
	echo.ָ�����
)

echo.
call %1start.bat