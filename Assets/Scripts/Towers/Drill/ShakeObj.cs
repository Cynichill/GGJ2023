using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObj : MonoBehaviour
{

    private bool shake = false;
    public float speed = 50.0f; //how fast it shakes
    public float amount = 0.01f; //how much it shakes
    public float shakeFor = 0.5f; //How long it shakes
    
    public void Start()
    {
        shake = true;
        //StartCoroutine(shakeTime(shakeFor));
    }

    // Update is called once per frame
    void Update()
    {
        if (shake)
        {
            transform.position = new Vector3(transform.position.x , transform.position.y + Mathf.Sin(Time.time * speed) * amount, transform.position.z);
        }
    }

    private IEnumerator shakeTime(float time)
    {
        yield return new WaitForSeconds(time);
        shake = false;
    }
}
