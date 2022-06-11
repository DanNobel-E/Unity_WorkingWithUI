using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ToggleTest : MonoBehaviour {
	public ToggleGroup toggleG;
    public bool UseLinq;
    public bool SelectToggle;
    public int ToggleToSelect;

    void Start () {
		
		Debug.Log ("Selected toggle name: " + getToggleSelected().name);		
	}
	
	void Update () {
        if (SelectToggle)
        {
            SelectToggle = false;
            selectToggle(ToggleToSelect);
        }
	}

	public Toggle getToggleSelected(){
		//We use Linq here, to access the Active toggle
        if(UseLinq)
            return toggleG.ActiveToggles().FirstOrDefault();
        else
        {
            IEnumerable<Toggle> toggleEnum = toggleG.ActiveToggles();
            foreach(Toggle t in toggleEnum)
            {
                if (t.isOn)
                    return t;
            }
            return null;
        }
    }

    public void selectToggle(int id)
    {
        Toggle[] toggles = toggleG.GetComponentsInChildren<Toggle>();
        toggles[id].isOn = true;
    }
    public void OnToggleValueChanged(bool newVal)
    {
        if (newVal)
            Debug.Log("You just select the toggle: " + getToggleSelected().name);
    }
}
