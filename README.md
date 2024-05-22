## EndGameTask

This project can be deconstructed into five main components:

### Game Entities

I used a reusable approach for creating both players and enemies. These entities share many of the same scripts but are used differently by the player manager and enemy manager. For the enemy AI, I implemented steering behaviors (wandering, chasing...) to give them a natural feel

### Player Input

This project uses the Unity Input System to handle both PC and mobile gamepad bindings.For the player attack system, I took inspiration from games like Zooba, Brawl Stars, and Zombs Royale.

### Components

Each game entity has different components that serve a single purpose, such as the weapon controller and health controller. 

###  Interactables

For the player to interact with buildings, keys, or enemies, I used a reusable approach with interfaces. Each component is responsible for handling how it responds to player interactions

### Inventory

The player can save interactables inside an inventory. I used a Scriptable Object game events architecture approach to ensure the inventory is not reliant on the player, and vice versa. The player changes the inventory Scriptable Object, and every other object can easily listen for these changes.


A pc and mobile executables are included in the executables folder
