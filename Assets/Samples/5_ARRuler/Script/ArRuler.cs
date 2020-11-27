using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ArRuler : MonoBehaviour
{   
    public ARRaycastManager m_RaycastManager;
    public Vector2 _centerVec;
    public Transform _rulerObjPool;
    public GameObject _rulerObj;
    public List<RulerObj> _rulerList = new List<RulerObj>();
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    public Image _centerImage;

    public bool _enableCheck;
    public int _makeCheckPoint;
    public RulerObj _nowRulerObj;
    public Vector3 _tVec;
    public Transform _mainCam;

    void Start()
    {
        _centerVec = new Vector2( Screen.width * 0.5f, Screen.height * 0.5f);
    }

    void Update()
    {
        if (m_RaycastManager.Raycast(_centerVec, s_Hits, TrackableType.FeaturePoint))
        {
            var hitPose = s_Hits[0].pose;
            _enableCheck = true;
            _tVec = hitPose.position;
            OnImage();

            if(_makeCheckPoint==1)
            {
                if(_nowRulerObj!=null) 
                {
                    _nowRulerObj._dotList[1].transform.position = hitPose.position;
                    _nowRulerObj._dotList[1].gameObject.SetActive(true);
                    _nowRulerObj._lineObj.gameObject.SetActive(true);
                    _nowRulerObj._valueTextBG.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            OffImage();
            _enableCheck = false;
            _tVec = new Vector3(0,0,-10000f);

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
                _makeCheckPoint = 0;
            }
        }
    }

    void OnImage()
    {
        _centerImage.color = new Color(1,1,1,0.5f);
    }

    void OffImage()
    {
        _centerImage.color = new Color(1,1,1,0.1f);
    }
    


}
