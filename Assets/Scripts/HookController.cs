using System;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [Header("Lắc Móc")]
    public float swaySpeed = 70f;
    public float minZ = -70f, maxZ = 70f;
    private bool isSwayRight = true;
    public bool isSwaying;
    private float currentZ;

    [Header("Thả Móc")]
    public float dropSpeed = 5f;
    private float minX = -8.5f, maxX = 8.5f, minY = -4.5f;
    public bool isDropping = false;

    [Header("Kéo Móc")]
    public float pullSpeed = 10f;
    public float initialPullSpeed;
    public bool isPulling = false;

    [Header("Ném thuốc nổ")]
    public bool isThrowExplosive = false;

    private Vector3 initialPosition;

    private GoldMinerAnimator goldMinerAnimator;
    void Start()
    {
        isSwaying = true;
        currentZ = transform.eulerAngles.z;
        initialPullSpeed = pullSpeed;
        initialPosition = transform.position;
        goldMinerAnimator = FindObjectOfType<GoldMinerAnimator>();
    }

    void Update()
    {
        if (isSwaying) 
        {
            RotationZ();
            
            if (Input.GetMouseButtonDown(0))
            {
                isSwaying = false;
                isDropping = true;
            }
        }
        else
        {
            MoveHook();
        }

        gameObject.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, transform.position);
    }

    private void MoveHook()
    {
        goldMinerAnimator.ThaMoc();
        if (isDropping)
        {
            transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
            if (transform.position.x >= maxX || transform.position.x <= minX || transform.position.y <= minY)
            {
                isDropping = false;
                isPulling = true;
            }
        }

        if (isPulling)
        {
            goldMinerAnimator.KeoMoc();
            if (Input.GetMouseButtonDown(1))
            {
                if (GameManager.Instance.explosive > 0)
                {
                    goldMinerAnimator.NemThuocNo();
                    isThrowExplosive = true;   
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, pullSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
            {
                goldMinerAnimator.Stop();
                isPulling = false;
                pullSpeed = initialPullSpeed;
                transform.position = initialPosition;
                isSwaying = true;
                isThrowExplosive = false;
            }
        }
    }

    private void RotationZ()
    {
        if (isSwayRight) currentZ += swaySpeed * Time.deltaTime;
        else currentZ -= swaySpeed * Time.deltaTime;

        if (currentZ >= maxZ) isSwayRight = false;
        if (currentZ <= minZ) isSwayRight = true;
        
        transform.rotation = Quaternion.Euler(0, 0, currentZ);
    }
}
