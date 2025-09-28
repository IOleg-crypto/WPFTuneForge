@echo off
echo ========================================
echo    WpfTuneForgePlayer Test Runner
echo ========================================
echo.

echo [1/4] Restoring NuGet packages...
dotnet restore
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to restore packages
    pause
    exit /b 1
)

echo.
echo [2/4] Building test project...
msbuild WpfTuneForgePlayer.Tests.csproj /p:Configuration=Debug /verbosity:minimal
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to build test project
    pause
    exit /b 1
)

echo.
echo [3/4] Running unit tests...
dotnet test --verbosity normal --logger trx --results-directory TestResults
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
