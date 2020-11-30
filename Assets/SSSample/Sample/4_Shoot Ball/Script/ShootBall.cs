using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AR로 인식된 공간에 공을 날려보는 아주 간단한 예제.
public class ShootBall : MonoBehaviour
{
    public GameObject _theBall; //생성 소스
    public Transform _camObj; //카메라 오브젝트
    public Transform _shootPoint; // 공이 날라가는 시작 좌표.
    // Start is called before the first frame update

    public void ShootBallObj()
    {
        GameObject tObj = Instantiate(_theBall); // 오브젝트를 생성하는 코드
        tObj.transform.position =  _shootPoint.transform.position; //포지션 좌표를 연동해줌.
        Vector3 tVec = (_shootPoint.transform.position- _camObj.transform.position).normalized; //방향벡터 확인
        Rigidbody tR = tObj.GetComponent<Rigidbody>(); //리지드 바디 컴포넌트 가져옴

        tR.AddForce(tVec*100f); //힘을 가해줌
    }
}
