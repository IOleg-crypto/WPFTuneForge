@echo off
echo ========================================
echo    WpfTuneForgePlayer Test Runner
echo ========================================
echo.

echo [1/4] Restoring NuGet packages (NuGet.exe)...
set NUGET_EXE=%~dp0.nuget\nuget.exe
if not exist "%~dp0.nuget" mkdir "%~dp0.nuget"
if not exist "%NUGET_EXE%" (
    echo Downloading nuget.exe...
    powershell -NoProfile -ExecutionPolicy Bypass -Command "Invoke-WebRequest -UseBasicParsing -OutFile '%NUGET_EXE%' 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe'"
)
"%NUGET_EXE%" restore WpfTuneForgePlayer.sln -NonInteractive
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to restore packages
    pause
    exit /b 1
)

echo.
echo [2/4] Building solution (MSBuild)...
set "MSBUILD_EXE=msbuild"
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe" set "MSBUILD_EXE=%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe" set "MSBUILD_EXE=%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" set "MSBUILD_EXE=%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
"%MSBUILD_EXE%" WpfTuneForgePlayer.sln /p:Configuration=Debug /m /v:m
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to build solution
    pause
    exit /b 1
)

echo.
echo [3/4] Running unit tests (VSTest)...
set "VSTEST_CONSOLE=vstest.console.exe"
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" set "VSTEST_CONSOLE=%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" set "VSTEST_CONSOLE=%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" set "VSTEST_CONSOLE=%ProgramFiles(x86)%\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
if not exist TestResults mkdir TestResults
"%VSTEST_CONSOLE%" "WpfTuneForgePlayer.Tests\bin\Debug\WpfTuneForgePlayer.Tests.dll" /Logger:trx;LogFileName=TestResults\results.trx
if %ERRORLEVEL% neq 0 (
    echo WARNING: Some tests failed or were skipped
)

echo.
echo [4/4] Test Results Summary:
echo - Test results saved to TestResults\ directory
echo - Check TestResults\*.trx files for detailed results
echo.

echo ========================================
echo    Test run completed
echo ========================================
pause
