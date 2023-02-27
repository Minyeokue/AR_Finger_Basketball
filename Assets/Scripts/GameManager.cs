using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    int MaxCountBall = 10;
    int currentBallCount = 0;
    bool isGoalCreat = false;
    public bool isRimCreat
    {
        get
        {
            return isGoalCreat;
        }
        set
        {
            isGoalCreat = value;
            if (isGoalCreat == false)
            {
                isGoalCreat = true;
            }
        }
    }
    public int CurrenBallCount
    {
        get
        {
            return currentBallCount;
        }
        set
        {
            currentBallCount = value;
            if (currentBallCount <= MaxCountBall)
            {
                currentBallCount++;
            }
        }
    }
    public int BallMaxCount
    {
        get
        {
            return MaxCountBall;
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
