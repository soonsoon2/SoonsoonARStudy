using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject _lineRenderePrefabs;
    public LineRenderer _lineRendere;
    public Transform _linePool;
    public List<LineRenderer> _lineList = new List<LineRenderer>();
    public Transform _pivotPoint;

    public bool _startLine;
    public bool _use;
    public float _timer;
    public float _timerForLim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_use)
        {
            if(_startLine)
            {
                _timer += Time.deltaTime;
                if(_timer > _timerForLim)
                {
                    _timer = 0;
                    DrawLineContinue();
                }
            }
        }
    }

    //라인 긋기 시작
    public void StartDrawLine()
    {
        Debug.Log("버튼 눌리는중");
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

    public void DrawLineContinue()
    {
        _lineRendere.positionCount = _lineRendere.positionCount+1;
        _lineRendere.SetPosition(_lineRendere.positionCount-1,_pivotPoint.position);
    }
}
