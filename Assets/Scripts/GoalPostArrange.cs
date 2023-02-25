using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

// 골포스트를 배치하고 싶다

// 마커 없이 수평인 지형을 인식해서 농구골대를 배치하고

// 한 번 인식하면 갱신하지 않도록 하고 싶다.

// 

public class GoalPostArrange : MonoBehaviour
{
    //
    public GameObject goalPostFactory;

    // Start is called before the first frame update
    void Start()
    {
        if (goalPostFactory != null)
        {
            goalPostFactory = GameObject.Find("Basketball Goal");
        }

        // 
        goalPostFactory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo))
                {
                    GameObject goalPost = GameObject.Instantiate(goalPostFactory);

                    goalPost.transform.position = hitInfo.point;

                    goalPost.transform.up = hitInfo.normal;

                    Vector3 dir = Camera.main.transform.position - this.transform.position;

                    dir.y = 0;

                    this.transform.forward = dir;
                }
            }
        }
    }
}