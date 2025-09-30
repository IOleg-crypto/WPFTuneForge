@echo off
echo Testing build process...
echo.

echo [1] Restoring packages...
dotnet restore WpfTuneForgePlayer.sln
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to restore packages
    pause
    exit /b 1
)

echo.
echo [2] Building solution...
dotnet build WpfTuneForgePlayer.sln --configuration Debug
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to build solution
    pause
    exit /b 1
)

echo.
echo [3] Running tests...
dotnet test WpfTuneForgePlayer.sln --verbosity normal
if %ERRORLEVEL% neq 0 (
    echo WARNING: Some tests failed or were skipped
)

echo.
echo Build and test completed!
pause
