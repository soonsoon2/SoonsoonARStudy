using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

//AR 가상 공간에서 거리 길이 측정을 진행하는 예제 앱. Measure length in AR space
public class ArRuler : MonoBehaviour
{   
    public ARRaycastManager m_RaycastManager; //ARRaycastManager 컴퍼넌트를 연동
    public Vector2 _centerVec; //화면의 중심점 좌표
    public Transform _rulerObjPool; //복제 생성된 오브젝트 저장소
    public GameObject _rulerObj; //복제를 위한 소스
    public List<RulerObj> _rulerList = new List<RulerObj>(); //제작된 오브젝트들 저장
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>(); //AR Hit 저장소, Raycast 를 위해서 활용됨.
    public Image _centerImage; //레이캐스트 체크를 위한 디버그 이미지 역할 수행.

    public bool _enableCheck; //현재 배치가 가능한지를 체크해주는 Bool
    public int _makeCheckPoint; // 현재 몇번째 체크인지를 체크해주는 값
    public RulerObj _nowRulerObj; // 현재 배치중인 측정 자 오브젝트
    public Vector3 _tVec; //배치가 진행될 좌표 연동
    public Transform _mainCam; //카메라 오브젝트
    public Transform _pivotObj; //측정면의 각도를 보여주기위한 피벗 오브젝트

    void Start()
    {
        //화면 중앙 좌표값 획득, 스크린 사이즈에 따라 연동됨.
        _centerVec = new Vector2( Screen.width * 0.5f, Screen.height * 0.5f);
    }

    void Update()
    {
        //AR Raycast 사용. AR Session Origin 에 Raycast Manager 필요.
        if (m_RaycastManager.Raycast(_centerVec, s_Hits, TrackableType.FeaturePoint))
        {
            var hitPose = s_Hits[0].pose; // 첫번째로 측정된 면의 정보를 가져옴.
            _enableCheck = true;
            _tVec = hitPose.position; // 좌표 연동
            OnImage(); //경로상에 측정 가능한 면이 존재하는지.

            if(_makeCheckPoint==1)
            {
                if(_nowRulerObj!=null) 
                {
                    _pivotObj.transform.rotation = hitPose.rotation; // 각도값 연동.
                    _nowRulerObj._dotList[1].gameObject.SetActive(true);
                    _nowRulerObj._lineObj.gameObject.SetActive(true);
                    _nowRulerObj._valueTextBG.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            OffImage();//경로상에 측정 가능한 면이 존재하는지.
            //없을 경우 아래와 같이 초기화를 시켜줘야 한다.
            _enableCheck = false;
            _tVec = new Vector3(0,0,-10000f);
            _pivotObj.transform.rotation = Quaternion.identity;
            if(_makeCheckPoint==1)
            {
                if(_nowRulerObj!=null) 
                {
                    _nowRulerObj._dotList[1].gameObject.SetActive(false);
                    _nowRulerObj._lineObj.gameObject.SetActive(false);
                    _nowRulerObj._valueTextBG.gameObject.SetActive(false);
                }
            }
        }
    }
    //값 측정이 가능하면 자 오브젝트를 제작해준다.
    void MakeRulerObj()
    {
        GameObject tObj = Instantiate(_rulerObj);
        tObj.transform.SetParent(_rulerObjPool);
        tObj.transform.position = Vector3.zero;
        tObj.transform.localScale = new Vector3(1,1,1);

        _nowRulerObj = tObj.GetComponent<RulerObj>();
        _rulerList.Add(_nowRulerObj);
        _nowRulerObj._dotList[0].transform.position = _tVec;
        _nowRulerObj._mainCam = _mainCam;

        _makeCheckPoint = 1;
    }

    //버튼과 연동되는 메소드. 
    public void SetRuler()
    {
        if(_enableCheck)
        {
            if(_makeCheckPoint==0)
            {
                MakeRulerObj();
            }
            else if(_makeCheckPoint==1)
            {
                _nowRulerObj.use = false; // 배치를 완료했으니 update 가 돌지 않도록 한다.
                _makeCheckPoint = 0;
            }
        }
    }

    //가운데 디버깅용 이미지의 색상값 조정.
    void OnImage()
    {
        _centerImage.color = new Color(1,1,1,0.5f);
    }

    void OffImage()
    {
        _centerImage.color = new Color(1,1,1,0.1f);
    }
    


}
