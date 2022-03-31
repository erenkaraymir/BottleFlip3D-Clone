using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BottleManager : MonoBehaviour
{
    public float jumpForce;
    public Vector3 jumpDirectionOnGround;
    public Vector3 jumpDirectionOnAir;
    private bool isTouch = true;
    private Rigidbody rb;
    private int touchCount = 0;
    private int speed;
    private float startPos,endPos;
    private bool isClick = false;
    private bool corotine›sDone = false;
    private bool turn = false;
    public Transform to;
    public Transform to2;

    public GameObject start, finish, finishCube;

    public Slider slider;

    public ParticleSystem confetti;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position.x;
        endPos = finishCube.transform.position.x;
    }
    private void Update()
    {
        slider.value = (transform.position.x-startPos) / (endPos-startPos);
        if (Input.GetMouseButtonDown(0))
        {
            start.gameObject.SetActive(false);
            slider.gameObject.SetActive(true);
            touchCount++;
            if(touchCount <=2)
            {
                if(touchCount == 1)
                {
                    rb.AddForce(jumpDirectionOnGround * jumpForce);
                    Debug.Log("corotne");
                    StartCoroutine(RotateObj(1.15f));

                    isClick = true;
                }
                else if(touchCount == 2)
                {
                    //rb.velocity = Vector3.zero;
                    Debug.Log("Havada t˝kland˝");
                    StopCoroutine(RotateObj(1.15f));
                    StartCoroutine(RotateObj(1.15f));
                    rb.AddForce(jumpDirectionOnAir * jumpForce);
                    isClick = false;
                }
            }
        }
     
        Debug.Log(isTouch);
        if (corotine›sDone == true)
        {
            if(transform.eulerAngles.z < 180 && transform.eulerAngles.z > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, to2.rotation, 10f * Time.deltaTime);               
            }
            else if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 360)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, to.rotation, 4f * Time.deltaTime);
            }
            Debug.Log("Buras˝ Áal˝˛˝yor");
        }

    }

    IEnumerator RotateObj(float duration)
    {
        corotine›sDone = false;
        Quaternion startRot = transform.rotation;
        Debug.Log(startRot);
        float t = 0.0f;
        while (t <= duration)
        {
            t += Time.deltaTime;
            transform.rotation = startRot * Quaternion.AngleAxis(t / duration * 360f, Vector3.forward); //or transform.right if you want it to be locally based
            yield return null;
        }
        corotine›sDone = true;
       // transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jumpable"))
        {
            touchCount = 0;
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            confetti.Play();
            Invoke("Finish", .75f);
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Finish()
    {
        slider.gameObject.SetActive(false);
        finish.gameObject.SetActive(true);
    }
}
