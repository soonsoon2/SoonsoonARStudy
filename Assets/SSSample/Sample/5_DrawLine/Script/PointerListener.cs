using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//OnPress 를 구현하기 위한 코드.
//버튼에 추가되어서 사용되어진다.
public class PointerListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public DrawLine _drawLineST; //DrawLine 스크립트를 참조하기 위함.
    bool _pressed = false;  //버튼이 눌렸는지 확인하기 위함.
    public void OnPointerDown(PointerEventData eventData) //해당 버튼이 눌렸을때.
    {
        _pressed = true; 
    }
 
    public void OnPointerUp(PointerEventData eventData) //해당 버튼이 띄어졌을때.
    {
        _pressed = false;
        _drawLineST.StopDrawLine();
    }
 
    void Update()
    {
        if (_pressed)
        {
            //버튼이 눌려져 있는 동안 
            //참조된 DrawLine 스크립트의
            //StartDrawLine()을 호출해와 선을 긋는다.
            _drawLineST.StartDrawLine();
        }
    }
}