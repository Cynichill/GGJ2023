using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //Gameobjects
    private Transform target;
    private GameManager gm;

    //Smoothing
    public float smoothing;
    private bool fixSmoothing = false;
    public float smoothFix;

    //Camera bounds
    public Vector2 maxPosition;
    public Vector2 minPosition;

    //Camera direction changing
    private bool direction = false; //Left is false, right is true
    private float directionAdd = 4;
    private float directionChange;
    private bool moveCam = true;

    //Respawn fixing
    public bool respawnFix;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
       // gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
      //  minPosition = gm.minPosition;
       // maxPosition = gm.maxPosition;
    }

    void FixedUpdate()
    {

        //After changing direction, slowly increase smoothing back to full
        if (smoothing < 0.25f && fixSmoothing)
        {
            smoothing += smoothFix; //Could potentially replace with a function to slowly increase smoothing back to its original value.. This looks messy
        }

        if (!direction)
        {
            directionChange = -directionAdd; //Move camera target left when you hit the left trigger
        }
        else
        {
            directionChange = directionAdd; //Move camera target right when you hit the right trigger
        }

        if (transform.position != target.position && !respawnFix)
        {
            moveCam = true; //Reset movecam so it's true unless conditions are fulfilled

            Vector3 targetPosition = new Vector3(target.position.x + directionChange, target.position.y, transform.position.z); //Add direction change so player is not centered

            //Check if player is going in the opposite direction, if they are, stop moving the camnera until they move forward again or hit the opposite trigger
            if (!direction)
            {
                if (target.position.x > transform.position.x - directionChange)
                {
                    moveCam = false;
                }
            }
            else
            {
                if (target.position.x < transform.position.x - directionChange)
                {
                    moveCam = false;
                }
            }

            if (!moveCam)
            {
                targetPosition.x = transform.position.x; //Camera only stops on X axis when player changes direction
            }
            else
            {
                targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            }


            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

        }

    }

    private void Update()
    {
        if (transform.position.x > maxPosition.x)
        {
            transform.position = new Vector3(maxPosition.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < minPosition.x)
        {
            transform.position = new Vector3(minPosition.x, transform.position.y, transform.position.z);
        }

        if (transform.position.y > maxPosition.y)
        {
            transform.position = new Vector3(transform.position.x, maxPosition.y, transform.position.z);
        }
        else if (transform.position.y < minPosition.y)
        {
            transform.position = new Vector3(transform.position.x, minPosition.y, transform.position.z);
        }
    }

    public void TriggerHit(bool dir)
    {
        direction = dir;
        StartCoroutine(DirectionSmoothing());
    }

    public void RespawnFix()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        if (targetPosition.x > maxPosition.x)
        {
            targetPosition = new Vector3(maxPosition.x, targetPosition.y, targetPosition.z);
        }
        else if (targetPosition.x < minPosition.x)
        {
            targetPosition = new Vector3(minPosition.x, targetPosition.y, targetPosition.z);
        }

        if (targetPosition.y > maxPosition.y)
        {
            targetPosition = new Vector3(targetPosition.x, maxPosition.y, targetPosition.z);
        }
        else if (targetPosition.y < minPosition.y)
        {
            targetPosition = new Vector3(targetPosition.x, minPosition.y, targetPosition.z);
        }
        //On respawn, place camera on player to avoid movecam issues.
        transform.position = new Vector3(targetPosition.x, targetPosition.y, -10f);

        respawnFix = true;
        StartCoroutine(RespawnTimer());
    }

    private IEnumerator DirectionSmoothing()
    {
        //When direction changes, lower smoothing so the camera is slower, after a short time fix the smoothing
        smoothing = 0.06f;
        fixSmoothing = false;
        yield return new WaitForSeconds(0.5f);
        fixSmoothing = true;
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(0.2f);
        respawnFix = false;
    }
}