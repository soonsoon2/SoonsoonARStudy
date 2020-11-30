using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AR공간에 선을 그어보는 예제.
//Line Renderer 컴퍼넌트를 활용함.
public class DrawLine : MonoBehaviour
{
    public GameObject _lineRenderePrefabs; //추가로 생성해줄 라인 오브젝트
    public LineRenderer _lineRendere; // 현재 추가된 라인 오브젝트의 라인 렌더러
    public Transform _linePool; //라인 오브젝트들이 추가되는 라인 풀
    public List<LineRenderer> _lineList = new List<LineRenderer>(); // 추가된 라인오브젝트들을 저장. 향후 지우기 기능 추가 가능
    public Transform _pivotPoint; //라인이 그려질 중심 포인트로 활용

    public bool _startLine; //라인이 그려지는지 체크
    public bool _use; //사용중인지 체크

    // Update is called once per frame
    void Update()
    {
        if(_use)
        {
            if(_startLine)
            {
                DrawLineContinue();
            }
        }
    }

    //라인 긋기 시작
    public void StartDrawLine()
    {
        _use = true;
        if(!_startLine)
        {
            MakeLineRendere();
        }
    }
    //라인 긋기 멈춤
    public void StopDrawLine()
    {
        Debug.Log("버튼 눌리기 끝");
        _use = false;
        _startLine = false;
        _lineRendere = null;
    }

    //라인 렌더러 오브젝트를 생성해줘서 라인을 그려준다.

    public void MakeLineRendere()
    {
        GameObject tLine = Instantiate(_lineRenderePrefabs);
        tLine.transform.SetParent(_linePool);
        tLine.transform.position=Vector3.zero;
        tLine.transform.localScale = new Vector3(1,1,1);

        _lineRendere = tLine.GetComponent<LineRenderer>();
        _lineRendere.positionCount = 1;
        _lineRendere.SetPosition(0,_pivotPoint.position);

        _startLine = true;

        _lineList.Add(_lineRendere);
    }

    //버튼이 눌리는동안 계속 호출 되는 메소드, 선이 그어지는 포인트가 계속 추가됨.
    public void DrawLineContinue()
    {
        _lineRendere.positionCount = _lineRendere.positionCount+1;
        _lineRendere.SetPosition(_lineRendere.positionCount-1,_pivotPoint.position);
    }
}
