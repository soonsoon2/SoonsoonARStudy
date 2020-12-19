using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//측정값을 보여주기위해서 생성되는 오브젝트
public class RulerObj : MonoBehaviour
{
    //총 2개의 볼 오브젝트를 사용하며 리스트로 관리함.
    public List<GameObject> _dotList = new List<GameObject>();
    public LineRenderer _lineObj; //2개의 볼 오브젝트간의 선을 나타내기 위함.
    public Transform _valueTextBG;
    public TMPro.TextMeshPro _valueText;
    public Transform _mainCam;
    public bool use;
    
    // Start is called before the first frame update
    void Start()
    {
        _lineObj.positionCount = 2;
        use = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(use)
        {
            _lineObj.SetPosition(0,_dotList[0].transform.position);
            _lineObj.SetPosition(1,_dotList[1].transform.position);

            Vector3 tVec = _dotList[1].transform.position - _dotList[0].transform.position;
            float tValue = (tVec).magnitude;
            _valueText.text = string.Format("{0}mm",tValue.ToString("N2"));
            _valueTextBG.transform.position = _dotList[0].transform.position + tVec*0.5f;

            Vector3 tDirVec = (_valueTextBG.transform.position - _mainCam.position).normalized;
            _valueTextBG.transform.forward = tDirVec;
             _valueTextBG.transform.position += tDirVec * -0.1f;
        }
        
    }
}
