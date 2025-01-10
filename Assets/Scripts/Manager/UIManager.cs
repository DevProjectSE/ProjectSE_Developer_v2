using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 화면에 구현되는 UI, 파괴되지않는다.
public class UIManager : MonoBehaviour
{
    private FadeInOut fadeInOutObj;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
