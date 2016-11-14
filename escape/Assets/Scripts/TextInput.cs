using UnityEngine;
using UnityEngine.UI;

//Class for the main Scene
public class TextInput : MonoBehaviour {

	InputField input;
	InputField.SubmitEvent se;
	public Text output;

	public void TextUpdate(string aStr){

		output.text = aStr;
	}
    
	//initialization
	void Start () {

		input = this.GetComponent<InputField>();
		se = new InputField.SubmitEvent();
		se.AddListener(SubmitInput);		
		input.onEndEdit = se;
		output.text = GameModel.currentPlayer.CurrentScene.Description;
	
	}
	
    //Submitting Input
	private void SubmitInput(string arg0)
	{
		//string currentText = output.text;
		CommandProcessor aCmd = new CommandProcessor();        
		aCmd.Parse(arg0,TextUpdate);
		input.text = "";
		input.ActivateInputField();

	}

    //When input changes
	private void ChangeInput( string arg0)
	{
		//Debug.Log(arg0);
	}
}
