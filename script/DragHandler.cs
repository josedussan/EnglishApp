using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler,IEndDragHandler,IBeginDragHandler
{// Start is called before the first frame update
    
    public static GameObject itemDragging;
    Vector3 startPosition;
    Transform startParent;
    Transform dragParent;
    void Start()
    {
        dragParent = GameObject.FindGameObjectWithTag("DragParent").transform;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDragging = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(dragParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemDragging = null;
        if (transform.parent == dragParent)
        {
            
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        else {
            float distance = Vector3.Distance(transform.position, dragParent.transform.position);
            if (distance < 50)
            {
                Debug.Log(distance);
                /*cero.transform.position = ceroblack.transform.position;
                aSource.PlayOneShot(audios[0]);
                textocero.transform.DOScale(new Vector3(1, 1, 1), 0.2f);*/

            }
        }
    }


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
