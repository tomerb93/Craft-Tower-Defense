# Craft-Tower-Defense

## Log of progress

- Added gridManager to store information of grid nodes and grid coordinate labeler, used for debugging:

![Used for debugging](READMEMedia/GridLabeler.jpg)

- Added Pathfinder with BFS traversal which uses the grid of nodes 
- Tile label colors dynamically set according to BFS traversal

![Used for debugging](READMEMedia/GridBFSLabelColor.jpg)

- Built path using references created during explore neighbors

![Path built](READMEMedia/GridBFSPathBuilt.jpg)

- Initial enemy prefab added, and added enemy movement to follow path from BFS

[Link](https://gifs.com/gif/enemymovement-6WzEzL)
![Enemy movement](https://j.gifs.com/6WzEzL.gif)

- Added ObjectPool to instantiate the enemies with a predefined size and timer between instantiations (using a coroutine)