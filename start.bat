@echo off

color E

SET Paths=%~dp0

echo.********************************************************************************
echo.**      1.Build前端 2.发布项目 3.Pull同步 4.Push上传 5.Run运行前端 0.退出     **
echo.********************************************************************************


SET /p key_val=请输入要执行的程序编号:
echo.

if "%key_val%"=="1" (
	call cmd /k .\Utils\build.bat %Paths%
) else if "%key_val%"=="2" (
	call cmd /k .\Utils\publish.bat %Paths%
) else if "%key_val%"=="3" (
	call cmd /k .\Utils\pull.bat %Paths%
) else if "%key_val%"=="4" (
	call cmd /k .\Utils\push.bat %Paths%	
) else if "%key_val%"=="5" (
	call cmd /k .\Utils\run.bat %Paths%
) else if "%key_val%"=="0" (
	exit
) else (
	echo.指令错误
)