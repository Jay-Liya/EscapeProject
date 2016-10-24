using UnityEngine;
using UnityEngine.UI;

//Class for register a new user
public class Register : MonoBehaviour {

    public Text Name;
    public Text Password;

	// Use this for initialization
	public Register()
    {
        GameObject aName = GameObject.Find("UserTxt");
        InputField aText = aName.GetComponent<InputField>() ;
        string name = Name.text;//  aText.text;

        GameObject aPassword = GameObject.Find("PwdTxt");
        aText = aPassword.GetComponent<InputField>();
        string password = Password.text;// aText.text;

        DataService aDS = new DataService();
        /*if (aDS.AddLogin(name, password) )
        {
            
        }
        */
    }
}