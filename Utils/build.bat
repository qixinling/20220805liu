@echo off

SET Paths=%1

SET ViewPath=%Paths%View
SET ViewAdminPath=%Paths%ViewAdmin

echo.********************************************************************************
echo.**                 1.打包View 2.打包View_Admin 3.两个都打包 0.退出            **
echo.********************************************************************************

SET /p key_val=请输入要打包哪种项目:
echo.

::/c执行完成后关闭/k执行完后不关闭
::/d表示改变当前目录

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
	echo.指令错误
)

echo.
call %1start.bat