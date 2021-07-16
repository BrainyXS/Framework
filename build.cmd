@echo off
echo =====================
echo BUILD FRAMEWORK NUGET
echo =====================
echo 
timeout /t 3
echo "Cleaning Solution"
dotnet clean
echo Clean Nuget
del E:\Coding\Nuget\Framework.*.nupkg
echo Building Solution
dotnet build
echo Copying packages
copy Framework.UI.Contract\bin\Debug\*.nupkg E:\Coding\Nuget
copy Framework.UI\bin\Debug\*.nupkg E:\Coding\Nuget
echo Fertig!
pause
