@echo off
setlocal enabledelayedexpansion
git --version || echo.���޷�ִ���������û�������
if exist ".git\" (
	echo.������ȡԶ�ֿ̲�...
	echo.
	
	git pull origin master
	
	echo.
	echo.��ȡ���
) else (
	SET /p keyval=��δ��ʼ�����زֿ�,��������Ŀ���:
	echo.

	echo.���ڳ�ʼ��...
	echo.
	
	git init
	echo.
	
	echo.��ʼ�����
	echo.
	
	echo.������ȡԶ�ֿ̲�...
	echo.
	
	git remote add origin https://github.com/qixinling/!keyval!.git && git pull origin master
	rem ��if�н��յĲ�����Ҫ��!�滻%,��������setlocal enabledelayedexpansion
	
	echo.
	echo.��ȡ���
)

echo.
call %1start.bat