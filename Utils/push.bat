@echo off
git --version || echo.如无法执行请先配置环境变量

for /F %%i in ('git config user.name') do ( set name=%%i)

git add .
echo.add完成
echo.

echo.正在commit...
echo.
git commit -m "%name%%time:~0,8%"

echo.
echo.%name%正在提交远程仓库...
echo.
git push origin master

echo.
echo.提交完成

echo.
call %1start.bat