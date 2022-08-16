using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public float albeto=0.0001F;
    float planck = Mathf.Pow(10, -34) * 6.63F;
    int c = 299792458;
    float frequency = Mathf.Pow(10, 14) * 4.997F;//600 nanometers
    float num = Mathf.Pow(10, 21) * 4.2F*32;
    public GameObject sail;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        sail.transform.position = new Vector3(0, 0, 0);
        //rb.AddForce(new Vector3 (0, 0, 0));
        //rb.AddForce(new Vector3(((2f * (1400 * sail.transform.localScale.x * sail.transform.localScale.y)) / c), 0, 0),ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float accel = ((2f * (1400 * sail.transform.localScale.x * sail.transform.localScale.y)) / c) / rb.mass;

        rb.AddForce(new Vector3(accel,0,0),ForceMode.Acceleration);
        
        
        //sail.transform.position=new Vector3(.5f*accel*Mathf.Pow(Time.unscaledTime,2),0,0);
        Debug.Log(Time.unscaledTime-4.0);


    }
}
