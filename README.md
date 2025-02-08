# Game of 2048: From HTML Page to VB.NET WinForms

Welcome to the Game of 2048 project! This is a classic number puzzle game implemented in two different platforms: HTML5 (for web browsers) and VB.NET WinForms (for Windows desktop applications). This project serves as a great practice in cross-platform development and demonstrates how to migrate a web-based game to a desktop application.

## Introduction

The Game of 2048 is a simple yet addictive puzzle game where the objective is to combine numbered tiles to create a tile with the number 2048. This project includes two versions of the game:

- **HTML Version**: A web-based implementation that runs in any modern web browser, making it portable across desktop and mobile devices.
- **VB.NET WinForms Version**: A Windows desktop application built using Visual Basic .NET and Windows Forms, requiring .NET SDK 8 and designed specifically for Windows users.

## Game Overview

In the Game of 2048, players are presented with a 4x4 grid. Tiles with numbers appear randomly on the grid, and the player can move these tiles in four directions (up, down, left, right) using arrow keys or touch gestures. When two tiles with the same number collide, they merge into a single tile with the sum of their values. The game continues until the player creates a tile with the number 2048 or until no more moves are possible.

## HTML Version

The HTML version of the game is a single-page web application built using HTML5, CSS3, and JavaScript. It is designed to be lightweight and compatible with both desktop and mobile browsers. Key features include:
- Responsive design for optimal play on any device.
- Touch gesture support for mobile devices.
- Simple and intuitive user interface.
- Game logic implemented in JavaScript.

### How to Run the HTML Version

1. Open the `Game of 2048.html` file in any modern web browser.
2. Start playing by using the arrow keys on your keyboard, or by swiping on the screen if you are on a mobile device.

## VB.NET WinForms Version

The VB.NET WinForms version of the game is a Windows desktop application built using Visual Basic .NET and Windows Forms. It provides a native Windows experience with a focus on performance and ease of use. Key features include __windowed mode__ and:
- Keyboard controls for tile movement.
- High-resolution graphics and colorful tiles.
- Game logic implemented in VB.NET.

### System Requirements

- Windows 10 or later, with .NET SDK 8 installed.

## How to Run the VB.NET WinForms Version

1. Open Visual Studio and create a new VB.NET WinForms project.
2. Copy the contents of `frmMain.vb` into the main form code file.
3. Build and run the project using Visual Studio.
4. Start playing by using the arrow keys on your keyboard.

## Installation and Setup

### HTML Version

No installation required. Simply open the `Game of 2048.html` file in your web browser.

### VB.NET WinForms Version

1. Download and install Visual Studio from the official website.
2. Ensure you have the .NET SDK 8 installed.
3. Create a new VB.NET WinForms project in Visual Studio.
4. Replace the default form code with the contents of `frmMain.vb`.
5. Build and run the project.

## How to Play

- **Objective**: Combine tiles to create a tile with the number 2048.
- **Controls**:
- - **HTML Version**: Use arrow keys on your keyboard or swipe gestures on mobile devices.
- - **VB.NET WinForms Version**: Use arrow keys on your keyboard.
- **Game Over**: The game ends when no more moves are possible or when you achieve the 2048 tile.
- **Restart**:
- - **HTML Version**: Click the "New Game" button to start a new game.
- - **VB.NET WinForms Version**: Press the Space bar to start a new game.

## Contributing

Contributions to this project are welcome! Feel free to fork the repository and submit pull requests with improvements, bug fixes, or new features. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
