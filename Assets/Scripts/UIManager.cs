using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    Text currentScore, bestScore;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = GameObject.Find("Canvas/Current").GetComponent<Text>();
        bestScore = GameObject.Find("Canvas/Best").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}