@echo off
setlocal enabledelayedexpansion
git --version || echo.如无法执行请先配置环境变量
if exist ".git\" (
	echo.正在拉取远程仓库...
	echo.
	
	git pull origin master
	
	echo.
	echo.拉取完成
) else (
	SET /p keyval=尚未初始化本地仓库,请输入项目编号:
	echo.

	echo.正在初始化...
	echo.
	
	git init
	echo.
	
	echo.初始化完成
	echo.
	
	echo.正在拉取远程仓库...
	echo.
	
	git remote add origin https://github.com/qixinling/!keyval!.git && git pull origin master
	rem 在if中接收的参数需要用!替换%,顶部加上setlocal enabledelayedexpansion
	
	echo.
	echo.拉取完成
)

echo.
call %1start.bat