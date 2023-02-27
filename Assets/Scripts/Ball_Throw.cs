using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


// 첫 터치 했을때와 마지막 터치 방향을 구해 그 방향으로 발사한다.
// 방향에 addfoce로 물리 적용해서 해보자.
// 필요속성 : 볼을 생성시켜주는 오브젝트, 볼 생성 위치값, rigidbody가져오기 


public class Ball_Throw : MonoBehaviour
{

    public GameObject ballFactory;
    Rigidbody rigid;
    int creatBallCount;
    int ballMaxCount;
    Transform cam;
    float ballZ = 1f;
    GameObject ball;
    bool ballFollw = false;
    Vector3 startPos;
    Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        creatBallCount = GameManager.instance.CurrenBallCount;
        ballMaxCount = GameManager.instance.BallMaxCount;
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        Create_Pc();
        MoveBall_PC();
        ThrowBall_PC();
#else
        CreateBall();
        MoveBall();
        ThrowBall();
#endif

    }


    // 터치했을때 공을 생성하고 드래그하면 볼이 따라온다
    void CreateBall()
    {
        //터치 했을때 
        Touch touch = Input.GetTouch(0);
        if (creatBallCount < ballMaxCount && touch.phase == TouchPhase.Began && GameManager.instance.isRimCreat == true)
        {
            GameObject newBall = Instantiate(ballFactory);
            ball = newBall;
            startPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z + ballZ));
            rigid = ball.GetComponent<Rigidbody>();
            rigid.useGravity = false;
            ballFollw = true;
            creatBallCount++;
            ball.transform.position = new Vector3(cam.position.x, cam.position.y, cam.position.z + ballZ);
        }
    }

    void Create_Pc()
    {
        if (creatBallCount < ballMaxCount && Input.GetMouseButtonDown(0) && GameManager.instance.isRimCreat == true)
        {
            GameObject newBall = Instantiate(ballFactory);
            ball = newBall;
            startPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z + ballZ));
            rigid = ball.GetComponent<Rigidbody>();
            rigid.useGravity = false;
            ballFollw = true;
            creatBallCount++;
            ball.transform.position = new Vector3(cam.position.x, cam.position.y, cam.position.z + ballZ);
        }
    }
    void MoveBall_PC()
    {
        if (ballFollw && Input.GetMouseButton(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z + ballZ));
            ball.transform.position = point;
        }

    }
    void MoveBall()
    {
        Touch touch = Input.GetTouch(0);
        if (ballFollw && touch.phase == TouchPhase.Moved)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z + ballZ));
            ball.transform.position = point;
        }

    }
    private void ThrowBall_PC()
    {
        if (Input.GetMouseButtonUp(0)&& ball != null)
        {
            ballFollw = false;
            endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z + ballZ));
            //볼의 X,Y 방향
            Vector3 dir = endPos - startPos;
            //볼의 z 방향
            Vector3 camZ = Camera.main.transform.forward;
            rigid.AddForce(dir * dir.magnitude * 10 + camZ * dir.magnitude * 15);
            rigid.useGravity = true;
        }
    }
    private void ThrowBall()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Ended && ball != null)
        {
            ballFollw = false;
            endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Input.mousePosition.z + ballZ));
            //볼의 X,Y 방향
            Vector3 dir = endPos - startPos;
            //볼의 z 방향
            Vector3 camZ = Camera.main.transform.forward;
            rigid.AddForce(dir * dir.magnitude * 10 + camZ * dir.magnitude * 15);
            rigid.useGravity = true;
        }
    }
}
