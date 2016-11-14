using System;

//Class as Data tranfer object for Player data
[Serializable]
public class PlayerDTO
{
		public int PlayerID;
		public int LocationID;
		public string PlayerName;
		public string Password;
		public float Experience;
        public float Health;

        public PlayerDTO ()
		{
		}
}


