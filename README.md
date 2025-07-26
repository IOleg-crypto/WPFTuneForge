# WPFTuneForge

TuneForge is a Windows Forms application developed in C# for audio playback and visualization. The project leverages the powerful TagLib library for audio metadata handling, and integrates a real-time audio visualizer for an enhanced user experience.

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

## Preview
![TuneForge Preview](https://i.imgur.com/4oqEfWo.png)
![Favorite Preview](https://i.imgur.com/uS6bPaD.png)
![Settings Preview](https://i.imgur.com/Ep5at1i.png)
![Music Preview](https://i.imgur.com/X2s0IBV.png)

## Installation

1. Clone the repository:
````
https://github.com/IOleg-crypto/TuneForge.git
````
2. Restore NuGet packages (TagLib#, NAudio).
```
dotnet restore
```
3. Open the solution file in Visual Studio 2022.
4. Build and run the project.

## Usage

- Load audio files into the player.
- Control playback via the provided interface buttons.
- View metadata such as artist, album, title extracted by TagLib.
- Enjoy live audio visualization synchronized with playback.

## Contributing

Contributions and suggestions are welcome. Feel free to open issues or submit pull requests.

## License

This project is licensed under the MIT License.
