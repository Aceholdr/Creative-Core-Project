using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileMovement : MonoBehaviour
{
    public float leftBound = -10.0f;
    public float rightBound = 10.0f;
    public float bottomBound = -10.0f;
    public float upperBound = 10.0f;
    private float boundBuffer = 0.00001f;

    [SerializeField] private float horizontalInput;
    [SerializeField] private float forwardInput;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if(transform.position.x > leftBound && transform.position.x < rightBound && transform.position.z > bottomBound && transform.position.z < upperBound)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        }
        else if(transform.position.x < leftBound)
        {
            transform.position = new Vector3(leftBound + boundBuffer, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > rightBound)
        {
            transform.position = new Vector3(rightBound - boundBuffer, transform.position.y, transform.position.z);
        }
        else if (transform.position.z < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomBound + boundBuffer);
        }
        else if (transform.position.z > upperBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upperBound - boundBuffer);
        }

        print(transform.position.z);
    }
}
