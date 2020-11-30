using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//배치되는 가구 3D 오브젝트를 반투명으로 보여주기 위한 코드.
//Shader 상에 X-ray 로 오브젝트가 보여지는 문제를 해결하기 위한 코드.
//반투명으로 보여질 필요가 없는 경우 무시해도 된다.
[ExecuteInEditMode]
public class ObjectTransprant : MonoBehaviour
{
    public enum BlendMode
     {
         Opaque,
         Transparent
     }

    public float _testValue;
    public bool _use;
    public List<Renderer> _rendereList = new List<Renderer>();
    // Start is called before the first frame update
    public void SetTrans(float value)
    {
        foreach( var i in _rendereList )
        {
            Color tColor = i.material.color;
            i.material.color = new Color (1,1,1,value);

            if( value < 0.9f) ChangeRenderMode(i.material,BlendMode.Transparent);
            else ChangeRenderMode(i.material,BlendMode.Opaque);
        }
    }

    void Update()
    {
        if(_use)
        {
            SetTrans(_testValue);
        }
    }

     public static void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
     {
         switch (blendMode)
         {
             case BlendMode.Opaque:
                 standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                 standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                 standardShaderMaterial.SetInt("_ZWrite", 1);
                 standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                 standardShaderMaterial.renderQueue = -1;
                 break;
             case BlendMode.Transparent:
                 standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                 standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                 standardShaderMaterial.SetInt("_ZWrite", 0);
                 standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                 standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                 standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                 standardShaderMaterial.renderQueue = 3000;
                 break;
         }
 
     }
}
