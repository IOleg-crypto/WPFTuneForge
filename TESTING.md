# Testing Guide for WpfTuneForgePlayer

## 📋 Overview

This document provides comprehensive information about the testing setup for WpfTuneForgePlayer, including unit tests, integration tests, and CI/CD testing strategies.

## 🏗️ Test Architecture

### Test Projects

- **WpfTuneForgePlayer.Tests** - Main test project containing all unit and integration tests
- **Separate from main project** - Tests are isolated to prevent production dependencies

### Test Categories

#### ✅ Unit Tests (CI/CD Enabled)
- **Helpers Tests**
  - `SongTests` - Tests for Song class equality, hashing, and construction
  - `LoggerTests` - Tests for logging functionality and file operations
  - `TimerHelperTests` - Tests for timer operations and null handling

- **Services Tests**
  - `AudioServiceTests` - Tests for audio service properties and validation
  - `VolumeServiceTests` - Tests for volume service basic functionality

- **Mathematics Tests**
  - `Vector2Tests` - Tests for mathematical operations

#### ⚠️ Integration Tests (Manual Run Only)
- Audio device interaction tests
- File system operation tests
- UI context dependent tests

## 🛠️ Test Technologies

### Frameworks
- **xUnit 2.9.3** - Primary testing framework
- **Moq 4.20.72** - Mocking framework for dependencies
- **NAudio** - Audio testing capabilities

### Test Patterns
- **Arrange-Act-Assert (AAA)** - Standard test structure
- **Mocking** - Isolated testing of components
- **Parameterized Tests** - Multiple input scenarios
- **Exception Testing** - Error handling validation

## 🚀 Running Tests

### Quick Start
```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --verbosity normal

# Run specific test class
dotnet test --filter "SongTests"
```

### Scripts Available
- **run-tests.bat** - Windows batch script for test execution
- **run-tests.ps1** - PowerShell script with enhanced output

### Visual Studio Integration
1. Open Test Explorer (Test → Test Explorer)
2. Build solution (Ctrl+Shift+B)
3. Click "Run All Tests" or run individual tests

## 📊 Test Coverage

### Current Coverage Areas
- ✅ **Core Models** - Song class functionality
- ✅ **Utilities** - Logger and TimerHelper operations
- ✅ **Service Properties** - Basic property getters/setters
- ✅ **Error Handling** - Null value processing
- ✅ **Edge Cases** - Boundary conditions

### Areas Needing Coverage
- ⚠️ **Audio Processing** - Complex audio operations
- ⚠️ **File I/O** - File system interactions
- ⚠️ **UI Components** - WPF-specific functionality
- ⚠️ **Device Management** - Hardware interactions

## 🔧 CI/CD Integration

### GitHub Actions Workflow
```yaml
- name: Test
  run: dotnet test --no-build --configuration Release --verbosity normal --logger trx --results-directory TestResults

- name: Publish Test Results
  uses: dorny/test-reporter@v1
  if: success() || failure()
  with:
    name: Test Results
    path: TestResults/*.trx
    reporter: dotnet-trx
```

### Test Results
- **TRX Format** - XML test result files
- **Test Reporter** - GitHub Actions integration
- **Build Status** - Automated test execution on every push

## 📝 Writing New Tests

### Test Structure Template
```csharp
[Fact]
public void MethodName_WithValidInput_ShouldReturnExpectedResult()
{
    // Arrange
    var input = "test";
    var expected = "expected";
    
    // Act
    var result = MethodUnderTest(input);
    
    // Assert
    Assert.Equal(expected, result);
}
```

### Mock Usage Example
```csharp
[Fact]
public void ServiceMethod_WithMockedDependency_ShouldWorkCorrectly()
{
    // Arrange
    var mockDependency = new Mock<IDependency>();
    mockDependency.Setup(x => x.Method()).Returns("mocked");
    var service = new Service(mockDependency.Object);
    
    // Act
    var result = service.ServiceMethod();
    
    // Assert
    Assert.Equal("expected", result);
    mockDependency.Verify(x => x.Method(), Times.Once);
}
```

### Skipping Tests
```csharp
[Fact(Skip = "Requires audio hardware - run manually")]
public void AudioTest_RequiresHardware_ShouldBeSkippedInCI()
{
    // This test won't run in CI/CD
    Assert.True(true);
}
```

## 🐛 Debugging Tests

### Common Issues
1. **Missing Dependencies** - Ensure all NuGet packages are restored
2. **Path Issues** - Check file paths in tests
3. **Mock Setup** - Verify mock configurations
4. **Async Operations** - Handle async/await properly

### Debug Configuration
```bash
# Run tests with debug symbols
dotnet test --configuration Debug --verbosity detailed

# Attach debugger to test process
dotnet test --logger "console;verbosity=detailed"
```

## 📈 Test Metrics

### Key Metrics to Track
- **Test Count** - Total number of tests
- **Pass Rate** - Percentage of passing tests
- **Coverage** - Code coverage percentage
- **Execution Time** - Test suite performance
- **Flaky Tests** - Tests with intermittent failures

### Continuous Improvement
- Add tests for new features
- Improve test coverage
- Reduce test execution time
- Eliminate flaky tests
- Enhance test readability

## 🤝 Contributing to Tests

### Guidelines
1. **Write tests first** - Follow TDD when possible
2. **Test edge cases** - Include boundary conditions
3. **Use descriptive names** - Clear test method names
4. **Keep tests simple** - One assertion per test when possible
5. **Mock external dependencies** - Isolate units under test

### Code Review Checklist
- [ ] Tests cover the main functionality
- [ ] Edge cases are tested
- [ ] Error conditions are handled
- [ ] Tests are readable and maintainable
- [ ] Mocks are properly configured
- [ ] No hardcoded values unless necessary

## 📚 Additional Resources

- [xUnit Documentation](https://xunit.net/)
- [Moq Documentation](https://github.com/moq/moq4/wiki/Quickstart)
- [.NET Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/)
- [GitHub Actions Testing](https://docs.github.com/en/actions/automating-builds-and-tests)
