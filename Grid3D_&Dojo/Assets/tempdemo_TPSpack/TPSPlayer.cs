using UnityEngine;
using System.Collections;

public class TPSPlayer : MonoBehaviour {

    public Transform target;

    public float 
        speed = 1,
        rspeed  = 1,
        friction = 1,
        lerpSpeed = 1;

    Quaternion
        fromRotation,
        toRotation;

    float 
        xDeg, 
        yDeg;


    void Start()
    {
        if (!target)
        {
            target = transform;
        }

        //**hide cursor**
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	// Update is called once per frame
    void Update()
    {
        //**position**
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed,
            y = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        if (x < -0.1f || x > 0.1f || y > 0.1f || y < -0.1f)
            target.position = Vector3.Lerp(transform.position, target.position+new Vector3(x, 0, y) , 1 );


        //**rotation**
        xDeg -= Input.GetAxis("Mouse X") * rspeed * friction;
        yDeg += Input.GetAxis("Mouse Y") * rspeed * friction;
        fromRotation = transform.rotation;
        toRotation = Quaternion.Euler(0, xDeg, 0);

        transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
        //transform.rotation = Camera.main.transform.rotation* Camera.main.transform.rotation*transform.rotation;


    }
}
