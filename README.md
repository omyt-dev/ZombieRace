# ZombieRace

A small mobile arcade game prototype built with Unity.

The player controls a turret mounted on a moving car, destroys incoming zombies, and tries to reach the end of the level while keeping the car alive.

## Gameplay

* The car moves forward automatically after the game starts.
* Zombies are spawned ahead of the player and begin chasing the car when they get close enough.
* The player controls the turret and shoots zombies.
* Zombies damage the car on impact.
* The game ends with either **Win** or **Lose**.
* The level can be restarted after the game ends.

## Controls

### Mobile

* **Tap** — start the game / restart after Win or Lose.
* **Horizontal touch movement** — rotate the turret.
* **Hold touch** — fire the weapon.

### Desktop

* **Left mouse button** — start/restart and control the turret.
* **Mouse movement** — rotate the turret.

## Features

* Automatic forward car movement
* Enemy detection and chase behavior
* Enemy and car health systems
* Turret aiming and shooting
* Projectile object pooling
* Enemy object pooling
* Particle effect pooling
* Hit reactions and hit flashes
* Car hit reaction and explosion effect
* Tire tracks and exhaust effects
* Level progress tracking
* Win/Lose game flow
* Mobile frame-rate configuration
* Runtime level generation and ground streaming

## Architecture

The project uses a lightweight component-based architecture focused on keeping gameplay systems independent without introducing unnecessary abstractions.

### Dependency Injection

[Zenject](https://github.com/modesttree/Zenject) is used for dependency injection and composition root configuration.

### Game Flow

The global game flow is controlled by a simple state machine:

`Ready → Playing → Win / Lose → Ready`

`GameSession` coordinates high-level game flow, while `GameStateMachine` is responsible only for state transitions.

### Configuration

Gameplay parameters are stored in `ScriptableObject` configuration assets, allowing values such as health, movement speed, damage and level settings to be tuned without changing code.

### Object Pooling

Frequently spawned objects use pooling to avoid unnecessary runtime allocations:

* Enemies
* Projectiles
* Particle effects
* Ground segments

### Events

Components communicate through local events where appropriate, for example:

* Health changes
* Damage
* Death
* Level completion
* Game state changes

This keeps gameplay components from depending directly on UI or unrelated systems.

## Tech Stack

* Unity 6
* C#
* Unity Input System
* Zenject
* Cinemachine
* DOTween
* TextMeshPro
* Unity Animation Rigging

## How to Run

1. Clone the repository.
2. Open the project with **Unity 6000.3.23f1**.
3. Open the main gameplay scene.
4. Press Play.

For Android, build the project as an APK from Unity's Android build settings.

## Download

[Download Android APK](../../releases/latest)

## Gameplay Video

[Watch gameplay video](https://drive.google.com/file/d/1Mxp2u3eH9LFAynhgeE1vHa3RGl6h_CAn/view?usp=sharing)

The video demonstrates both **Win** and **Lose** scenarios.
