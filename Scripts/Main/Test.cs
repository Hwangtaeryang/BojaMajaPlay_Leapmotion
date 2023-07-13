using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 캔버스 해상도 조절 > 16 : 9
        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, true);
    }

    // Update is called once per frame
    void Update()
    {
        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, true);
    }
}
