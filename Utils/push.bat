@echo off
git --version || echo.���޷�ִ���������û�������

for /F %%i in ('git config user.name') do ( set name=%%i)

git add .
echo.add���
echo.

echo.����commit...
echo.
git commit -m "%name%%time:~0,8%"

echo.
echo.%name%�����ύԶ�ֿ̲�...
echo.
git push origin master

echo.
echo.�ύ���

echo.
call %1start.bat