using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_FilterText : MonoBehaviour {
	public InputField inputField;
	public Action<string> onChangedInput;
	// Use this for initialization
	void Start () {
		inputField.onValueChange.AddListener(delegate { ValueChangeCheck(); });

	}
	// Invoked when the value of the text field changes.
	public void ValueChangeCheck() {
		if(onChangedInput != null )
			onChangedInput(inputField.text);
	}

}
