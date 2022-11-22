using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Position Variables")]
    public Transform Target;
    public float smooth;
    public Vector2 minPos;
    public Vector2 maxPos;

    [Header("Animator")]
    public Animator anim;

    [Header("PositionReset")]
    public VectorValue cameraMin;
    public VectorValue cameraMax;

    void Start()
    {
        maxPos = cameraMax.initialValue;
        minPos = cameraMin.initialValue;
        anim = GetComponent<Animator>();
        transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != Target.position)
        {
            Vector3 targetPos = new Vector3 (Target.position.x, Target.position.y, transform.position.z);

            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, targetPos, smooth);
        }
    }
    public void BeginKick()
    {
        anim.SetBool("kick_active", true);
        StartCoroutine(KickCo());
    }
    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("kick_active", false);
    }
}
