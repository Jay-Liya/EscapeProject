using UnityEngine;

//Supporting class for DataService class with three functions deleting, saving and loading
public class DataServiceUtilities : MonoBehaviour {

    //Deleting database
	public void DeleteDB(){

		DataService _connection = new DataService();

		if(_connection.DbExists("GameNameDb")){
			_connection.deleteDatabaseFile();
		}
	}

    //Saving scenes to database
	public void Save(){

		DataService _connection = new DataService();

		if(_connection.DbExists("GameNameDb")){
			_connection.Connect();
			_connection.SaveScenes();
		}
	}

    //Loading scenes from database
	public void Load(){

		DataService _connection = new DataService();

		if(_connection.DbExists("GameNameDb")){
			_connection.Connect();
			_connection.LoadScenes();
		}
	}
}
