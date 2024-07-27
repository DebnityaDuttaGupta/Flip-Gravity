using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class TestingScripts : MonoBehaviour
{
    public Transform player;
    public Transform environment;
    public float rotationAngle = 90f;
    public float rotationDuration = 0.1f;
    private bool isRotating = false;
    private bool gravityBackup;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            gravityBackup = rb.useGravity;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRotating)
        {
            StartCoroutine(RotateEnvironment(Vector3.up, -rotationAngle));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isRotating)
        {
            StartCoroutine(RotateEnvironment(Vector3.up, rotationAngle));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isRotating)
        {
            StartCoroutine(RotateEnvironment(Vector3.right, -rotationAngle));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isRotating)
        {
            StartCoroutine(RotateEnvironment(Vector3.right, rotationAngle));
        }
    }

    IEnumerator RotateEnvironment(Vector3 axis, float angle)
    {
        isRotating = true;

        if (rb != null)
        {
            rb.useGravity = false;
        }

        Vector3 pivotPoint = player.position;
        float elapsedTime = 0f;
        Quaternion startingRotation = environment.rotation;
        Quaternion targetRotation = Quaternion.Euler(axis * angle) * startingRotation;
        Vector3 rotationAxis = (pivotPoint - environment.position).normalized;

        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            environment.RotateAround(pivotPoint, axis, angle * t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        environment.rotation = targetRotation;

        if (rb != null)
        {
            rb.useGravity = gravityBackup;
        }

        isRotating = false;
    }
}
