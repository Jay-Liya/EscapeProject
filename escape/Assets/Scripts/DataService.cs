﻿using SQLite4Unity3d;
using System.IO;

#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

//For saving and retrieving from database
public class DataService{

	private SQLiteConnection _connection;
	private string currentDbPath = "";
	private bool dbExists;

	public bool	DbExists(string DatabaseName){

		// Watch out! this method has a side effect
		bool result = false;

		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		result = File.Exists(dbPath);
		#else
		// check if file exists in Application.persistentDataPath
		var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

		if (!File.Exists(filepath))
		{
		result = false;
		Debug.Log("Database not in Persistent path");
		// if it doesn't ->
		// open StreamingAssets directory and load the db ->

		#if UNITY_ANDROID 
		var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
		while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
		// then save to Application.persistentDataPath
		File.WriteAllBytes(filepath, loadDb.bytes);
		#elif UNITY_IOS
		var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#elif UNITY_WP8
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);

		#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#else
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);

		#endif

		Debug.Log("Database written");
		}

		var dbPath = filepath;
		#endif

		currentDbPath = dbPath;
		
		return result;
	}

	public void Connect(){

		_connection = new SQLiteConnection(currentDbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);  

	}

	public DataService(){

        DbExists("users.db");
        Connect();
           
	}

    // Set up utilities
	public void deleteDatabaseFile(){

		File.Delete(currentDbPath);
	}

    // Scene Save

	private void CreateIfNotExists<T>( ) where T:new () {
		// Since we are taking the perspective that Set puts the data into the
		// database, if the table does not exist then we create.
		_connection.CreateTable<T>();

	}

	private void IfNotExistsCreateSceneToScene( ){
	  // Since we are taking the perspective that Set puts the data into the
	  // database, if the table does not exist then we create.
	  _connection.CreateTable<SceneToSceneDTO>();

	}

	private void IfNotExistsCreateScene(){

		// Since we are taking the perspective that Set puts the data into the
		// database, if the table does not exist then we create.
		_connection.CreateTable<SceneDTO>();
	
	}

	private bool SceneToFromExists( int pFromID, int pToId)
	{
	   var y = _connection.Table<SceneToSceneDTO>().Where(
	              x => x.FromSceneID == pFromID && x.ToSceneID == pToId).FirstOrDefault();
		return y != null;

	}

	private bool SceneExists(int pSceneID){
		var y = _connection.Table<SceneDTO>().Where(
				x => x.SceneID == pSceneID).FirstOrDefault();

		return y != null;

	}

	private void SetScene(SceneDTO aSceneDTO){

		CreateIfNotExists<SceneDTO>();

		if(SceneExists(aSceneDTO.SceneID)){
			_connection.Update(aSceneDTO);
		}
		else{
			_connection.Insert(aSceneDTO);
		}
	}

	private void SetSceneToFrom(Scene pScene, Scene pDirection, string pLabel){

		if(pDirection != null ){

			// IfNotExistsCreateSceneToScene();
			CreateIfNotExists<SceneToSceneDTO>();
			SceneToSceneDTO aDTO = new SceneToSceneDTO{
				FromSceneID = pScene.ID, 
				ToSceneID = pDirection.ID,
				Label = pLabel
				};

			if(SceneToFromExists(aDTO.FromSceneID,aDTO.ToSceneID)){
				_connection.Update(aDTO);
			}
			else{
				_connection.Insert(aDTO);
			}
		}
	}// SetSceneToFrom

    //Saving Scenes
	public void SaveScenes( ){

		foreach( Scene aScene in Scene.AllScenes)
		{
				SceneDTO currentSceneDTO = new SceneDTO{

					SceneID = aScene.ID,
					GameID = 1, 
					Name = "Escape Jungle",
					Story =  aScene.Description
				};

				SetSceneToFrom(aScene, aScene.North, "North");
				SetSceneToFrom(aScene, aScene.South,"South");
				SetSceneToFrom(aScene, aScene.East, "East");
				SetSceneToFrom(aScene, aScene.West, "West");
				
				SetScene(currentSceneDTO);
				
		}

	}

    public void MakeNetPersist()
    {
        List<string> tableNames = new List<string>();
        tableNames.Add("tblPlayer");

        if(GameModel.JSNNet!=null)
            GameModel.JSNNet.MakeTables("ZORG321", tableNames, "53211");
    }

    // Scene Load
    public void LoadScenes( ){

		// Clear the current Scenes
		if(Scene.AllScenes.Count > 0){

			Scene.AllScenes.Clear();
		}

		// Get the Scenes
		var SceneDTOs = _connection.Table<SceneDTO>();

		// Rebuild the local data structure
		foreach(SceneDTO aDTO in SceneDTOs){

		    // Check we have not created this already!!
			Scene firstCheckScene = Scene.AllScenes.Find(x => x.ID == aDTO.SceneID);
			Scene aScene;

			if( firstCheckScene == null)
				aScene = new Scene(){ ID = aDTO.SceneID, Description = aDTO.Story};
			else
				aScene = firstCheckScene;

            string testJSN = aDTO.ToString();

            // Get North , South, East and West
            var directions = _connection.Table<SceneToSceneDTO>().Where(
						x => x.FromSceneID == aDTO.SceneID);
		    
			foreach( SceneToSceneDTO aDirScene in directions){

				var aSceneDTO = (_connection.Table<SceneDTO>().Where(
						x => x.SceneID == aDirScene.ToSceneID)).FirstOrDefault();
		        
				Scene aCheck = Scene.AllScenes.Find(x => x.ID == aSceneDTO.SceneID);
				Scene toScene;

				if( aCheck == null){
		          	toScene = new Scene(){ID = aSceneDTO.SceneID, 
										Description = aSceneDTO.Story};
				}
				else{
					toScene = aCheck;
				}

				switch(aDirScene.Label){

					case("North"): 
						aScene.North = toScene;
					    break;
					case ("South"):
						aScene.South = toScene;
					    break;
                    case ("East"):
                        aScene.East = toScene;
                        break;
                    case ("West"):
                        aScene.West = toScene;
                        break;
                }

			}//for each Direction		    

		    // Make the current Scene - this adds it to the AllScenes

		}// for eacn SceneDTO

	}     

	public IEnumerable<Person> GetPersons(){

		return _connection.Table<Person>();
	}

	public IEnumerable<Person> GetPersonsNamedRoberto(){

		return _connection.Table<Person>().Where(x => x.Name == "Roberto");
	}

	public Person GetJohnny(){

		return _connection.Table<Person>().Where(x => x.Name == "Johnny").FirstOrDefault();
	}

    //Creating the person with name "Johnny", surname "Mnemonic" and age 21
	public Person CreatePerson(){

		var p = new Person{
				Name = "Johnny",
				Surname = "Mnemonic",
				Age = 21
		};

		_connection.Insert (p);
		return p;
	}
    
    //Checking credentials
    public bool CheckLogin(string pName, string pPassword)
    {
        int count = 0;
        
            
         count = _connection.Table<Person>().Where(x => x.Name == pName
                                                          && x.Password == pPassword).Count();

          //  count = _connection.Table<Person>().Where(x => x.Name == pName).Count();
          
        return count > 0;
    }

    //Making new user
    public void makeNewUser(string pName, string pPassword)
    {
        CreateIfNotExists<Person>();
        Person anAccountDTO = new Person { Name = pName, Password = pPassword };
        _connection.Insert(anAccountDTO);
    }
        
}
