# Four In A Row - Console Game

A C# console-based implementation of the classic **Four In A Row** game, where two players take turns dropping coins into columns on a grid, aiming to align four coins in a row horizontally, vertically, or diagonally. This project emphasizes clean design and core OOP principles.

## Features

- **Two-player gameplay** with alternating turns
- **Player vs. Computer** mode (random moves for computer)
- Configurable grid size between **4x4 and 8x8**
- **Game state tracking**: win, tie, and ongoing status
- **Scorekeeping** across multiple rounds
- Modular design separating **game logic and UI layers**

## Design Principles

This project was designed with several software engineering principles in mind:
- **Separation of concerns** between logic and UI for clear responsibility boundaries
- **Encapsulation** to protect internal data and ensure controlled access
- **Modular and reusable design** to facilitate future improvements or UI changes

## Setup

### Prerequisites
- **.NET SDK**: Ensure you have the .NET SDK installed. [Download here](https://dotnet.microsoft.com/download).

### Build and Run
1. Clone the repository:
    ```bash
    git clone https://github.com/your-username/FourInARow.git
    cd FourInARow
    ```
2. Build the project:
    ```bash
    dotnet build
    ```
3. Run the game:
    ```bash
    dotnet run --project FourInARow.ConsoleApp
    ```
- **Notes** The above instructions are tailored for command line users. If you prefer using an IDE, you can open the solution file (.sln) in Visual Studio, Visual Studio Code, JetBrains Rider, or any other C# compatible development environment.
Ensure your IDE is configured with the necessary C# extensions and the .NET SDK.


## Usage

1. Choose the board size (between 4x4 and 8x8).
2. Decide whether to play against another player or the computer.
3. Players take turns choosing a column to drop their coin. The goal is to align four coins in a row.
4. View results after each round, and choose to play again or exit.

## Code Structure

- **FourInARow.Core**: Contains core game logic, including board setup, move validation, and victory conditions.
- **FourInARow.ConsoleApp**: The console user interface, handling player input and display.
- **ConsoleUtils**: A DLL utility for console management, referenced in the project.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request for any feature additions, improvements, or bug fixes.

## License

This project is licensed under the MIT License.
