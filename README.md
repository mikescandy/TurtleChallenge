# TurtleChallenge

## Assumptions
The board is modeled over the first quadrant of a cartesian plan. The origin (0,0) is in the bottom left corner.

Board tiles coordinates are 0 based.

The turtle can't move outside of the board. Trying to do that will end the processing of the moves.

## Compilation instructions
Run
```
dotnet restore
dotnet build
```
in the repository folder.

The binary will be in the 
``` 
./TurtleChallenge/bin/debug/netcoreapp3.1
```
folder.

## Settings file syntax
Settings file must be a json file with the following structure:
```
{
	"board": {
		"width": 4,
		"height": 6,
		"mines": [
			{ "x": 2, "y": 0},
			{ "x": 1, "y": 3},
			{ "x": 2, "y": 4},
			{ "x": 3, "y": 2}
		]
	},
	"startTile": {"x": 0, "y": 3},
	"exitTile": {"x": 3, "y": 1},
	"direction": "North"
}
```
Width must be a positive integer

Height must be a positive integer

Mines is a collection of Tiles. A Tile X and Y properties must be positive integers.

StartTile and EndTile are Tiles. A Tile X and Y properties must be positive integers.

The program validates the consistencty of the data provided (ranges, tiles must not overlap).


## Moves file syntax
Sequence of moves are defined as a sequence of comma separated actions. Each line defines a sequence of moves.
```
r,r,m,m,r,r,r,m,m,m
m,r,m,r,r,r,m,r,m,m,r,m,m,r,m,r,r,r,m,m,r,r,r,m
m,r,m,m
r,r,r,m
```

Valid moves are:
- m : Move forward
- r : Turn right

## Running the program

Call the program executable passing two arguments, the name of the settings file and the name of the moves file.

The project includes a sample settings file and a sample moves file, named respectivle sampleSettings and sampleMoves

```
TurtleChallenge.exe sampleSettings sampleMoves
```

In case of valid parameters provided, for each  sequence of moves the output will be one of the following text:
- Still in danger! (The turtle didn't hit any mine, didn't find the exit and is still on the board)
- BOOM! The turtle exploded. (The turtle hit a mine)
- The turtle found the exit and is safe! (The turtle found the exit without hitting any mine and without ever leaving the board)
- Uh oh, you took a wrong turn. (The turtle moved outside the board bounds)
