using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 * Listen to these events:
 * -EventMng.instance.OnItemPicked (OnAddItemCallback)
 * -EventMng.instance.OnItemRemoved (OnItemRemovedCallback)
 */
public class Inventory_ : MonoBehaviour {
	public const int inventorySize = 3;
	public GameObject cellItemPrefab;
	private int itemsCollected = 0;

    CellItem_[] cells = new CellItem_[inventorySize];
    GameObject currGo;
    int emptyCellIndex=-1;

    private void OnEnable()
    {
        EventMng_.ItemPicked.Subscribe(OnAddItemCallback, true);
        EventMng_.ItemRemoved.Subscribe(OnItemRemovedCallback, true);


        
        //- Subscribe to events
        //- Istantiate cellItemPrefab inventorySize times, taking this as the root parent
        //- Initialize each CellItem.ItemInThisCell to null
    }
    //TODO UnSubscribe to events
    private void OnDisable()
    {
        EventMng_.ItemPicked.Subscribe(OnAddItemCallback, false);
        EventMng_.ItemRemoved.Subscribe(OnItemRemovedCallback, false);

    }

    public int InstantiateCell()
    {
        int cellIndex = -1;

        GameObject go = Instantiate(cellItemPrefab, transform);
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] == null)
            {
                cells[i] = go.GetComponentInChildren<CellItem_>();
                cells[i].Item = null;
                return i;
            }

        }

        return cellIndex;
    }

    public void OnAddItemCallback(Item_ i, GameObject go)
    {
        if (cells[0] == null)
        {
            InstantiateCell();
            GetComponent<Animator>().SetBool("GrowInventory", true);
        }

        for (int j = 0; j < inventorySize; j++)
        {
            if (cells[j] != null)
            {


                Item_ cellItem = cells[j].Item;
                if (cellItem != null)
                {
                    if (cellItem.PrefabPath == i.PrefabPath)
                    {
                        emptyCellIndex = j;
                        break;
                    }


                }
                else
                    emptyCellIndex = j;
            }
        }

        if (emptyCellIndex == -1)
        {
            if (canGrabItem())
            {
                emptyCellIndex= InstantiateCell();
                AddItem(i, go);
            }
            else
                return;
        }
        else
            AddItem(i, go);
    }

    private void AddItem(Item_ i, GameObject go)
    {
        if (cells[emptyCellIndex].Item == null)
            itemsCollected++;


        Sprite sprite = Resources.Load<Sprite>(i.SpritePath);
        cells[emptyCellIndex].Item = i;
        i.SetOwner(cells[emptyCellIndex]);
        cells[emptyCellIndex].SetItemImage(true, sprite);

        string itemTextIndex = go.GetComponent<PickableItem_>().TextIndex.text;
        string newTextIndex = CalculateTextIndex(itemTextIndex, cells[emptyCellIndex].TextIndex.text);
        cells[emptyCellIndex].SetTextIndex(true, newTextIndex);
        go.GetComponent<Animator>().SetBool("Spawn", false);
        currGo = go;
        Invoke("Hide",0.1f);
        
        emptyCellIndex = -1;
    }

    private void Hide()
    {
        Destroy(currGo);
    }


    private string CalculateTextIndex(string itemTextIndex, string text)
    {
        int itemIndex = int.Parse(itemTextIndex);
        int currIndex = int.Parse(text);

        return (itemIndex + currIndex).ToString();

    }

    /*
* OnAddItemCallback
* - check if canGrabItem()
* - check if we already have that item in the inventory AND if not,
*      take the reference of the first empty cell
* - If we don't have that item, assign the new Item sprite icon to the
*      fgImage of the found empty cell
*      - enable the fgImage
*      - Update CellItem.ItemInThisCell with the Item we want to add
*      - itemsCollected++;
*      - Destroy 3D Game object on the stage floor
* - else return;  
* */

    public void OnItemRemovedCallback(Item_ i, bool b)
    {
        for (int j = 0; j < inventorySize; j++)
        {
            if (cells[j] != null)
            {


                Item_ cellItem = cells[j].Item;
                if (cellItem != null)
                {
                    if (cellItem.PrefabPath == i.PrefabPath)
                    {
                        if (b)
                        {
                            ResetCell(j, i);
                            if (CheckIfLastCell())
                                GetComponent<Animator>().SetBool("GrowInventory", false);
                        }
                        else
                        {
                            int newIndex = int.Parse(cells[j].TextIndex.text) - 1;
                            if (newIndex == 0)
                            {
                                ResetCell(j, i); 
                                if (CheckIfLastCell())
                                    GetComponent<Animator>().SetBool("GrowInventory", false);
                            }
                            else
                                cells[j].SetTextIndex(true, newIndex.ToString());
                        }
                    }

                }
            }
        }


    }

    public bool CheckIfLastCell()
    {
        

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] != null)
                return false;
        }

        return true;
    }

    public void ResetCell(int index, Item_ i)
    {
        cells[index].SetItemImage(false);
        cells[index].SetTextIndex(true, "0");
        cells[index].SetTextIndex(false);
        cells[index].Item = null;
        i.SetOwner(null);
        itemsCollected--;
        Destroy(cells[index].transform.parent.gameObject);
        cells[index] = null;
    }
    /*
     * OnItemRemovedCallback
     * - Find the inventory cell with the Item we want to remove
     * - Set CellItem fgImage component to disable
     * - Set CellItem.ItemInThisCell to null
     * - itemsCollected--;
     * */

    bool canGrabItem(){
		return itemsCollected < inventorySize;
	}
}
