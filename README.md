# WPFTuneForge

**TuneForge** is a desktop application built with **C#**, using WPF and Windows Forms, for audio playback and visualization. The project leverages **TagLib#** for handling audio metadata and includes a real-time audio visualizer for an enhanced user experience.

---

## Features

* Audio playback with support for common formats: **MP3, WAV**
  ⚠️ More formats may be added in the future
* Metadata extraction and display using **TagLib#**
* User-friendly **WPF interface** with Windows Forms elements
* Smooth playback controls: **play, pause, stop**, and track navigation
* Playlist management and song information display

---

## Technologies and Libraries

* **C# with WPF + Windows Forms** — for the desktop GUI
* **TagLib#** — for reading and editing audio metadata
* **NAudio** — for audio playback and processing
* **Visual Studio 2022** — development environment

---

## Preview

![TuneForge Preview](https://i.imgur.com/4oqEfWo.png)
![Favorite Preview](https://i.imgur.com/uS6bPaD.png)
![Settings Preview](https://i.imgur.com/Ep5at1i.png)
![Music Preview](https://i.imgur.com/X2s0IBV.png)

---

## Installation

1. Clone the repository:

```bash
git clone https://github.com/IOleg-crypto/TuneForge.git
```

2. Restore NuGet packages (TagLib#, NAudio):

```bash
dotnet restore
```

3. Open the solution file in **Visual Studio 2022**.
4. Build and run the project.

---

## Usage

* Load audio files into the player.
* Control playback via the interface buttons.
* View metadata such as artist, album, and title extracted by TagLib#.
* Enjoy live audio visualization synchronized with playback.

> [!IMPORTANT]  
> TuneForge saves your favorite songs in a `FavoriteSong.bin` file within the program folder.
> If this file is deleted, all your favorite songs will be lost.

---

## Contributing

Contributions and suggestions are welcome! Feel free to open issues or submit pull requests.

---

## License

This project is licensed under the **MIT License**.
