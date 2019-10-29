using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public GameObject objectToFollow;
    private Vector3 offset = new Vector3(0, 0, 0);
    public float speed = 2.0f;

    void Update()
    {
        transform.position = objectToFollow.transform.position + offset;
        //float interpolation = speed * Time.deltaTime;

        //Vector3 position = this.transform.position;
        //position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
        //position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);

        //this.transform.position = position;
    }
}
