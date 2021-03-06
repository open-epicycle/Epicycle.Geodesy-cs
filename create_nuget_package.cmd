@echo off

rmdir NuGetPackage /s /q
mkdir NuGetPackage
mkdir NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0
mkdir NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\lib

copy package.nuspec NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\Epicycle.Geodesy-cs.0.1.4.0.nuspec
copy README.md NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\README.md
copy LICENSE NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\LICENSE

xcopy bin\net35\Release\Epicycle.Geodesy_cs.dll NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\lib\net35\
xcopy bin\net35\Release\Epicycle.Geodesy_cs.pdb NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\lib\net35\
xcopy bin\net35\Release\Epicycle.Geodesy_cs.xml NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\lib\net35\
xcopy bin\net40\Release\Epicycle.Geodesy_cs.dll NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\lib\net40\
xcopy bin\net40\Release\Epicycle.Geodesy_cs.pdb NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\lib\net40\
xcopy bin\net40\Release\Epicycle.Geodesy_cs.xml NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\lib\net40\
xcopy bin\net45\Release\Epicycle.Geodesy_cs.dll NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\lib\net45\
xcopy bin\net45\Release\Epicycle.Geodesy_cs.pdb NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\lib\net45\
xcopy bin\net45\Release\Epicycle.Geodesy_cs.xml NuGetPackage\Epicycle.Geodesy-cs.0.1.4.0\lib\net45\

cd NuGetPackage
nuget pack Epicycle.Geodesy-cs.0.1.4.0\Epicycle.Geodesy-cs.0.1.4.0.nuspec -Properties version=0.1.4.0
7z a -tzip Epicycle.Geodesy-cs.0.1.4.0.zip Epicycle.Geodesy-cs.0.1.4.0 Epicycle.Geodesy-cs.0.1.4.0.nupkg

echo nuget push Epicycle.Geodesy-cs.0.1.4.0.nupkg > push.cmd
echo pause >> push.cmd

pause