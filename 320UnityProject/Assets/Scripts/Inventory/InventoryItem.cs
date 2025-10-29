using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public interactableObject item;

    [Header("UI")]
    public Image image;

    [HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;

        // Keep track of current parent in case dropped on nothing
        parentAfterDrag = transform.parent;

        // Move to top-level canvas so it can follow mouse properly
        transform.SetParent(GetComponentInParent<Canvas>().transform, false);
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        // Place back into whatever slot OnDrop assigned
        transform.SetParent(parentAfterDrag, false);
        transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void InitialiseItem(interactableObject newItem)
    {
        item = newItem;
        if (image.sprite != null) { image.sprite = newItem.sprite; }

    }
}
