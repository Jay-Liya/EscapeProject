using System;

public delegate void aDisplayer( String value );

//Implement the command pattern in an extensible way
public class CommandProcessor
{
	public CommandProcessor ()
	{
	}
		
	public void Parse(String pCmdStr, aDisplayer display){ 
         
		String strResult = "Do not understand"; 
		char[] charSeparators = new char[] {' '};
		pCmdStr = pCmdStr.ToLower();

		String[] parts = pCmdStr.Split(charSeparators,StringSplitOptions.RemoveEmptyEntries); // tokenise the command


        if (parts.Length > 0) //checking whether the array "parts" consists of elements or not
        {
            // process the tokens      
            switch (parts[0])
            {
                case "pick":
                    if (parts[1] == "up")
                    {
                        strResult = "Got Pick up";

                        if (parts.Length == 3)
                        {
                            String param = parts[2];
                        }// do pick up command
                            // GameModel.Pickup();
                    }
                    break;

                case "go":
                    switch (parts[1])
                    {
                        case "north":
                            GameModel.go(GameModel.DIRECTION.North);
                            break;

                        case "south":
                            GameModel.go(GameModel.DIRECTION.South);
                            break;

                        case "west":
                            GameModel.go(GameModel.DIRECTION.West);
                            break;

                        case "east":
                            GameModel.go(GameModel.DIRECTION.East);
                            break;

                        default:
                            strResult = "Do not know how to go there";
                            break;
                    }// end "go" switch

                    strResult = GameModel.currentPlayer.CurrentScene.Description;
                    display(strResult);
                    break;

                default:
                    strResult = "Do not understand";
                    break;

            }// end "parts[0]" switch
        }// return strResult;
	}// Parse
}


