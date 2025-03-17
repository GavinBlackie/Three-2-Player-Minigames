# Three 2-Player Minigames - By Team IA10
**Created & Developed By: Joseph, Artem, & Gavin**

## Project Overview
The application is a collection of three 2-player minigames. The players can choose from a selection of games: Connect 4, Checkers, or Dots & Boxes. The primary goal of this project is to provide an engaging and interactive experience for 2 players to compete against each other in classic turn-based strategy games. This project will be developed using Visual Studio as well as Rider on Windows & MacOS platforms. For this project we will use the MAUI framework using C# and XAML to implement the business-logic features and UI presentation. 

## User Interface Wireframes
Connect 4:

![connect4Wireframe](https://github.com/user-attachments/assets/3154bd6e-fc18-4ddf-b584-94ec0a0c54a1)

Checkers:
![checkersWireframe](https://github.com/user-attachments/assets/0dc0e434-2d4b-49ed-b030-fb90533a4738)

Dots & Boxes:
![Dots&BoxesWireframe](https://github.com/user-attachments/assets/86846502-afed-4b00-8eed-f9400bcf3ca1)

## Project Design Diagrams

**The Main Class Diagram:**

(see lower diagrams for "zoom-ins" of this one)

![Screenshot 2025-03-17 174038](https://github.com/user-attachments/assets/f1abd78c-363a-437e-bbd5-99df13bafafd)

**Dots & Boxes Section Zoom-in:**

![Screenshot 2025-03-17 182020](https://github.com/user-attachments/assets/427f5c87-dc6e-4fb6-9b61-48abbdccdad6)

**Connect 4 Section Zoom-in:**

![Screenshot 2025-03-17 182047](https://github.com/user-attachments/assets/10442b9e-a313-4a17-9630-d7158fb8c7da)

**Checkers Section Zoom-in:**

![Screenshot 2025-03-17 182128](https://github.com/user-attachments/assets/429d46bb-5e02-4b48-8dec-3c49e5d17bef)

**Implementation Notes Zoom-in:**

![Screenshot 2025-03-17 182313](https://github.com/user-attachments/assets/e2a58cf2-2752-4888-a99e-15e93df8683e)

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
