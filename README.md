# SMB_Clone

A Unity project meant to replicate Super Mario Bros. World 1-1 (Unity 2020.3.2f1)

## Index
---
- [Scene Transitions](#SMB_Clone#scene-transitions)
- Movement
- Timer & Score
- Coinblocks
- Automatic Movement

## Scene Transitions
---
Scene transitions are switched by using a SceneLoader prefab. It switches with a fade-in/out animation as scenes are opened/closed.

// Show scene transitions

## Movement
---
Movement uses a Rigidbody2D component and velocity positioning. It's controlled through the arrow and A/D keys.

// Show movement

Jumping also uses velocity positioning, but vertically. It is mapped with the 'SPACE' key.
<br>

To prevent the character from double jumping, an isGrounded boolean is defined by using the OverlapCircle() method that checks if the character's feet position overlaps the ground layermask.

// Show something

## Timer & Score
---
UI for score, coins, lives, and timer are displayed with the help of a GameManager prefab. As the GameManager updates its' stats, so will the UI.

The statistics are only recorded per attempt through the use of the DontDestroyOnLoad() method. The prefab is also being controlled by using the singleton design pattern.

// Show something

## Coinblocks
---
Upon collision with the character, only animate the block when the character's collider max 'y' position is lower than the coin block's 'y' position.

Coins are instantiated through the use of a coin prefab and as a rigidbody. Force is added on the coin in the 'y' axis and destroyed over time.

// Show coinblock gif

## Automatic Movement
---
An automatic control of movement is implemented as soon as the character comes into contact with the flagpole.

- The controls are disabled with the use of a boolean
- Linear interpolation is utilized to tranform the character's position to the base of the flagpole.
- Followed by changing the character's velocity toward the castle's entrance.

// Show automatic movement