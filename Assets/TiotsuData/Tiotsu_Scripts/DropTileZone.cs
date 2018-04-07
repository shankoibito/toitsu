using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropTileZone : MonoBehaviour , IDropHandler , IPointerEnterHandler ,IPointerExitHandler{

	public Draggable.Slot typeofitem = Draggable.Slot.POWERANDARMY;

	public void OnPointerEnter(PointerEventData EventData){
		//Debug.Log ("OnPointerEnter");
	}

	public void OnPointerExit(PointerEventData EventData){
		//Debug.Log ("OnPointerExit");
	}

	public void OnDrop(PointerEventData EventData){
		Debug.Log (EventData.pointerDrag.name +" as Dropped on "+ gameObject.name);
		Draggable d = EventData.pointerDrag.GetComponent<Draggable> ();
		if(d!=null){
			if(typeofitem == d.typeofitem){
				d.ParentToReturnTo = this.transform;
			}
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
