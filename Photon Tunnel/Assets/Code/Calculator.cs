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
        GameObject temp = new GameObject();
        float accel = ((2f * (1400 * sail.transform.localScale.x * sail.transform.localScale.y)) / c) / 5;

        for (int x =-8; x <sail.transform.localScale.x/2; x++)
        {
            for (int y = -1; y < sail.transform.localScale.y/2; y++)
            {
                rb.AddForceAtPosition(new Vector3(accel/((sail.transform.localScale.x)*(sail.transform.localScale.y)), 0, 0), new Vector3(sail.transform.position.x, y+.5f, x +.5f), ForceMode.Acceleration);
                if (Mathf.RoundToInt(Time.unscaledTime) % 10 == 1)
                {
                    Debug.Log(Time.unscaledTime);
                    //Debug.Log(sail.transform.position.x);
                    temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    temp.transform.position= new Vector3(0 + sail.transform.position.x, y+.5f , x +.5f);
                    temp.transform.localScale = new Vector3(.1f, .1f, .1f);
                }
            }
        }


        //sail.transform.position=new Vector3(.5f*accel*Mathf.Pow(Time.unscaledTime,2),0,0);
        Debug.Log(accel);


    }
}
