using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvent : Button
{
    // Button is Pressed
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        Debug.Log("Pressed");
    }

    // Button is released
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        Debug.Log("Released");

    }
}