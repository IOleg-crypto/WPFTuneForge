# WPFTuneForge

TuneForge is a Windows Forms application developed in C# for audio playback and visualization. The project leverages the powerful TagLib library for audio metadata handling, and integrates a real-time audio visualizer for an enhanced user experience.

⚠️ **TuneForge is currently in development and not fully implemented.**
>
> Some features may be incomplete or unstable. Contributions and feedback are welcome!

## Features

- Audio playback with support for common audio formats (MP3 , WAV)
⚠️
>
> More formats could be added in the future
- Metadata extraction and display using TagLib# 
- User-friendly WPF interface with Windows Forms elements
- Smooth playback controls including play, pause, stop, and track navigation
- Playlist management and song information display

## Technologies and Libraries Used

- **C# with WPF + Windows Forms**: For the desktop graphical user interface
- **TagLib#**: For reading and editing audio metadata tags
- **NAudio**: For audio playback and processing
- **Visual Studio 2022**: The IDE used for development

## Installation

1. Clone the repository:
````
https://github.com/IOleg-crypto/TuneForge.git
````
2. Install libraries using NuGet Package Manager
```
dotnet add package NAudio
dotnet add package taglib
dotnet add package Microsoft.Xaml.Behaviors.Wpf
```


3. Open the solution file in Visual Studio 2022.
4. Restore NuGet packages (TagLib#, NAudio).
5. Build and run the project.

## Usage

- Load audio files into the player.
- Control playback via the provided interface buttons.
- View metadata such as artist, album, title extracted by TagLib.
- Enjoy live audio visualization synchronized with playback.

## Contributing

Contributions and suggestions are welcome. Feel free to open issues or submit pull requests.

## License

This project is licensed under the MIT License.
