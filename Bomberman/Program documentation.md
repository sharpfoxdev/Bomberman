This game is a clone of bomberman game for two players. The code consists of several classes in separate files. 

# Form1.cs
## partial class Form1
	- entry point of the code, handles window and initialises game

	public Form1():
		- initialises window
		- makes new instance of game

	private void pictureBox1_Paint()
		- tells the game to draw itself (it further tells other instances to draw themselves)

	private void timer1_Tick()
		- is called regularly thus making game loop
		- when game is over, the timer is stopped and MessageBox displays who won

	private void Form1_KeyDown()
		- tells the game which key was pressed

	private void Form1_KeyUp()
		- tells the game which key was lifted


# Game.cs
	## enum Direction
		- used by class Game and class Player to determine orientation and movement of the player. Also used by class Bomb to initialise instances of class Explosion

	## class Game
		- central class of the code, its instance is passed to all of the instances of other classes, thus they can comunicate via this Game instance (which was initialised in Form1)

		fields and properties:
			+int tileSize //size in px of the tiles (sand, wall, crate), bombs, explosion and bonuses
			+int playerSize //size in px of game players (they are slightly smaller than the tiles for easier movement in the game)
			-string pathToPlan //path to where the plan of mapgrid is stored
			-int amountOfPlayers
			+bool gameOver //true if game is over

			+Map map //instance of map, does all the map related stuff
			+List<Player> players //stores all players in list (so we can iterate through them)
			+PictureManager pictureManager //knows pictures of all sprites
			+SoundManager //can play sounds


		public Game()
			- makes instances of players, map sound manager and picture manager, sets tileSize and playerSize
			- starts to play backgroundMusic on loop

		public void Draw(Graphics g)
			- tells map and all players to draw themselves, is called from Form1

		public void KeyDown(Keys key)
			- called from Form1, sets player's Direction and its picture according to pressed key or tells the player to place bomb

		public void KeyUp(Keys key)
			- sets Direction of player to NONE when the given key is lifted (because of that the player will stop moving)

		public void Step()
			- called from Form1 from timer1_Tick() method regularly
			- tells map to do step, checks for game over and then tells all players to do step as well

# Map.cs
## class Map
	- takes care of loading map, generating bonuses, drawing everything except for players, telling objects (exept for players) to do step, stores map grid and gives information about it, deletes and adds objects into map when needed

	fields and properties:
		+Tile[,] mapGrid //2D array of Tiles (sand, wall, crate)
		-List<GameObject>objects, objectsToAdd, objectsToDelete //lists of objects, objects that appeared and objects that disappeared during the step
		-int width, height //size of mapGrid
		-Random generator //for generating bonuses into crates randomly
		-Game game //to comunicate with players and pictureManager through Game instance
		-tileSize //size in px of tiles, used for their drawing in correct places

	public Map(Game game, int tileSize, string pathToPlan)
		-called from Game, has just one instance
		-opens file in pathToPlan, reads the width, height and the plan itself, makes instances of Tiles according to the letter in the plan and adds them into mapGrid into correct place
		-generates with generator the contents of crates that will become visible when the crate is destroyed
			-generator generates random int in small range and according to the generated number there will be placed either bonusBomb, BomusSpeed, BonusExplosion or nothing at all
		-also tells players their position

	public void Draw(Graphics g)
		- called from Game
		- draws all tiles in the correct position using tileSize
		- tells all objects in ist of objects (bombs, explosions, bonuses) to draw themselves, if they are visible (for example bonus in the crate isnt visible thus wont be drawn)

	public void Step()
		- called from Game
		- tells all objects in the list of objects to do step and then checks if those objects colide with any of the players - if yes and the object is pickable, the player will "pick" it and the object moves into objectsToDelete (it disapeared)
		- then it iterates through objectsToDelete and deletes all of those in objects, then clears objectsToDelete
		- finally it iterates through objectsToAdd and adds all of those into list of objects, then clears objectsToAdd

	public bool IsStepable(int x, int y)
		- called mostly from Player when he checks for movement possibilities
		- gets the coordinates in window, matches this coordinates into mapGrid and returns whether the Tile on given position is stepable or 
		- also called from Bomb when it checks whether it should place Explosion on the tile

	public bool IsDestroyable(int x, int y)
		- called from Bomb when it checks if the explosion reached crate
		- same like IsStepable(), just checks if the tile is destroyable instead

	public void DeleteObject(GameObject obj)
		- called from explosion (when it destroys something), bomb (when it disappears after explosion) or from here when the player picks bonus
		- adds object into objectsToDelete

	public void AddObject(GameObject obj)
		- called from Player when it makes Bomb (adds instance of Bomb) and from Bomb when it makes Explosion (adds instance of Explosion)
		- adds object into objectsToAdd

	public List<GameObject> ReturnGameObjects()
		- called from Explosion to check for its collision with any of the objects
		- returns objects

# GameObject.cs
## class GameObject
	- abstract class, all game objects (bonuses, explosions, players, bombs) inherit from it

	- fields and properties:
		+Bitmap picture //stores picture of the object
		+Point position //position of up left corner of object from the up left corner of picturebox, in pixels
		protected Game game //all game objects know the same game instance
		+bool pickable //true if the player can pick it
		+bool visible //true if the game object should be drawn

	public void Draw(Graphics g)
		- uses graphics to draw Bitmap picture into Point position

	public abstract void Step()
		- all of the objects can make step, each game objects has here code that is executed every step the game makes

	public bool Collision(GameObject obj)
		- checks for collision with another game object
		- returns true if this object and the object given as parameter collide

# Player.cs
## class Player : GameObject
	- takes care about players movement and placing bombs

	- fields and properites:
		+int bombStrenght //strenght of detonation
		+bool dead //true if dead
		+Direction orientation //which way is the player facing
		+int amountOfBombs //how many bombs can player place at once
		+int timeSpeededUp //how much time is left when the player moves faster due to speed bonus
		-int speed //how many px does the player move itself every step

	public Player(Game game, int numberOfPlayer)
		- called from game
		- is visible on map, cant be picked
		- bombStrenght is 1 in the beginning, rises when player picks BonusExplosion
		- orientation = Direction.NONE //doesnt move
		- amountOfBombs is 1 in the beginning, rises when player pics BonusBomb
		- speed in the beginning is 2 px, temporary rises when player picks BonusSpeed
		- checks which player he is and loads correct picture

	public override void Step()
		- checks what speed he is supposed move
		- then if possible, moves in the direction of set orientation (asks game.map if he can step on the new position)
		- if orientation = Direction.NONE then doesnt move (when no key is pressed that would move it)

	public void PlaceBomb()
		- if he still has some bombs to place (amountOfBombs is not zero) then he loses one bomb and places it on position where he is standing (but changes the position slightly so the bomb will be placed in the grid on one spot)
		- player passes game instance, bombStrenght and itself to the new instance of bomb (so it knows game, how strng it is and who placed it)
		- adds bomb into map's objects so it can now also make steps

# Bomb.cs
## Bomb : GameObject
	- created by player, when the time comes, it detonates and makes new instances of Explosion

	- fields and properties:
		-int bombStrenght //tells how many Explosion instances it should make in each direction
		-int timeTillDetonation //gets smaller every step until it reaches zero and the bomb explodes
		-Player whoPlacedIt //so it can return the bomb to the player after it detonates

	public Bomb(Game game, int bombStrenght, Player whoPlacedIt)
		- initialises fields, is visible but not pickable
		- loads its picture

	public override void Step()
		- every step timeTillDetonation gets smaller
		- when it reaches zero:
			- sound of explosion is played
			- player who placed this bomb gets one bomb back
			- this bomb disappears (game.map.DeleteObject())
			- in its spot there appears explosion
			- then MakeExplosion() is called that makes new Explosions in all four directions accoring to the bombStrenght

	private MakeExplosion(Direction direction)
		- makes new Explosions in the given direction and with given bombStrenght
		- from the direction and the bomb position calculates positions of new Explosions
		- if the place where we want to place new explosion is stepable (sand), then we put there the explosion, add it to map's objects and then we continue in generating another explosion in the given direction
		- if the place is destroyable (crate), then we put explosion on its place, add explosion in maps's objects but then we stop further creating new explosions
		- if the place is nor stepable nor destroyable (wall), we stop generating new explosions in this direction

# Explosion.cs
## class Explosion : GameObject
	- made by bomb, stays for a bit in place, destroys visible bonuses, kills players, destroys crates
	- fields and properties:
		-int timeOfExploding //gets smaller every step, upon reaching zero the explosion disappears

	public Explosion(Game game)
		- gets its picture, is visible, isnt pickable

	public override void Step()
		- timeOfExploding gets smaller
		- checks if colides with any of the players (player stepped in the explosion) - if yes, then the player is dead
		- when timeOfExploding reaches zero
			- checks whether the tile on the explosions place is destroyable and not stepable (crate)
				-if yes, puts sand instead of the crate in the mapGrid
			- gets list of all objects from map
			- deletes itself
			- checks all objects from map
				- if the object collides with the explosion, is visible and pickable (bonus), then it will be deleted (game.map.DeleteObject)
				- if the object colides with explosion and isnt visible, then it becomes visible (bonus that was in the crate but now the crate is destroyed)

# BonusBomb.cs
## class BonusBomb : GameObject
	
	public BonusBomb(Game game)
		- gets its picture, is not visible (until the explosion makes it visible), is pickable
	public override void Step()
		- iterates through players, checks if it colides with any of them, if yes, his amountOfBombs gets higher (can place one more bomb at once)

# BonusExplosion.cs
## class BonusExplosion : GameObject
	public BonusExplosion(Game game)
		- gets its picture, is not visible (until the explosion makes it visible), is pickable
	public override void Step()
		- iterates through players, checks if it colides with any of them, if yes, his bombStrenght gets higher (detonations of bombs they place is greater)

# BonusSpeed.cs
## class BonusSpeed : GameObject
	public BonusSpeed(Game game)
		- gets its picture, is not visible (until the explosion makes it visible), is pickable
	public override void Step()
		- iterates through players, checks if it colides with any of them, if yes, his timeSpeededUp is set to positive value (has temporary speed boost)

# Tile.cs
## class Tile
	- takes care of tiles (sand, crate, wall)
	- sand - not destroyable, stepable
	- wall - not destroyable, not stepable
	- crate - destroyable, not stepable

	- fields and properties:
		-Bitmap picture //stores picture of the tile
		+bool destroyable //true if explosion can destroy this tile
		+bool stepable //true if player can step on this tile

	public Tile(Game game, bool stepable, bool destroyable)
		- called from map when making grid and from explosion when it makes sand tile in the place of destroyed crate
		- sets picture, destroyable and stepable

	public void Draw(int x, int y, Graphics g)
		- draws the tile on given spot

# PictureManager.cs
## class PictureManager
	- knows pictures of all sprites and tiles

	- properties:
		+Bitmap wall, crate, sand
		+Bitmap player1Up, player1Down, player1Left, player1Right
		+Bitmap player2Up, player2Down, player2Left, player2Right
		+Bitmap bomb, explosion
		+Bitmap bonusExplosion, bonusSpeed, bonusBomb

	public void LoadPictures()
		- called from game in its constructor
		- loads all the sprites and tiles from path and stores them

# SoundManager.cs
## class SoundManager
	- knows all the sounds used in game
	- properties:
		+SoundPlayer backgroundMusic

	public void LoadSounds()
		- loads background music from path and stores it
		- called in the constructor of the Game

	public void PlayBomb()
		- plays bomb sound using MediaPlayer class
		- winforms doesn't usually want to play two sounds at once, that's why I'm using MediaPlayer instance instead of another instance of SoundPlayer
		- if I used two instances of SoundPlayer, it would stop background music when the sound of bomb is played
		- called from Bomb when it's instance detonates

	public void PlayBonus()
		- same like PlayBomb(), it just plays sound of bonus
		- called from Map when bonus is picked