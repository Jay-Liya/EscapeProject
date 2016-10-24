using UnityEngine;

//Class to make sure instantiated only once
public class RunOnlyOnce : MonoBehaviour {

	public static RunOnlyOnce instance;

    //Destoying the other instance
	void Awake() {

		if(instance != null && instance != this) {
			DestroyImmediate(gameObject);
			return;
		}

		instance = this;
		GameModel.makeScenes();
	}
}
