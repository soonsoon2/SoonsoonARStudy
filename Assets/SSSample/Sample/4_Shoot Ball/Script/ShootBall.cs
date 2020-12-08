using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AR로 인식된 공간에 공을 날려보는 아주 간단한 예제.
//A very simple example of throwing a ball in a space recognized as AR
public class ShootBall : MonoBehaviour
{
    public GameObject _theBall; //생성 소스, Generating source
    public Transform _camObj; //카메라 오브젝트, Camera object
    public Transform _shootPoint; // 공이 날라가는 시작 좌표, The starting point of flying the ball.
    
    public void ShootBallObj()
    {
        GameObject tObj = Instantiate(_theBall); // 오브젝트를 생성하는 코드 , Instantiate the object
        tObj.transform.position =  _shootPoint.transform.position; //포지션 좌표를 연동해줌. Location linkage.
        Vector3 tVec = (_shootPoint.transform.position- _camObj.transform.position).normalized; //방향벡터 확인 , Check direction vector
        Rigidbody tR = tObj.GetComponent<Rigidbody>(); //리지드 바디 컴포넌트 가져옴  , Get the Rigidbody Component.

        tR.AddForce(tVec*100f); //힘을 가해줌, Apply force
    }
}
