# Solve-8Puzzle-Problem
solving 8 puzzle problem using BFS and A* algorithm
# The 8-Puzzle Problem

The 8-puzzle problem is a classic puzzle and a type of sliding puzzle that consists of a 3x3 grid with eight numbered tiles and one empty space. The goal of the puzzle is to rearrange the tiles from their initial configuration into a target or goal configuration by sliding them one at a time into the empty space. The target configuration is usually a specific order of numbers, such as in ascending order from left to right, top to bottom, with the empty space in the bottom-right corner.
![Screenshot 2023-10-01 102655](https://github.com/HeshamHM/Solve-8Puzzle-Problem/assets/65486855/40b96f68-6ab4-4422-a525-3b731df09581)
https://github.com/HeshamHM/Solve-8Puzzle-Problem/assets/65486855/1dacd412-51b6-4640-a40f-ef9bf9c0aac4
## Key Characteristics and Rules
- **Grid Configuration**: The puzzle is played on a 3x3 grid, resulting in a total of nine squares. Eight of these squares contain numbered tiles (usually from 1 to 8), and one square is empty.

- **Initial Configuration**: The puzzle starts with an initial configuration of the tiles, which may be a random arrangement or a specific pattern. The goal is to transform this initial configuration into the target configuration.

- **Valid Moves**: Only certain moves are valid. A tile can be moved into the adjacent empty space if it is horizontally or vertically adjacent to it. Diagonal moves are not allowed.

- **Objective**: The objective is to find a sequence of moves that transforms the initial configuration into the target configuration.

- **Complexity**: The 8-puzzle problem is a classic example in the field of artificial intelligence and computer science, particularly in the study of search algorithms and heuristic methods. It is used to demonstrate and test algorithms for solving problems involving state spaces.

- **Solvability**: Not all configurations of the 8-puzzle are solvable. Some formats are unsolvable, meaning there is no sequence of moves that can transform them into the goal configuration. Solvability depends on the initial and target arrangements.

- **Heuristic Functions**: To efficiently solve the 8-puzzle, various heuristic functions are used to estimate the number of moves required to reach the goal. One common heuristic is the Manhattan distance, which calculates the sum of the horizontal and vertical distances each tile is away from its goal position.

- **Algorithmic Approaches**: Solving the 8-puzzle involves searching through a state space of possible configurations. Common search algorithms used for solving the 8-puzzle include A* search with heuristics, breadth-first search, and depth-first search.
  ## How to Determine Solvability of the 8-Puzzle

To determine if a given instance of the 8-puzzle problem can be solved (i.e. if it has a solution), you can use the concept of "parity." The solvability of the 8-puzzle depends on the initial configuration of the puzzle tiles. Follow these steps to check the solvability:

1. **Count Inversions**:
   - An "inversion" occurs when a tile with a higher number precedes a tile with a lower number in the order of the tiles.

   - For each tile, count the number of tiles that come after it with lower numbers but ignore the empty space (represented as 0). This count is the number of inversions for that tile.
   - Sum up the inversion counts for all tiles in the puzzle.

2. **Parity of Inversions**:
   - If the total number of inversions in the initial configuration is even, the puzzle is considered solvable.
   - If the total number of inversions is odd, the puzzle is considered unsolvable.

3. **Special Consideration for the Blank Tile**:
   - When counting inversions, you might encounter a situation where the blank tile (represented as 0) is in an odd row (when counting rows from top to bottom, starting from 1).
   - In such cases, you need to adjust the inversion count by adding 1 to it.

### Example

Suppose you have the following initial configuration of the 8-puzzle:
```
1 2 3
4 5 6
8 7 0
```
Counting inversions:
- Tile 1 has 0 inversions.
- Tile 2 has 0 inversions.
- Tile 3 has 0 inversions.
- Tile 4 has 0 inversions.
- Tile 5 has 0 inversions.
- Tile 6 has 0 inversions.
- Tile 8 has 3 inversions (it precedes tiles 7).
- Tile 7 has 0 inversions.
- Tile 0 (blank) has 0 inversions.

Total inversions = 0 + 0 + 0 + 0 + 0 + 0 + 1 + 0 + 0 = 1

Since the total number of inversions is odd, this initial configuration is unsolvable.

It's important to note that not all initial configurations of the 8-puzzle are solvable. If an initial configuration is unsolvable, there is no sequence of moves that can transform it into the goal configuration. You can use the inversion count method to quickly assess solvability before attempting to solve the puzzle.





