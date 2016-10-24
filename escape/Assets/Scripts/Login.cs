using UnityEngine;
using UnityEngine.UI;

//Class for login
public class Login : MonoBehaviour {

    public Text Name;
    public Text Password;
    public Canvas nextCanvas;

	// Use this for initialization
	public void login()
    {
      //  GameObject aName = GameObject.Find("UserTxt");
       // InputField aText = aName.GetComponent<InputField>() ;
        string name = Name.text;//  aText.text;

       // GameObject aPassword = GameObject.Find("PwdTxt");
        //aText = aPassword.GetComponent<InputField>();
        string password = Password.text;// aText.text;

        DataService aDS = new DataService();

        //aDS.makeNewUser("Jay", "123"); //Creating the user with name "Jay" and password "123"

        if (aDS.CheckLogin(name, password) )
        {
            ShowCanvas(nextCanvas);
        }
        
    }

    public void ShowCanvas(Canvas pCanvas)
    {

        pCanvas.gameObject.SetActive(true);
        Debug.Log(gameObject.name);
        Canvas[] canvases = gameObject.GetComponentsInChildren<Canvas>();

        foreach (Canvas cnv in canvases)
        {

            if (cnv.name != pCanvas.name)
            {

                cnv.gameObject.SetActive(false);
            }
        }

    }


}