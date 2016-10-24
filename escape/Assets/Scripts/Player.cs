using System;

//Class to hold player details, number, name, inventory and current scene
public class Player
{		
	private static int _player_number = 0;
	private int _number = (Player._player_number++); 
	private string _name;
	private Item[] _inventory;    
	private Scene _currentScene;
	   
    //property CurrentScene
	public Scene CurrentScene
	{ 
		get{
			return _currentScene;
		} 
		set{
			_currentScene = value;
		}
	}

    //property Name
    public String Name
	{ 
		get{
			return _name;
		} 
		set{
			_name = value;
		}
	}

    private void UpdateExperience()
    {
        Persist.control.Experience = Persist.control.Experience + 1;
    }

    private void UpdateHealth()
    {
        Persist.control.Health = Persist.control.Health - 1;
    }

    //Moving in one of the four directions
    public void Move(GameModel.DIRECTION pDirection){

		switch(pDirection){

			case GameModel.DIRECTION.North: 
					 
				if( _currentScene.North != null)
				{
					_currentScene =  _currentScene.North;
                   // _currentScene.ID
                    UpdateExperience();
                    UpdateHealth();
                }
				break;

			case GameModel.DIRECTION.South:

                if (_currentScene.South != null)
                {
                    _currentScene = _currentScene.South;
                    UpdateExperience();
                    UpdateHealth();
                }
                break;

			case GameModel.DIRECTION.East:

                if (_currentScene.East != null)
                {
                    _currentScene = _currentScene.East;
                    UpdateExperience();
                    UpdateHealth();
                }
                break;

			case GameModel.DIRECTION.West:

                if (_currentScene.West != null)
                {
                    _currentScene = _currentScene.West;
                    UpdateExperience();
                    UpdateHealth();
                }
                break;

		}//End of the Switch
	}

    public void InitialisePlayerState()
    {
        if (Persist.control != null)
        {
            Persist.control.Experience = 0;
            Persist.control.Health = 10;
        }
    }

    public Player ()
	{
        InitialisePlayerState();
    }
}


