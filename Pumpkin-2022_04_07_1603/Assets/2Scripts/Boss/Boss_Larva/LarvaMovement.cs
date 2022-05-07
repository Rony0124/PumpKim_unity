using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LarvaMovement : MonoBehaviour
{
    public Transform prevBody;

    //Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, prevBody.position) > 0.3f)
        {
            transform.position = Vector3.Lerp(transform.position, prevBody.position, 0.08f);
        }
        else
        {
            transform.position = this.transform.position;
        }
        
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
