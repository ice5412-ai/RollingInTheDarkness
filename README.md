# Rolling in the Darkness

A Unity 3D puzzle prototype about guiding a ball through a dark labyrinth by shifting gravity, collecting crystals, and gradually restoring light to the map.

## Portfolio and Repository

* Portfolio Page: https://jintaphumsungkirdportfolio.web.app/project-rolling.html
* GitHub Repository: https://github.com/ice5412-ai/RollingInTheDarkness

This is the GitHub repository for Rolling in the Darkness. For a more complete project presentation with visuals and gameplay context, please visit the Portfolio Page above.

## Overview

Rolling in the Darkness is a student project created for Physics in Game Development at CAMT, Chiang Mai University.

The project is inspired by the classic Maze Ball toy. Instead of directly controlling the ball, the player shifts gravity to guide the ball through a 3D labyrinth. The maze starts in darkness, and the player must collect 12 colored crystals placed across the map. Each crystal restores light to its surrounding zone and expands the player's visible area.

After all 12 crystals are collected, the Pure Crystal appears at the center of the map as the final objective.

## My Role

* Game Director
* Game Design
* Level Design
* Game Design Document
* Unity C# Programming
* Look Development
* Teaser

My work focused on gameplay design, level design, GDD, Unity C# programming, look development, and teaser creation for a playable academic prototype.

## Core Gameplay

The core gameplay is built around gravity control and spatial navigation.

The player uses gravity shifts to roll the ball through the maze, avoid hazards, solve obstacle timing, and search for crystals. Collecting crystals gradually restores visibility, making the map easier to read as the player progresses.

### Gameplay Loop

1. Roll the ball by shifting gravity
2. Navigate maze paths and physics obstacles
3. Find colored crystals in the labyrinth
4. Collect crystals to restore light
5. Repeat until all crystals are collected
6. Return to the center and collect the Pure Crystal

## Key Features

* Gravity-based ball movement
* Dark labyrinth exploration
* Crystal collection progression
* Light restoration feedback
* Physics-based obstacles
* Checkpoint respawn
* Top view and third-person camera toggle
* PC keyboard controls

## Technical / Implementation Highlights

### Gravity Control

The player presses A or D to shift the gravity direction. This changes how the ball rolls through the maze and acts as the main mechanic for movement and obstacle interaction.

### Physics Obstacles

The level includes multiple obstacle types built around timing, momentum, force, friction, and directional movement. These include machine guns, hinge doors, rotating doors, blackholes, sticky obstacles, wrecking balls, wrecking walls, fans, and surfaces with different friction values.

### Checkpoint Respawn

Some hazards reset the ball to the last checkpoint. This keeps failure understandable and allows the player to retry obstacle sections without restarting the entire level.

### Camera Toggle

The player can switch between top view and third-person view. This supports both maze readability and closer movement control.

## Design Highlights

### Movement as the Main Puzzle

The project does not rely on direct character movement. The main challenge comes from understanding how gravity direction, momentum, and maze layout affect the ball's movement.

### Progression Through Visibility

The darkness system gives the player limited information at the start. As crystals are collected, the light radius expands and more of the maze becomes readable. This connects exploration progress with visual feedback.

### Physics-Based Level Design

Each obstacle was designed around gravity control, timing, speed, or positioning. The goal was to make the physics mechanics support the maze experience rather than feel like separate hazards placed into the level.

### Clear Objective Structure

The objective is simple: collect all crystals, then collect the Pure Crystal. This keeps the project accessible for casual players while still allowing the maze and obstacle design to create challenge.

## Tools

* Unity
* C#

## Project Status

Playable academic prototype.

This project was developed as a one-semester class project. It should be viewed as a gameplay and physics prototype rather than a production-ready game.

## What I Learned

Rolling in the Darkness helped me practice designing a playable prototype around one clear core mechanic. I learned how to connect physics-based movement with level structure, player feedback, and progression.

The project also helped me understand how important readability is in a physics puzzle game. When the player is controlling gravity instead of directly controlling the character, camera view, obstacle timing, checkpoint placement, and visual feedback all affect the player experience.

If I continued improving this project, I would focus on clearer onboarding, smoother difficulty pacing between obstacle types, and better camera readability for precise movement sections.
