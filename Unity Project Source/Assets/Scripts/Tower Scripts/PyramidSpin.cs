using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidSpin : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed;
    // Update is called once per frame
    void Update()
    {
        Spin();
    }

    void Spin()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, spinSpeed * 10.0f) * Time.deltaTime, Space.Self);
    }

}
