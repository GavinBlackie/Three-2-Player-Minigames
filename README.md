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

## Final Interface
Connect 4:
![connect4Interface](https://github.com/user-attachments/assets/a90ef9d7-f8a4-4384-b0eb-e42c1340edde)

Checkers:
![image](https://github.com/user-attachments/assets/12e39293-38f2-491f-9d60-75455a3d98d2)

## Project Design Diagrams

**The Main Class Diagram:**

(see lower diagrams for "zoom-ins" of this one - NOTE THIS OVERALL DIAGRAM IS OLD, THE SECTION ONES ARE UPDATED TO PRESENT)

![Screenshot 2025-03-17 174038](https://github.com/user-attachments/assets/f1abd78c-363a-437e-bbd5-99df13bafafd)

**Dots & Boxes Section Zoom-in:**

![Screenshot 2025-03-17 182020](https://github.com/user-attachments/assets/427f5c87-dc6e-4fb6-9b61-48abbdccdad6)

**Connect 4 Section Zoom-in:**

![Connect4Zoomin](https://github.com/user-attachments/assets/8c13ed3c-7bfa-4887-80ab-33cfaf4447af)

**Checkers Section Zoom-in:**

![image](https://github.com/user-attachments/assets/1b39cb98-d257-4260-946b-74c02a48f36f)

**Implementation Notes Zoom-in:**

![Screenshot 2025-03-17 182313](https://github.com/user-attachments/assets/e2a58cf2-2752-4888-a99e-15e93df8683e)

## Data Design
+ Checkers:
  - Save player score
  - Save player name
    
+ Connect 4:
  - Save player score.
  - Save player name.
    
+ Dots & Boxes:
  - Save line location.
  - Boxes that have been captured.
  
+ Player Win/Loss Save Data

## Work Assignments
| Contribution       | Artem Kotliar                                  | Gavin Blackie                               | Joseph Thomas                            |
|--------------------|-----------------------------------------------|---------------------------------------------|------------------------------------------|
| **Presentation Layer** | **Connect 4 Page**                           | **Checkers Page**                           | **Dots & Boxes Page**                     |
| **Business Logic**  | `ConnectFourGame`, `SoundPlayer`, `Disk`, `Connect4Exception` | `CheckerGame`, `CheckerGameState`, `Piece`, `Tile` | `Dots&BoxesGame`, `Dots&BoxesGameState`, `Line`, `Box` |
| **Data Layer**      | Handles `PlayerData` using `JSONHandler`          | Handles `CheckerGameState` storage         | Handles `Dots&BoxesGameState` storage     |
| **Report**         | Connect 4 Documentation                      | Checkers Documentation                     | Dots & Boxes Documentation                |



[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/10HqGkJE)
