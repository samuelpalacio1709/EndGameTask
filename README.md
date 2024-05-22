# EndGameTask

This project can be deconstructed into five main components:

### Game Entities

I used a reusable approach for creating both players and enemies. These entities share many of the same scripts but are used differently by the player manager and enemy manager.
For the enemy AI, I implemented steering behaviors (Wandering, chasing...) to give them a natural feeling

### Player Input

This project uses the Unity Input System to handle both PC and mobile gamepad bindings.

### Components

Each game entity has different components that serve a single purpose, such as weapon controller and health controller.

For the player attack system, I took inspiration from games like Zooba, Brawl Stars, and Zombs Royale.

### Interactables

For the player to interact with buildings, keys, or enemies, I use a reusable approach with interfaces. Each component is responsible for handling how to respond to player interactions.

### Inventory

The player can save interactables inside an inventory. I used a Scriptable Object game events architecture approach to ensure the inventory is not reliant on the player, and vice versa. The player changes the inventory scriptable object, and every other object can easily listen for these changes.
