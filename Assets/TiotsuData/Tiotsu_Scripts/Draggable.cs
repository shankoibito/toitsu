using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler {

	public enum Slot {POWERANDARMY};
	public Slot typeofitem = Slot.POWERANDARMY;
	GameObject placeholder =null;

	public Transform ParentToReturnTo = null;

	public void OnBeginDrag(PointerEventData EventData){
		//Debug.Log ("OnBeginDrag");

		placeholder = new GameObject();
		placeholder.transform.SetParent (this.transform.parent);
		LayoutElement le = placeholder.AddComponent<LayoutElement> ();
		le.preferredWidth = this.GetComponent<LayoutElement> ().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement> ().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex (this.transform.GetSiblingIndex());

		ParentToReturnTo = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent);
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData EventData){
		//Debug.Log ("OnDrag");
		this.transform.position = EventData.position;

		int newSiblingIndex = ParentToReturnTo.childCount;

		for(int i=0;i<ParentToReturnTo.childCount;i++){
			if(this.transform.position.x< ParentToReturnTo.GetChild(i).position.x){
				newSiblingIndex = i;
				if(placeholder.transform.GetSiblingIndex()<newSiblingIndex){
					newSiblingIndex--;
				}
				break;
			}
		}
		placeholder.transform.SetSiblingIndex (newSiblingIndex);
	}

	public void OnEndDrag(PointerEventData EventData){
		//Debug.Log ("OnEndDrag");
		this.transform.SetParent (ParentToReturnTo);
		this.transform.SetSiblingIndex (placeholder.transform.GetSiblingIndex());
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		Destroy (placeholder);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
