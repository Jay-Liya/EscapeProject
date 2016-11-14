using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Keeping data between scenes
public class Persist : MonoBehaviour {

	public static Persist control; 
	public float health = 10; 
	public float experience = 0;
    public float waterLevel = 100;
    public Text HealthText;
    public Text ExperienceText;
    public Text WaterLevelText;

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (HealthText != null)
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
            if(ExperienceText!= null)
                ExperienceText.text = "Experience = " + experience.ToString();
        }
    }
    
    public float WaterLevel
    {
        get
        {
            return waterLevel;
        }
        set
        {
            waterLevel = value;
            if (WaterLevelText != null)
                WaterLevelText.text = "Drinking water level = " + waterLevel.ToString() + "%";            
        }
    }
    
    // Initialization
    void Start () {
		
	}
    	
	void Awake(){

		if( control == null){

			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if(control!= this){

			Destroy(gameObject);
		}

	}
    	
    //Saving data
	public void Save(){

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data  = new PlayerData();
		data.health = Health;
		data.experience = Experience;
        data.waterLevel = WaterLevel;
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
            WaterLevel = data.waterLevel;
        }
	}
}

[Serializable]
//Class to hold player data, health and experience
class PlayerData
{
	public float health;
	public float experience;
    public float waterLevel;
}