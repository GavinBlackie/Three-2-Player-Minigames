# Three 2-Player Minigames - By Team IA10
**Created & Developed By: Joseph, Artem, & Gavin**

## Project Overview
The application is a collection of three 2-player minigames. The players can choose from a selection of games: Connect 4, Checkers, or Dots & Boxes. The primary goal of this project is to provide an engaging and interactive experience for players to compete against each other in classic turn-based strategy games. This project will be developed using Visual Studio as well as Rider on Windows & MacOS platforms. For this project we will use the MAUI framework using C# and XAML to implement the business-logic features and UI presentation. 

## User Interface Wireframes
Connect 4:

<img width="764" alt="Screenshot 2025-03-15 at 5 11 43 PM" src="https://github.com/user-attachments/assets/3154bd6e-fc18-4ddf-b584-94ec0a0c54a1" />

Checkers:
![checkersWireframe](https://github.com/user-attachments/assets/0dc0e434-2d4b-49ed-b030-fb90533a4738)

Dots & Boxes:
![Dots&BoxesWireframe](https://github.com/user-attachments/assets/86846502-afed-4b00-8eed-f9400bcf3ca1)

## Project Design Diagrams

![Screenshot 2025-03-16 225304](https://github.com/user-attachments/assets/95480028-83d7-4ee7-810a-4a3c2aff6f88)

<!---
![Screenshot 2025-03-15 190909](https://github.com/user-attachments/assets/b9cdc1ee-e8f6-4468-83db-8b268042f8d3)
--->

## Data Design
+ Checkers:
  - Save piece location.
  - Save whether the piece is a king.
    
+ Connect 4:
  - Save disk location.
  - Amount of disks left for each player.
    
+ Dots & Boxes:
  - Save line location.
  - Boxes that have been captured.
  
+ Player Win/Loss Save Data

## Work Assignments
| Contribution       | Artem Kotliar                                  | Gavin Blackie                               | Joseph Thomas                            |
|--------------------|-----------------------------------------------|---------------------------------------------|------------------------------------------|
| **Presentation Layer** | **Connect 4 Page**                           | **Checkers Page**                           | **Dots & Boxes Page**                     |
| **Business Logic**  | `Connect4Game`, `Connect4GameState`, `TurnTimer`, `Disk` | `CheckerGame`, `CheckerGameState`, `Piece`, `Tile` | `Dots&BoxesGame`, `Dots&BoxesGameState`, `Line`, `Box` |
| **Data Layer**      | Handles `Connect4GameState` storage          | Handles `CheckerGameState` storage         | Handles `Dots&BoxesGameState` storage     |
| **Report**         | Connect 4 Documentation                      | Checkers Documentation                     | Dots & Boxes Documentation                |



[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/10HqGkJE)
