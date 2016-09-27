using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Used to drag object from the start position into real-world space to be placed for defense
/// </summary>
[RequireComponent(typeof(Image))] //This script requires an image to work properly
public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    /// <summary>
    /// Wether or not the dragging in on a surfce or not
    /// </summary>
    public bool dragOnSurface = true;

    /// <summary>
    /// A collection of the dragging icons instanced at specific key locations
    /// </summary>
    private Dictionary<int, GameObject> draggingIcons = new Dictionary<int, GameObject>();

    /// <summary>
    /// A collection of the planes that will be dragged along
    /// </summary>
    private Dictionary<int, RectTransform> draggingPlanes = new Dictionary<int, RectTransform>();

    /// <summary>
    /// The Object to be placed
    /// </summary>
    public GameObject dropObject;

    /// <summary>
    /// the plane of existence the dragging will occur on
    /// </summary>
    public GameObject dragPlane;

    /// <summary>
    /// Called when the objects starts being dragged
    /// </summary>
    /// <param name="eData">the event data to be used</param>
    public void OnBeginDrag(PointerEventData eData)
    {

        //creating a copy of the object to drop onto the grid
        draggingIcons[eData.pointerId] = new GameObject(dropObject.ToString());

        draggingIcons[eData.pointerId].transform.SetParent(dragPlane.transform, false);
        draggingIcons[eData.pointerId].transform.SetAsLastSibling();

        if (dragOnSurface)
            draggingPlanes[eData.pointerId] = transform as RectTransform;
        else
            draggingPlanes[eData.pointerId] = dragPlane.transform as RectTransform;

    }

    /// <summary>
    /// CHanges the position of the object when the cursor ismoved
    /// </summary>
    /// <param name="eData">the event data</param>
    public void OnEndDrag(PointerEventData eData)
    {
        if (draggingIcons[eData.pointerId] != null)
            Destroy(draggingIcons[eData.pointerId]);

        draggingIcons[eData.pointerId] = null;
    }

    /// <summary>
    /// CHanges the position of the dragged Object
    /// </summary>
    /// <param name="eData">event</param>
    public void OnDrag(PointerEventData eData)
    {
        if (draggingIcons[eData.pointerId] != null)
            SetDraggedPosition(eData);
    }

    /// <summary>
    /// Called when dragging stops
    /// </summary>
    /// <param name="eventData">The event Data Signal</param>
    private void SetDraggedPosition(PointerEventData eData)
    {
        if (dragOnSurface && eData.pointerEnter != null && eData.pointerEnter.transform as RectTransform != null)
            draggingPlanes[eData.pointerId] = eData.pointerEnter.transform as RectTransform;

        var rt = draggingIcons[eData.pointerId].GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlanes[eData.pointerId], eData.position, eData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = draggingPlanes[eData.pointerId].rotation;
        }
    }


    /// <summary>
    /// A method that finds a specific object type in the parrents of the inputted object
    /// and returns the first instance of that object
    /// </summary>
    /// <typeparam name="T">The type to be found</typeparam>
    /// <param name="go">the object to search upward from</param>
    /// <returns>returns the first instance of T</returns>
    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        var t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }
}
