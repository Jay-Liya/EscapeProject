using System.Collections.Generic;

//Class to hold Scene details such as players, array of connected scenes, description, id and list of scenes
public class Scene
{
	private Players _players = new Players();
	private Scene[] _connected_scenes = new Scene[4];
	private string _description = "default";
    private int _id;
    public static List<Scene> AllScenes = new List<Scene>();

    //property ID
    public int ID
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    //property Description
    public string Description{ 

		get
        { 
			return _description;
		}
		set
        { 
			_description = value;
		}
	}

    //property North   
    public Scene North { 

		get
        { 
			return _connected_scenes[(int)GameModel.DIRECTION.North];
		}
		set
        { 
			_connected_scenes[(int)GameModel.DIRECTION.North] = value;
		}
	}

    //property South
    public Scene South { 

		get
        { 
			return _connected_scenes[(int)GameModel.DIRECTION.South];
		}
		set { 
			_connected_scenes[(int)GameModel.DIRECTION.South] = value;
		}
	}

    //property East 
    public Scene East{

        get
        {
            return _connected_scenes[(int)GameModel.DIRECTION.East];
        }
        set
        {
            _connected_scenes[(int)GameModel.DIRECTION.East] = value;
        }
    }

    //property West 
    public Scene West{

        get
        {
            return _connected_scenes[(int)GameModel.DIRECTION.West];
        }
        set
        {
            _connected_scenes[(int)GameModel.DIRECTION.West] = value;
        }
    }

    public Scene()
    {
        Scene.AllScenes.Add(this);
    }

}


