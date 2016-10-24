using System.Collections.Generic;

// Class to hold details of set of players
public class Players
{
    //Initializing list of players
	private List<Player> _players = new List<Player>();

	public Player this[int index] 
	{
		get
		{ 
			return  _players[index];
		} 
		set
		{
			_players[index] = value;
		}
	}

	public Players ()
	{
		
	}
}


