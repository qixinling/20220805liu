@echo off

color E

SET Paths=%~dp0

echo.********************************************************************************
echo.**      1.Buildǰ�� 2.������Ŀ 3.Pullͬ�� 4.Push�ϴ� 5.Run����ǰ�� 0.�˳�     **
echo.********************************************************************************


SET /p key_val=������Ҫִ�еĳ�����:
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
	echo.ָ�����
)