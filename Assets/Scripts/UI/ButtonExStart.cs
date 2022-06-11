using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonExStart : MonoBehaviour {
	public Color HighlightCol;
	public Color NormalCol;
	public Color PressedCol;
	public Color DisabledCol;
	public bool active;
	public Image img;           //Image of the fake button that will behave like the real button

	bool selected;
	bool activation;

    private void Start()
    {
		if (active)
			activation = true;
	}

    void Update()
	{

		ActiveControl();


		if (active && selected)
			SelectionControl();

	}

	void SelectionControl()
    {
		if(EventSystem.current.currentSelectedGameObject != img.gameObject 
			|| EventSystem.current.currentSelectedGameObject == null)
        {
			img.color = NormalCol;
			selected = false;
		}
	}
	
	void ActiveControl()
    {
		if (activation)
		{
			if (active)
			{
				img.color = NormalCol;
				activation = false;
			}

		}
		else if (!active)
		{
			img.color = DisabledCol;
			activation = true;
		}
	}

	public void OnPointerDown()
	{

		if (!active)
			return;

		img.color = PressedCol;
		selected = true;
		EventSystem.current.SetSelectedGameObject(img.gameObject);
	}
	public void OnPointerEnter()
	{
		if (!active)
			return;

		if (!selected)
			img.color = HighlightCol;
	}

	public void OnPointerExit()
	{
		if (!active)
			return;

		if (!selected)
			img.color = NormalCol;
	}

	public void OnPointerUp()
	{
		if (!active)
			return;

		img.color = HighlightCol;
	}
}
