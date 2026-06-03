# Realm Of Ardga

A 2D top-down fantasy RPG built in Unity featuring unique enemy AI, XP and leveling system, skill tree, shop system, and combat systems.

## Gameplay Overview

Players explore a fantasy world, battling mystical enemies and searching for treasure while gaining experience, leveling up, and unlocking unique skills that help them overcome increasingly difficult challenges.

## Features

### Core Systems
- Enemy AI detection and chase system
- Combat mechanics
- Health and damage system
- Loot system
- Experience system
- Skill tree with 3 unique branches
- Persistent player character
- HUD and gameplay UI systems

### Technical Features
- Built with Unity and C#
- Object-oriented architecture
- State-based enemy AI
- Modular gameplay systems
- Event-driven gameplay logic

## Technologies Used

- Unity
- C#
- Visual Studio
- Git/GitHub

## Gameplay Videos
### Enemy Behaviours
[Enemy Behaviour](https://youtu.be/LR2QYrz0JVA)
### Combat and reloading
[Combat & Reloading](https://youtu.be/QS4_MXb0eK4)



## Screenshots
### Mission and Objective UI
![Mission and Objective UI](Bounty/Assets/Images/ScreenShots/Mission.png)
### Game View
![Game View](Bounty/Assets/Images/ScreenShots/Game.png)
### Pause UI
![Pause UI](Bounty/Assets/Images/ScreenShots/PauseUI.png)
### Death UI
![Death UI](Bounty/Assets/Images/ScreenShots/DeathUI.png)
### Interaction UI
![Interaction UI](Bounty/Assets/Images/ScreenShots/InteractUI.png)
### DeskTop UI
![Desktop UI](Bounty/Assets/Images/ScreenShots/DesktopUI.png)

## What I Learned

This project was focused on improving my understanding of gameplay loops and persistent game state and player state when loading new levels. 
Through development, I gained experience with:
- Gameplay architecture
- AI state management
- Combat systems
- Debugging complex gameplay interactions
- 2D game development workflows
- experience and leveling systems
- Persistent states
- 2D sorting layer management
- 2D animation script triggers
- Sprite sort points
- sprite Order in Layer

## Challenges Faced

### Scene And Player Persistence
- One major challenge for me was getting data from one level to persist into the next(player level, unlocked skills, and collected coins).
### Enemy AI
- My first time working with AI, I created a system where enemies can detect the player and when they move into range, chase the player and then attack. Each enemy has a unique attack rate and attack range.
### Animations
- Developing sprite animations that worked correctly with gameplay code, such as activating attack colliders only during specific animation frames.
### Sprite Collisions
- Making sure my sprite collisions make sense for the objects position in the world for example my player being behind a tree and then being able to be in front of it.
### Level Design
- I wanted to make an interesting and engaging world for the player to explore while also benefitting the gameplay loop.

## Future Improvements

- Save and load system
- Additional enemy archetypes
- Improved animations and visual effects
- Boss encounters
- Expanded UI systems
- Additional skills
- Voiced NPCs
- Improved combat mechanics
- Settings menu
- Inventory system
- Unique items
- Ambient sounds (birds chirping, wind blowing, etc.)

## Controls

Realm of Ardga supports both keyboard & mouse and controller input.

### Keyboard & Mouse

| Action | Key |
|----------|----------|
| Move Up | W |
| Move Left | A |
| Move Down | S |
| Move Right | D |
| Attack | Right Mouse Button |
| Double Slash (Skill) | Left Mouse Button |
| Dash (Skill) | Shift |
| Soul Crush (Skill) | Q |
| Use Health Potion | X |
| Pause Game | ESC |

### Controller Support

| Action | Input |
|----------|----------|
| Move | Left Stick |
| Attack | Right Trigger |
| Double Slash (Skill) | Right Bumper |
| Dash (Skill) | Left Bumper |
| Soul Crush (Skill) | Left Trigger |
| Use Health Potion | D-Pad Up |
| Pause Game | Menu Button |



## Play the Game

Realm Of Ardga is available to play on itch.io:

[Play Realm Of Ardga](https://ping-wing.itch.io/realm-of-ard)

## Author
**Jared Akigbesote**
- portfolio: [Portfolio Website](https://awesome-code-monolith-lab.base44.app)
- GitHub: [GitHub Profile](https://github.com/jareda034)
