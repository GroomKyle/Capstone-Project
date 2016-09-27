using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Checks for Keyboard/Controller inputs for menu navigation
/// </summary>
public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSys;
    public GameObject selectedObj;

    /// <summary>
    /// Used for selecting buttons
    /// </summary>
    private bool buttonSel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetAxisRaw("Vertical")!=0 && buttonSel == false)
        {
            eventSys.SetSelectedGameObject(selectedObj);
            buttonSel = true;
        }
	}

    private void OnDisable()
    {
        buttonSel = false;
    }
}
