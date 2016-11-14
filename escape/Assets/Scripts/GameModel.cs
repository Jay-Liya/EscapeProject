//Class to hold game data
public static class GameModel
{
	private static string _name;
	private static Player _player = new Player();
	public enum DIRECTION  {North, South, East, West};
	private static Scene _start_scene; 
	public static Players PlayersInGame = new Players();
    public static JSNDrop JSNNet;
    public static DataService theService;

    //property Start_scene
    public static Scene Start_scene{

		get 
		{ 
			return _start_scene;  
		}
		set{
			_start_scene = value; 
		}

	}

    //property Name
    public static string Name{

		get 
		{ 
			return _name;  
		}
		set{
			_name = value; 
		}

	}

    //property currentPlayer
    public static Player currentPlayer
	{
		get
		{
			return _player;
		}
		set
		{
			_player = value;
		}

	}

    public static void go(DIRECTION pDirection)
	{
		currentPlayer.Move(pDirection);
	}

    //Making all scenes
	public static void makeScenes()
	{
        Scene tmp;
        theService = new DataService();
        theService.MakeNetPersist();

        if (theService.DbExists("GameNameDb"))
        {
            theService.Connect();
            theService.LoadScenes();
            currentPlayer.InitialisePlayerState();
            currentPlayer.CurrentScene = Scene.AllScenes[0];
        }
        else
        {

            Start_scene = new Scene();
            Start_scene.ID = 1;
            Start_scene.Description = "You are lost in a forest";

            //Case 1 in story board
            tmp = new Scene();
            tmp.ID = 2;
            tmp.Description = "A Lion sleeps on the path, so cannot go further.";
            tmp.South = Start_scene;
            Start_scene.North = tmp;

            //Case 2 in story board
            tmp = new Scene();
            tmp.ID = 3;
            tmp.Description = "A foot path through two mountains.";
            tmp.West = Start_scene;
            Start_scene.East = tmp;

            //Case 3 in story board
            tmp = new Scene();
            tmp.ID = 4;
            tmp.Description = "A Cave but can’t go further.";
            tmp.North = Start_scene;
            Start_scene.South = tmp;

            //Case 4 in story board
            tmp = new Scene();
            tmp.ID = 5;
            tmp.Description = "A beautiful stream but cannot go further.";
            tmp.East = Start_scene;
            Start_scene.West = tmp;

            //Case 2.1 in story board
            tmp = new Scene();
            tmp.ID = 6;
            tmp.Description = "Some elephants near the path, so cannot go further.";
            tmp.South = Start_scene.East;
            Start_scene.East.North = tmp;

            //Case 2.2 in story board
            tmp = new Scene();
            tmp.ID = 7;
            tmp.Description = "A dangerous cliff cannot go further.";
            tmp.West = Start_scene.East;
            Start_scene.East.East = tmp;

            //Case 2.3 in story board
            tmp = new Scene();
            tmp.ID = 8;
            tmp.Description = "A path can be seen through the nice green bushes.";
            tmp.North = Start_scene.East;
            Start_scene.East.South = tmp;

            //Case 2.3.2 in story board
            tmp = new Scene();
            tmp.ID = 9;
            tmp.Description = "A beautiful waterfall but cannot go further.";
            tmp.West = Start_scene.East.South;
            Start_scene.East.South.East = tmp;

            //Case 2.3.3 in story board
            tmp = new Scene();
            tmp.ID = 10;
            tmp.Description = "A path on the slippery stones.";
            tmp.North = Start_scene.East.South;
            Start_scene.East.South.South = tmp;

            //Case 2.3.4 in story board
            tmp = new Scene();
            tmp.ID = 11;
            tmp.Description = "A steep hill, cannot climb and go further.";
            tmp.East = Start_scene.East.South;
            Start_scene.East.South.West = tmp;

            //Case 2.3.3.2 in story board
            tmp = new Scene();
            tmp.ID = 12;
            tmp.Description = "A beautiful pond with lotuses but cannot go further.";
            tmp.West = Start_scene.East.South.South;
            Start_scene.East.South.South.East = tmp;

            //Case 2.3.3.3 in story board
            tmp = new Scene();
            tmp.ID = 13;
            tmp.Description = "A huge stone, cannot climb and go further.";
            tmp.North = Start_scene.East.South.South;
            Start_scene.East.South.South.South = tmp;

            //Case 2.3.3.4 in story board - Player Won
            tmp = new Scene();
            tmp.ID = 14;
            tmp.Description = "You escaped. You are on a street towards the city and you can see the town in the far distance.";
            Start_scene.East.South.South.West = tmp;

            currentPlayer.InitialisePlayerState();
            currentPlayer.CurrentScene = Start_scene;

            theService.Connect();
            theService.SaveScenes();
        }

	}
}

