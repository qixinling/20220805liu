@echo off

SET Paths=%1

SET ViewPath=%Paths%View
SET ViewAdminPath=%Paths%ViewAdmin

echo.********************************************************************************
echo.**                 1.运行View 2.运行View_Admin 3.两个都运行 0.退出            **
echo.********************************************************************************

SET /p key_val=请输入要运行的项目编号:
echo.

::/c执行完成后关闭/k执行完后不关闭
::/d表示改变当前目录

if "%key_val%"=="1" (
	start cmd /k "cd/d"%ViewPath%" && npm run serve"
) else if "%key_val%"=="2" (
	start cmd /k "cd/d"%ViewAdminPath%" && npm run serve"
) else if "%key_val%"=="3" (
	start cmd /k "cd/d"%ViewPath%" && npm run serve"
	@ping -n 3 127.0.0.1 >nul
	start cmd /k "cd/d"%ViewAdminPath%" && npm run serve"
) else if "%key_val%"=="0" (
	exit
) else (
	echo.指令错误
)

echo.
call %1start.bat