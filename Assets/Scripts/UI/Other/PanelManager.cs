using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour {

	public Animator initiallyOpen;

	private int m_OpenParameterId;
	private Animator m_Open;                //Current Open Menu
	private GameObject m_PreviouslySelected;

	public void OnEnable()
	{
		m_OpenParameterId = Animator.StringToHash ("Open");

		if (initiallyOpen == null)
			return;

		OpenPanel(initiallyOpen);
	}

	public void OpenPanel (Animator anim)
	{
        //We are trying to open the an already open panel
		if (m_Open == anim)
			return;

        //anim is the animator of the object we want to Open
        //This anim should have a 'Open' bool public parameter
        anim.gameObject.SetActive(true);
        //Save the current selected Menu (which we are going to close)
		m_PreviouslySelected = EventSystem.current.currentSelectedGameObject;
        //Send close event to the Animator of the current Menu and set the previous Menu Selected entry as selected
		CloseCurrent();
        //Save current Menu Animator ...
		m_Open = anim;
        //...and send Open trigger to it
		m_Open.SetBool(m_OpenParameterId, true);

		GameObject go = FindFirstEnabledSelectable(anim.gameObject);

		SetSelected(go);
	}

	static GameObject FindFirstEnabledSelectable (GameObject gameObject)
	{
		GameObject go = null;
        //Search for the first selectable UI element 
		var selectables = gameObject.GetComponentsInChildren<Selectable> (true);
		foreach (var selectable in selectables) {
			if (selectable.IsActive () && selectable.IsInteractable ()) {
				go = selectable.gameObject;
				break;
			}
		}
		return go;
	}

	public void CloseCurrent()
	{
		if (m_Open == null)
			return;

		m_Open.SetBool(m_OpenParameterId, false);
		SetSelected(m_PreviouslySelected);
        m_Open = null;
	}

	private void SetSelected(GameObject go)
	{
		EventSystem.current.SetSelectedGameObject(go);
	}
}
