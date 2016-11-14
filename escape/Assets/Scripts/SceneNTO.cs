using System;

[Serializable]
public class SceneNTO
{
	public int ID;

	public string Description;

	public SceneNTO North;

	public SceneNTO South;

    public SceneNTO East;

    public SceneNTO West;

    public SceneNTO ()
	{
      
	}  
}

