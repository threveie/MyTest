using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    public float parallaxFactor = 0.1f;//整体移动
    public float frameParallaxFactor = 0.3f;//每层移动量
    public float smoothX = 4;
    public Transform[] backgrounds;//transfrom是真实位置

    private Transform cam;
    private Vector3 camPrePos;
    

    private void Awake()
    {
        cam = Camera.main.transform;
        camPrePos = cam.position;
    }

    void Start()
    {
        //  camPrePos = cam.position;或者写在这儿
    }


    void bkParallax()//背景移动
    {
        float fparallax = (camPrePos.x - cam.position.x)* parallaxFactor;//整体平移量
        //每层
        for(int i = 0; i <backgrounds.Length;i++)
        {
            float bkNewX = backgrounds[i].position.x + fparallax * (1 + i*frameParallaxFactor);
            Vector3 bkNewPos = new Vector3(bkNewX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, bkNewPos, Time.deltaTime * smoothX);//赋新位置
        }
        camPrePos = cam.position;
    }
    // Update is called once per frame
    void Update()
    {
        bkParallax();
    }
}
