@echo off

cd projects
msbuild Epicycle.Geodesy.net35.sln /t:Clean,Build /p:Configuration=Debug
msbuild Epicycle.Geodesy.net35.sln /t:Clean,Build /p:Configuration=Release
msbuild Epicycle.Geodesy.net40.sln /t:Clean,Build /p:Configuration=Debug
msbuild Epicycle.Geodesy.net40.sln /t:Clean,Build /p:Configuration=Release
msbuild Epicycle.Geodesy.net45.sln /t:Clean,Build /p:Configuration=Debug
msbuild Epicycle.Geodesy.net45.sln /t:Clean,Build /p:Configuration=Release

pause
