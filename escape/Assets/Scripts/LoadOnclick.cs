using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Class for click event
public class LoadOnclick : MonoBehaviour {

    public Canvas nextCanvas;

    //Loading Scene
    public void LoadScene(string pSceneName){

		SceneManager.LoadScene(pSceneName);
	}

    //Showing Canvas
	public void ShowCanvas(Canvas pCanvas){
		
		pCanvas.gameObject.SetActive(true);
		Debug.Log(gameObject.name);
		Canvas[] canvases = gameObject.GetComponentsInChildren<Canvas>();

		foreach(Canvas cnv in canvases){

		 	if(cnv.name != pCanvas.name){

		 		cnv.gameObject.SetActive(false);
			}
		}
		 
	}

    public void Login()
    {
        GameObject goLogin = GameObject.FindGameObjectWithTag("Login");
        //InputField aText = aName.GetComponent<InputField>() ;
        string name = goLogin.GetComponent<InputField>().text;//  aText.text;

        GameObject goPassword = GameObject.FindGameObjectWithTag("Password");
        //aText = aPassword.GetComponent<InputField>();
        string password = goPassword.GetComponent<InputField>().text;// aText.text;

        DataService aDS = new DataService();

        //aDS.MakeAnTestAccount("sunil", "real123");
        //aDS.MakeAnTestAccount("todd", "sdv602");

        if (aDS.CheckLogin(name, password))
        {
            // LOGIN OK
            ShowCanvas(nextCanvas);
        }
    }


}
