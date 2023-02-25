using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

// 골포스트를 배치하고 싶다

// 마커 없이 수평인 지형을 인식해서 농구골대를 배치하고

// 한 번 인식하면 갱신하지 않도록 하고 싶다.

// 

public class GoalPostArrange : MonoBehaviour
{
    ARRaycastManager arRaycastManager;

    public GameObject goalPostFactory;

    public GameObject arCamera;
    public GameObject unityCamera;
    public GameObject unityWall;

    bool isCreated = false;

    Vector3 center;

    void Start()
    {
        center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        arRaycastManager = GetComponent<ARRaycastManager>();

#if UNITY_EDITOR
        arCamera.SetActive(false);
        unityCamera.SetActive(true);
        unityWall.SetActive(true);
#elif UNITY_ANDROID
        arCamera.SetActive(true);
        unityCamera.SetActive(false);
        unityWall.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        UpdateForUnityEditor();        

#elif UNITY_ANDROID
        UpdateForAndroid();

#endif
        if (isCreated)
        {
            unityWall.SetActive(false);
        }
    }

    private void UpdateForAndroid()
    {
        List<ARRaycastHit> arHit = new List<ARRaycastHit>();
     
        if (arRaycastManager.Raycast(center, arHit))
        {
            unityWall.SetActive(true);
            unityWall.transform.position = arHit[0].pose.position;
            unityWall.transform.rotation = arHit[0].pose.rotation;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !isCreated)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo))
                {
                    isCreated = true;

                    GameObject goalPost = GameObject.Instantiate(goalPostFactory);

                    goalPost.transform.position = hitInfo.point;

                    goalPost.transform.forward = hitInfo.normal;

                    goalPost.transform.Rotate(90.0f, 0.0f, 0.0f);
                }
            }
        }
    }

    private void UpdateForUnityEditor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Input.GetButtonDown("Fire1") && !isCreated)
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                isCreated = true;

                GameObject goalPost = GameObject.Instantiate(goalPostFactory);

                goalPost.transform.position = hitInfo.point;

                goalPost.transform.forward = hitInfo.normal;

                goalPost.transform.Rotate(90.0f, 0.0f, 0.0f);
            }
        }
    }
}