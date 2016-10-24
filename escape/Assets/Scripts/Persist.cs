using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

//Keeping data between scenes
public class Persist : MonoBehaviour {

	public static Persist control; 
	public float health=10; 
	public float experience=0;
    public Text HealthText;
    public Text ExperienceText;

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            HealthText.text = "Health = " + health.ToString();
        }

    }

    public float Experience
    {
        get
        {
            return experience;
        }
        set
        {
            experience = value;
            ExperienceText.text = "Experience = " + experience.ToString();
        }

    }

    // Use this for initialization
    void Start () {
		// PLAYER PREFS
		// PlayerPrefs.SetInt("health",21);
		//int health = PlayerPrefs.GetInt("health");

		// DontDestroyOnLoad
		//DontDestroyOnLoad(gameObject);

	}

	// Now there can be only one of
	void Awake(){
		if( control == null){
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if(control!= this){
			Destroy(gameObject);
		}

	}

	// Serialisation

	// Unity Serialisation

	// Update is called once per frame
	//void Update () {
	
	//}

    //Saving data
	public void Save(){

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data  = new PlayerData();
		data.health = Health;
		data.experience = Experience;
		bf.Serialize(file,data);
		file.Close();
	}

    //loading data
	public void Load(){

		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat",FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			Health = data.health;
			Experience = data.experience;
		}
	}
}

[Serializable]
//Class to hold player data, health and experience
class PlayerData
{
	public float health;
	public float experience;
}