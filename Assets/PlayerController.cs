using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20.0f;

    public void MoveForward() { transform.position += transform.forward * moveSpeed * Time.deltaTime; }
    public void MoveBackward() { transform.position -= transform.forward * moveSpeed * Time.deltaTime; }
    public void MoveLeft() { transform.position -= transform.right * moveSpeed * Time.deltaTime; }
    public void MoveRight() { transform.position += transform.right * moveSpeed * Time.deltaTime; }
}