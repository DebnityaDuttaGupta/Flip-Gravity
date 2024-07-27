using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    [Header("References")]
    public Transform Environment;
    public Transform Player;

    public float rotationDuration = 0.5f;
    public float slowMotionFactor = 0.1f;

    private bool isFlipping = false;

    private Quaternion targetRotation;
    private BoxCollider playerCollider;

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!isFlipping)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartGravityFlip(Vector3.right);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartGravityFlip(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartGravityFlip(Vector3.forward);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartGravityFlip(Vector3.back);
            }
        }
        
    }

    private void StartGravityFlip(Vector3 axis)
    {
        isFlipping = true;
        targetRotation = Quaternion.AngleAxis(90, axis) * Environment.rotation;

        playerCollider.enabled = false;

        StartCoroutine(RotateEnvironment(targetRotation));
    }

    IEnumerator RotateEnvironment(Quaternion endRotation)
    {
        Quaternion startRotation = Environment.rotation;
        float elapsedTime = 0;

        Time.timeScale = slowMotionFactor;

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            Environment.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationDuration);
            yield return null;
        }

        Environment.rotation = endRotation;

        AdjustPlayerPosition();

        playerCollider.enabled = true;

        Time.timeScale = 1.0f;

        isFlipping = false;
    }

    private void AdjustPlayerPosition()
    {
        Player.position += Player.transform.up * 0.1f;
    }
}
