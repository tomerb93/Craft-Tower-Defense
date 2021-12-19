# Craft-Tower-Defense

## Log of progress

![Used for debugging](READMEMedia/GridLabeler.jpg)

- Added gridManager to store information of grid nodes and grid coordinate labeler, used for debugging:


![Used for debugging](READMEMedia/GridBFSLabelColor.jpg)

- Added Pathfinder with BFS traversal which uses the grid of nodes 
- Tile label colors dynamically set according to BFS traversal

![Path built](READMEMedia/GridBFSPathBuilt.jpg)

- Built path using references created during explore neighbors


[Link](https://gifs.com/gif/enemymovement-6WzEzL)
![Enemy movement](https://j.gifs.com/6WzEzL.gif)

- Initial enemy prefab added, and added enemy movement to follow path from BFS

- Added ObjectPool to instantiate the enemies with a predefined size and timer between instantiations (using a coroutine)
Enemies are predefined and are enabled/disabled instead of destroyed (more efficient).

![Block node and recalculate path](https://j.gifs.com/79Onqr.gif)

- Added an option to block nodes and Pathfinder now broadcasts message to all enemies to recalculate their path when a node has been blocked. Prevented situation where node will cause the path to be blocked.