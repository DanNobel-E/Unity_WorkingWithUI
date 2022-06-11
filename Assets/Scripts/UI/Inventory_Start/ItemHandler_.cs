using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Listen to this event:
 * -EventMng.instance.OnItemRemoved (OnItemRemovedCallback)
 * Triggers this event:
 * -EventMng.instance.OnItemPicked
 */
public class ItemHandler_ : MonoBehaviour {
    public Transform itemsOnStageRootT; //parent of items on stage. Add here 3D items removed from Inventory

    //TODO Subscribe to events
    private void OnEnable()
    {
        EventMng_.ItemRemoved.Subscribe(OnItemRemovedCallback, true);

    }
    //TODO UnSubscribe to events
    private void OnDisable()
    {
        EventMng_.ItemRemoved.Subscribe(OnItemRemovedCallback, false);
    }

    //OnItemRemovedCallback
    //  - Istantiate the Item we want to remove with position near the player and itemsOnStageRootT as parent
    public void OnItemRemovedCallback(Item_ i, bool b)
    {
        if (b)
        {
            if (i.Owner != null)
            {
                int numObj = int.Parse(i.Owner.TextIndex.text);

                for (int j = 0; j < numObj; j++)
                {
                    OnItemRemoved(i);
                }

            }
        }
        else
            OnItemRemoved(i);
    }

    private void OnItemRemoved(Item_ i)
    {
        GameObject gO = Resources.Load<GameObject>(i.PrefabPath);

        Vector3 randomRot = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        Quaternion rot = Quaternion.Euler(randomRot);
        Vector2 randomPos = Random.insideUnitCircle * 1;
        Vector2 randomOffset = new Vector2(Random.Range(1f, 1.5f), Random.Range(1f, 1.5f));
        Vector2 randomSign = new Vector2(Random.Range(0,2)==0?-1:1, Random.Range(0, 2)==0?-1:1);
        Vector3 pos = new Vector3(transform.position.x + randomPos.x+(randomOffset.x*randomSign.x), 1, transform.position.z + randomPos.y+(randomOffset.y*randomSign.y));
        
        GameObject newGo=Instantiate(gO, pos, rot, itemsOnStageRootT);

        newGo.GetComponent<PickableItem_>().TextIndex.text = "1";
        newGo.GetComponent<Animator>().SetBool("Spawn", true);

    }

    private void OnCollisionEnter(Collision collision)
    {
        PickableItem_ pI = collision.gameObject.GetComponent<PickableItem_>();

        if(pI!=null)
            EventMng_.OnItemPicked(pI.Item, pI.gameObject);
    }

    //React to collision enter
    //  - Take PickableItem component from the collider gameObject 
    //  - Trigger EventMng.instance.OnItemPicked (what parameters do you have to pass?)
}
