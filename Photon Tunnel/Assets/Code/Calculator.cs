using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    

    public float albeto = 0.0001F;
    float planck = Mathf.Pow(10, -34) * 6.63F;
    int c = 299792458;
    float frequency = Mathf.Pow(10, 14) * 4.997F;//600 nanometers
    float num = Mathf.Pow(10, 21) * 4.2F * 32;
    public GameObject sail;
    public float tubesize;
    public float hres;
    public Rigidbody rb;
    public int mult;
    public float q;
    public float vres;
    public GameObject point;
    // Start is called before the first frame update
    void Start()
    {
        point.transform.position = Vector3.zero;
        //sail.transform.position = new Vector3(0, 0, 0);
        //rb.AddForce(new Vector3 (0, 0, 0));
        //rb.AddForce(new Vector3(((2f * (1400 * sail.transform.localScale.x * sail.transform.localScale.y)) / c), 0, 0),ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Round(Time.fixedUnscaledTime) % 30 == 1)
        {
            Debug.Log(Time.fixedUnscaledTime);
            Debug.Log(sail.transform.position.x);
        }
        Vector3 vector = new Vector3();
        //GameObject temp = new GameObject();
        int hits = 0;
        float accel = mult*((2f * (1368 * sail.transform.localScale.x * sail.transform.localScale.y)) / c) / 5;
        for (float x = tubesize * -sail.transform.localScale.x / 2 / hres; x < tubesize * sail.transform.localScale.x / 2 / hres; x++)
        {
            for (float y = tubesize * -sail.transform.localScale.y / 2 / vres; y < tubesize * sail.transform.localScale.y / 2 / vres; y++)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(-1, (y + .5f) * vres + sail.transform.position.y, (x + .5f) * hres + sail.transform.position.z), Vector3.right, out hit))

                {
                    hits += 1;
                }
            }
        }
        Debug.Log(hits);

        for (float x=tubesize*-sail.transform.localScale.x/2/hres;x< tubesize*sail.transform.localScale.x / 2 / hres; x++)
        {
            for(float y = tubesize*-sail.transform.localScale.y / 2 / vres; y < tubesize*sail.transform.localScale.y / 2 / vres; y++)
            {

                //Debug.Log(y);
                RaycastHit hitFo;
                Debug.DrawRay(new Vector3(-1, (y + .5f) * vres + sail.transform.position.y, (x + .5f) * hres + sail.transform.position.z), Vector3.right*100,color:Color.red);
                if (Physics.Raycast(new Vector3(-1,(y+.5f)*vres+sail.transform.position.y,(x+.5f)*hres + sail.transform.position.z), Vector3.right, out hitFo))

                {
                    //Debug.DrawRay(new Vector3(-1, (y + .5f) * vres + sail.transform.position.y, (x + .5f) * hres + sail.transform.position.z), Vector3.right * 100, color: Color.white);
                    //Debug.Log("hit");
                    Vector3 cachedNormal = hitFo.normal; // Normal of the surface the ray hit
                    //Debug.Log(cachedNormal);
                    vector += -1 * cachedNormal.normalized * (rb.mass * accel / (((sail.transform.localScale.x / hres) * (sail.transform.localScale.y / vres))));
                    rb.AddForceAtPosition(-1*cachedNormal.normalized * (rb.mass * accel /hits), hitFo.point);
                    
                }
            }
        }
        //Debug.DrawRay(sail.transform.position, vector);
        //Debug.Log(vector);
        /*
        for (float x = -8f; x < sail.transform.localScale.x / 2f; x++)
        {
            for (float y = -1f; y < sail.transform.localScale.y / 2f; y++)
            {
                if (x > -18)
                {
                    float crossSectionalArea = ((sail.transform.localScale.x) * (sail.transform.localScale.y)) * Mathf.Cos(sail.transform.localRotation.ToEulerAngles().y-Mathf.Deg2Rad*90) * Mathf.Cos(sail.transform.localRotation.ToEulerAngles().z + Mathf.Deg2Rad * 0);

                    Debug.Log(crossSectionalArea);
                    point.transform.position = new Vector3(sail.transform.position.x + .5f, y, x + .5f);
                    point.transform.RotateAround(sail.transform.position, Vector3.down, sail.transform.localRotation.ToEulerAngles().y - 90);
                    Quaternion rot = sail.transform.localRotation;// Quaternion.Euler(sail.transform.localRotation.eulerAngles.x, sail.transform.localRotation.eulerAngles.y, sail.transform.localRotation.eulerAngles.z);
                    
                    var v = sail.transform.position - new Vector3(sail.transform.position.x + .5f, y, x + .5f);
                    v = rot * v;
                    v = new Vector3(sail.transform.position.x + .5f, y, x + .5f) + v;
                    v = rot * (new Vector3(sail.transform.position.x + .5f, y, x + .5f) - sail.transform.position) + sail.transform.position;
                    //temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    //temp.transform.position = v;
                    //temp.transform.localScale = new Vector3(.1f, .1f, .1f);
                    //temp.transform.RotateAround(sail.transform.position, Vector3.down, sail.transform.localRotation.ToEulerAngles().y - 90);
                    //temp.transform.position = new Vector3((sail.transform.position.x + q*x * Mathf.Sin(sail.transform.localRotation.ToEulerAngles().y + Mathf.Deg2Rad * 90)), y+.5f, ( (x+.5f) * q*Mathf.Cos(sail.transform.localRotation.ToEulerAngles().y+Mathf.Deg2Rad*90 )));
                    //temp.transform.position = new Vector3((sail.transform.position.x), y + .5f, (x + .5f ));

                    rb.AddForceAtPosition(Vector3.right * (rb.mass * accel / ((sail.transform.localScale.x) * (sail.transform.localScale.y))), new Vector3((sail.transform.position.x + q * x * Mathf.Sin(sail.transform.localRotation.ToEulerAngles().y + Mathf.Deg2Rad * 90)), y + .5f, ((x + .5f) * q * Mathf.Cos(sail.transform.localRotation.ToEulerAngles().y + Mathf.Deg2Rad * 90))), ForceMode.Force);
                    
                    //Debug.Log(sail.transform.localRotation.ToEulerAngles().y);
                        //Debug.Log(Time.unscaledTime);
                        //Debug.Log(sail.transform.position.x);
                        
                    
                    //Debug.Log(v);

                }
            }
        }*/


        //sail.transform.position=new Vector3(.5f*accel*Mathf.Pow(Time.unscaledTime,2),0,0);
        //Debug.Log(accel);


    }
}
