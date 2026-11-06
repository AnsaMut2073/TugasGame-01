## Game States
a. **MainMenu**
b. **MainScene**
c. **PauseGame**
d. **Inventory**
e. **GameOver**

## Gameplay Mechanics & Features
1. **Enemy:**
   - There are two spawn points, the enemies will continue to appear every 3 seconds from their respective spawn points.
   - Have 3 states: Idle, Patrol, Chase, and Attack.
   - After attacking the player, they will istantly die.
   - Each enemy = HP -10.
2. **Fighting:**
   - The player has an HP system, default value = 100.
   - The player can perform an attack action using a sword.
   - Just by a single slash, enemy will die immediately (because Enemy didn't have the HP system yet).
3. **Inventory:**
   - 1 slot = 1 type of item.
   - Maximum amount for each type is 99.
   - Can only store items (because there is no feature to retrieve items from the bag / discard item yet).

## How To Play (Keyboard Only)
- Walk = WASD
- Attack = Left click mouse
- Inventory = I
   a. Select item = Left click mouse
   b. Deselect item = Right click mouse
- Pause game = Esc