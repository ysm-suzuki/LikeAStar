cd /d %~dp0
cd ..
C:\Windows\Microsoft.NET\Framework64\v3.5\csc /target:exe /out:lib/test_likeastar.exe /recurse:*.cs /reference:lib/Atagoal.dll /reference:lib/LWCollide.dll
lib\test_likeastar.exe
del lib\test_likeastar.exe
pause