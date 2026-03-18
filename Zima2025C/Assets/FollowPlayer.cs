using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerTransform;
    public Vector3 distance;

    public float smoothing;
    public float scrollSensitivity;

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        distance = cameraTransform.position - playerTransform.position;

        cameraTransform.LookAt(playerTransform.position);
    }

    void Update()
    {
        //cameraTransform.position = playerTransform.position + distance;

        distance = Quaternion.Euler(0, Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity, 0) * distance;

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, playerTransform.position + distance, smoothing * Time.deltaTime);
        cameraTransform.LookAt(playerTransform.position);
    }
}