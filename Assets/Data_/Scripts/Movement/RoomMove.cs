using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChangeMin;
    public Vector2 cameraChangeMax;
    public Vector3 playerChange;
    public CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger){

            cam.minPos.x = cameraChangeMin.x;
            cam.minPos.y = cameraChangeMin.y;
            cam.maxPos.x = cameraChangeMax.x;
            cam.maxPos.y = cameraChangeMax.y;
            other.transform.position += playerChange;

            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }
    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
