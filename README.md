# Space-Shooter

## Description

A simple space invader - themed game 

## Frameworks used

- SplashKit: https://splashkit.io

## Features

- Consists of 4 different game mode: By Level, Boss Fight, Survival and Mine Field
  - By Level mode includes a wave of different enemies and a boss fight at the end
  - Boss Fight mode only spawns boss enemies
  - Survival tests the player with increasing difficulty
  - Mine Field mode features waves of asteroids and space mines
- Player can choose from 3 types of spaceships:
  - Versatile: offers medium speed and armour
  - Agile: sacrifices armour for speed 
  - Armoured: heavily armoured at the expense of speed
- Spaceships contain 2 different type of bullets: 
  - Blue Laser: medium speed and damage
  - Red Beam: higher speed at lowered damage

## Design patterns:

- Factory pattern: used in Factory.cs to create enemies
- Strategy pattern: used in MovePattern.cs to encapsulate different movement behaviours
- State pattern: used Game.cs to encapsulate different states of the game

