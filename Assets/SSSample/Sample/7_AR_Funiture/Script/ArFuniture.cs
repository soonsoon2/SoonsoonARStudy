using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

//AR 공간에서 가구를 배치해보는 샘플 예제.
//향후 좀 수정이 진행 될 예정.
public class ArFuniture : MonoBehaviour
{   
    public List<GameObject> _funitureObj = new List<GameObject>();
    public List<GameObject> _funitureObjPlaced = new List<GameObject>();
    public Transform _pivotPoint;
    public ARRaycastManager m_RaycastManager;
    public Vector2 _centerVec;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    public bool _enableCheck;
    public bool _placeChk;
    public int _makeCheckPoint;
    public Vector3 _tVec;

    public GameObject _proxyObj;
    public ObjectTransprant _proxyObjTrans;
    public Transform _funiturePool;

    void Start()
    {
        _centerVec = new Vector2( Screen.width * 0.5f, Screen.height * 0.5f);
    }

    void Update()
    {
        if(!_placeChk) return;
        if (m_RaycastManager.Raycast(_centerVec, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose;
            _enableCheck = true;
            _tVec = hitPose.position;

            if(_proxyObj!=null)
            {
                _proxyObj.transform.position = _tVec;
                _proxyObjTrans.SetTrans(1.0f);
            }
        }
        else
        {
            _enableCheck = false;
            if(_proxyObj!=null)
            {
                _proxyObj.transform.position = _pivotPoint.transform.position;
                _proxyObjTrans.SetTrans(0.1f);
            }
        }
    }

    public void SetPlace()
    {
        if(_enableCheck && _placeChk)
        {
            _placeChk = false;
            _funitureObjPlaced.Add(_proxyObj);
        }
    }

    public void SetRotation(int num)
    {
        if(_proxyObj!=null)
        {
            if(num == -1)
            {
                _proxyObj.transform.localEulerAngles += new Vector3(0,15f,0);
            }
            else
            {
                _proxyObj.transform.localEulerAngles += new Vector3(0,-15f,0);
            }
        }
    }




    public GameObject _selectCanvas;
    
    public void OpenSelectCanvas()
    {
        _selectCanvas.gameObject.SetActive(true);
    }

    public void CloseSelectCanvas()
    {
        if(_proxyObj!=null)
        {
            GameObject tObj = _proxyObj;
            Destroy(tObj);
        }
        
        _selectCanvas.gameObject.SetActive(false);
        _proxyObj = null;
        _proxyObjTrans = null;
    }

    public int _nowFuniture;
    public void SelectFuniture(int num)
    {
        if(_proxyObj!=null)
        {
            GameObject tObj = _proxyObj;
            Destroy(tObj);
        }

        _nowFuniture = num;

        _proxyObj = Instantiate(_funitureObj[_nowFuniture]);
        _proxyObj.transform.SetParent(_funiturePool);
        _proxyObj.transform.localScale = new Vector3(1,1,1);

        _proxyObjTrans = _proxyObj.GetComponent<ObjectTransprant>();

        _selectCanvas.gameObject.SetActive(false);
        _placeChk = true;
    }

    public void DeleteObj()
    {
        if(_proxyObj!=null)
        {
            GameObject tObj = _proxyObj;
            Destroy(tObj);
        }

        _proxyObj = null;
        _proxyObjTrans = null;
    }
}
