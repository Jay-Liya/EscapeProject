using System;
using System.Collections.Generic;
using UnityEngine;

//Class to hold player details, number, name, inventory and current scene
public class Player
{		
	private static int _player_number = 0;
	private int _number = (Player._player_number++); 
	private string _name;
	private Item[] _inventory;    
	private Scene _currentScene;

    //property Inventory
    public Item[] Inventory
    {
        get
        {
            return _inventory;
        }
        set
        {
            _inventory = value;
        }
    }

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

    //updating experience
    private void UpdateExperience()
    {
        Persist.control.Experience = Persist.control.Experience + 1;
    }

    //updating health
    private void UpdateHealth()
    {
        Persist.control.Health = Persist.control.Health - 1;
    }
    
    //updating water level
    private void UpdateWaterLevel()
    {
        Persist.control.WaterLevel = Persist.control.WaterLevel - 10;
    }

    //updating the Inventory
    public void InventoryUpdate()
    {
        UpdateWaterLevel();
    }

    //Making playerID
    private string MakePlayerID()
    {
        return "\"PlayerID\":" + _number.ToString();
    }

    private List<PlayerDTO> GetPlayerDTO(List<PlayerDTO> aDTOList)
    {
        foreach (PlayerDTO aPlayer in aDTOList)
        {
            Debug.Log("GOT BACK" + aPlayer.PlayerID.ToString() + "<<<");

        }

        return aDTOList;
    }

    private List<PlayerDTO> NextCommand(List<PlayerDTO> aDTO)
    {
        GameModel.JSNNet.jsnGet<PlayerDTO>("tblPlayer", "", GetPlayerDTO);
        return aDTO;
    }

    private void MoveInDirection(Scene pToScene)
    {
        if (pToScene != null)
        {
            _currentScene = pToScene;

            PlayerDTO aDTO = new PlayerDTO
            {
                PlayerID = _number,
                LocationID = CurrentScene.ID,
                PlayerName = "test",
                Password = "12345",
                Experience = Persist.control.Experience,
                Health = Persist.control.Health
            };

            UpdateExperience();
            UpdateHealth();

            string anID = MakePlayerID();
            if(GameModel.JSNNet!=null)
                GameModel.JSNNet.jsnPut<PlayerDTO>("tblPlayer", anID, aDTO, NextCommand);
        }
    }

    //Moving in one of the four directions
    public void Move(GameModel.DIRECTION pDirection){

		switch(pDirection){

            case GameModel.DIRECTION.North: 
                MoveInDirection(_currentScene.North);
                break;
            case GameModel.DIRECTION.South:
                MoveInDirection(_currentScene.South);
                break;
            case GameModel.DIRECTION.East:
                MoveInDirection(_currentScene.East);
                break;
            case GameModel.DIRECTION.West:
                MoveInDirection(_currentScene.West);
                break;

        }//End of the Switch
	}

    public void InitialisePlayerState()
    {
        if (Persist.control != null)
        {
            Persist.control.Experience = 0;
            Persist.control.Health = 10;
            Persist.control.WaterLevel = 100;
        }
    }

    public Player ()
	{
        InitialisePlayerState();
    }
}


