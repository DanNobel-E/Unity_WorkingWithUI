using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownListTest: MonoBehaviour
{
	//Create a List of new Dropdown options
	List<string> dropOptions = new List<string> { "Option 1", "Option 2"};
	//This is the Dropdown
	public Dropdown dropdown;

	void Start()
	{
		//Clear the old options of the Dropdown menu
		dropdown.ClearOptions();
		//Add the options created in the List above
		dropdown.AddOptions(dropOptions);
	}

    public void OnDropDownValueChanged(int newVal)
    {
        Debug.Log("DropDown new Value: "+newVal);
    }
}
