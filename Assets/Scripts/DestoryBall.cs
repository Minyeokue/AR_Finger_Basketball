using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryBall : MonoBehaviour
{
    GameObject destory;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        destory = other.gameObject;
        StartCoroutine(DestoryBasketBall());
    }

    IEnumerator DestoryBasketBall()
    {
        yield return new WaitForSeconds(2);
        Destroy(destory);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
