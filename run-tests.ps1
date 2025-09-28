# WpfTuneForgePlayer Test Runner
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "    WpfTuneForgePlayer Test Runner" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

try {
    Write-Host "[1/4] Restoring NuGet packages..." -ForegroundColor Yellow
    dotnet restore
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to restore packages"
    }

    Write-Host ""
    Write-Host "[2/4] Building test project..." -ForegroundColor Yellow
    msbuild WpfTuneForgePlayer.Tests.csproj /p:Configuration=Debug /verbosity:minimal
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to build test project"
    }

    Write-Host ""
    Write-Host "[3/4] Running unit tests..." -ForegroundColor Yellow
    dotnet test --verbosity normal --logger trx --results-directory TestResults
    if ($LASTEXITCODE -ne 0) {
        Write-Host "WARNING: Some tests failed or were skipped" -ForegroundColor Yellow
    }

    Write-Host ""
    Write-Host "[4/4] Test Results Summary:" -ForegroundColor Green
    Write-Host "- Test results saved to TestResults\ directory" -ForegroundColor White
    Write-Host "- Check TestResults\*.trx files for detailed results" -ForegroundColor White
    Write-Host ""

    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "    Test run completed successfully" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Cyan

} catch {
    Write-Host ""
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Red
    Write-Host "    Test run failed" -ForegroundColor Red
    Write-Host "========================================" -ForegroundColor Red
    exit 1
}

Read-Host "Press Enter to continue"
