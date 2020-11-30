using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Scene Load
    public void LoadScene(int num)
    {
        //씬매니저를 사용합니다. 기존 로드 방식은 추천 되지 않으므로 확인 필요.
        //Using SceneManager. Deprecated using Application Load. Please check it.
        SceneManager.LoadScene(num);
    }
}
