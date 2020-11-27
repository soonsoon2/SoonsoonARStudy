using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointerListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public DrawLine _drawLineST;
    bool _pressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        
    }
 
    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
        _drawLineST.StopDrawLine();
    }
 
    void Update()
    {
        if (_pressed)
        {
            _drawLineST.StartDrawLine();
        }
        // DO SOMETHING HERE
    }
}