using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float m_panSpeed = 30.0f;
    public float m_panBorderThickness = 10.0f;
    public float m_scrollSpeed;

    public Vector3 m_minClampPos;
    public Vector3 m_maxClampPos;
    public float m_minY = 5.0f;
    public float m_maxY = 15.0f;

    // Update is called once per frame
    void Update()
    {

        if (GameManager.m_gameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - m_panBorderThickness)
        {
            transform.Translate(Vector3.forward * m_panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= m_panBorderThickness)
        {
            transform.Translate(Vector3.back * m_panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= m_panBorderThickness)
        {
            transform.Translate(Vector3.left * m_panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - m_panBorderThickness)
        {
            transform.Translate(Vector3.right * m_panSpeed * Time.deltaTime, Space.World);
        }
            
        float m_scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= m_scroll * 100 * m_scrollSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, m_minClampPos.x, m_maxClampPos.x);
        pos.y = Mathf.Clamp(pos.y, m_minY, m_maxY);
        pos.z = Mathf.Clamp(pos.z, m_minClampPos.z, m_maxClampPos.z);
        transform.position = pos;

    }
}
