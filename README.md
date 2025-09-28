# WpfTuneForgePlayer

**TuneForge** is a modern desktop audio player built with **C#** and **WPF**, featuring advanced audio visualization and a sleek Material Design interface. The application combines powerful audio processing capabilities with an intuitive user experience.

---

## ✨ Features

### 🎵 Audio Playback
* Support for common audio formats: **MP3, WAV, FLAC, AAC**
* High-quality audio processing with **NAudio** library
* Advanced audio device management and selection
* Volume control and audio enhancement features

### 🎨 User Interface
* Modern **Material Design** theme with **MahApps.Metro** integration
* **Avalonia UI** components for cross-platform compatibility
* Responsive and intuitive navigation
* Custom audio visualizer with **OpenGL** shaders
* **FluentWPF** styling for enhanced aesthetics

### 📱 Core Functionality
* Playlist management with drag-and-drop support
* Favorite songs collection with persistent storage
* Music directory browsing and file management
* Real-time audio visualization with **OpenTK** graphics
* Comprehensive metadata display using **TagLibSharp**
* Settings panel for audio device configuration

### 🛠️ Advanced Features
* **ReactiveUI** for responsive MVVM architecture
* **CommunityToolkit.Mvvm** for modern data binding
* **Syncfusion** controls for enhanced UI components
* **ActiproSoftware** editors for advanced input controls
* Logging system for debugging and monitoring
* Automated testing with **xUnit** framework

---

> [!IMPORTANT]  
> TuneForge saves your favorite songs in a `FavoriteSong.bin` file within the program folder.
> If this file is deleted, all your favorite songs will be lost.

## 🚀 Technologies and Libraries

### Core Framework
* **.NET Framework 4.8.1** — Primary runtime environment
* **C# 9.0** — Modern language features and syntax
* **WPF** — Windows Presentation Foundation for UI
* **MVVM Pattern** — Clean architecture with separation of concerns

### Audio Processing
* **NAudio 2.2.1** — Comprehensive audio library
* **TagLibSharp 2.3.0** — Metadata extraction and editing
* **OpenTK 3.3.3** — OpenGL bindings for audio visualization

### UI Framework
* **MaterialDesignThemes 5.2.1** — Material Design components
* **MahApps.Metro 2.0.0** — Modern window styling
* **Avalonia 11.3.2** — Cross-platform UI framework
* **FluentWPF 0.10.2** — Fluent Design system
* **Syncfusion Controls** — Professional UI components

### Reactive Programming
* **ReactiveUI 20.4.1** — Reactive extensions for WPF
* **System.Reactive 6.0.1** — Reactive programming framework
* **DynamicData 9.4.1** — Reactive collections

### Development Tools
* **xUnit 2.9.3** — Unit testing framework
* **Moq 4.20.72** — Mocking framework for unit tests
* **CommunityToolkit.Mvvm 8.4.0** — MVVM toolkit
* **Microsoft.Xaml.Behaviors** — XAML behaviors and triggers

---

## 🖼️ Screenshots

![TuneForge Preview](https://i.imgur.com/4oqEfWo.png)
*Main interface with audio visualization*

![Favorite Preview](https://i.imgur.com/uS6bPaD.png)
*Favorite songs management*

![Settings Preview](https://i.imgur.com/Ep5at1i.png)
*Audio device and settings configuration*

![Music Preview](https://i.imgur.com/X2s0IBV.png)
*Music library and playlist view*

> [!NOTE]  
> To remove a song from the list, click on the song and press the **Delete** button.

---

## 🛠️ Installation & Setup

### Prerequisites
- **Windows 10/11** (64-bit recommended)
- **.NET Framework 4.8.1** or later
- **Visual Studio 2022** (for development)

### Building from Source

1. **Clone the repository:**
```bash
git clone https://github.com/IOleg-crypto/WpfTuneForgePlayer.git
cd WpfTuneForgePlayer
```

2. **Restore NuGet packages:**
```bash
dotnet restore
```

3. **Build the solution:**
```bash
msbuild WpfTuneForgePlayer.csproj /p:Configuration=Release
```

4. **Run the application:**
```bash
.\bin\Release\WpfTuneForgePlayer.exe
```

### Development Setup

1. Open `WpfTuneForgePlayer.sln` in **Visual Studio 2022**
2. Restore NuGet packages through Package Manager
3. Build the solution (Ctrl+Shift+B)
4. Run with debugging (F5)

---

## 🧪 Testing

The project includes comprehensive unit tests using **xUnit** and **Moq** frameworks:

### Test Coverage
- **Helpers**: Song, Logger, TimerHelper classes
- **Services**: AudioService, VolumeService functionality
- **Mathematics**: Vector2 operations
- **Integration Tests**: Audio device interactions (manual run)

### Running Tests
```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --verbosity normal

# Run specific test class
dotnet test --filter "SongTests"
```

### Test Structure
```
Tests/
├── Helpers/          # Utility class tests
├── Services/         # Service layer tests
├── Mathematics/      # Math operation tests
└── Properties/       # Test assembly info
```

## 🚀 Automated Builds

This project includes **GitHub Actions** for automated building and releasing:

- **Continuous Integration** on every push to `main` branch
- **Automated Testing** with xUnit test framework
- **Release Packaging** with executable and assets
- **GitHub Releases** with downloadable ZIP files

### Build Status
[![.NET TuneForge](https://github.com/IOleg-crypto/WpfTuneForgePlayer/actions/workflows/dotnet.yml/badge.svg)](https://github.com/IOleg-crypto/WpfTuneForgePlayer/actions/workflows/dotnet.yml)

---

## 📖 Usage

### Basic Operations
1. **Load Music**: Use the file browser to select audio files or entire directories
2. **Playback Controls**: Use play, pause, stop, and track navigation buttons
3. **Volume Control**: Adjust audio levels through the interface
4. **Favorites**: Mark songs as favorites for quick access
5. **Visualization**: Enjoy real-time audio visualization during playback

### Advanced Features
- **Audio Device Selection**: Configure output devices in settings
- **Playlist Management**: Create and manage custom playlists
- **Metadata Viewing**: View detailed song information and album art
- **Keyboard Shortcuts**: Use standard media keys for playback control

---

## 🏗️ Project Structure

```
WpfTuneForgePlayer/
├── Commands/           # Command pattern implementations
├── Helpers/            # Utility classes and converters
├── Mathematics/        # Vector and math utilities
├── Services/           # Audio and business logic services
├── Shader/             # OpenGL shaders for visualization
├── ViewModel/          # MVVM view models
├── Views/              # XAML user interface files
├── assets/             # Application assets and icons
└── bin/                # Compiled binaries
```

---

## 🤝 Contributing

We welcome contributions! Here's how you can help:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/amazing-feature`)
3. **Commit** your changes (`git commit -m 'Add amazing feature'`)
4. **Push** to the branch (`git push origin feature/amazing-feature`)
5. **Open** a Pull Request

### Development Guidelines
- Follow C# coding conventions
- Write unit tests for new features
- Update documentation as needed
- Ensure all tests pass before submitting PR

---

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

- **NAudio** team for excellent audio processing library
- **Material Design** community for UI inspiration
- **OpenTK** developers for OpenGL bindings
- All contributors who help improve this project

---

## 📞 Support

If you encounter any issues or have questions:

1. Check the [Issues](https://github.com/IOleg-crypto/WpfTuneForgePlayer/issues) page
2. Create a new issue with detailed information
3. Join our community discussions

---

**Made with ❤️ using C# and WPF**
