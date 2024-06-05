using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovimentation : MonoBehaviour
{


    [SerializeField] Transform Player;
    [SerializeField] float velocity = 1f;
    [SerializeField] float sizeChangeSpeed = 1f;
    [SerializeField] float minSize, maxSize; 

    private Camera cam;
    public float targetSize;
    private bool isMovingSize = false;

    public bool isUsing = false;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Camera component not found!");
            return;
        }
        targetSize = cam.orthographicSize;
    }

    void Update()
    {
        // Atualiza a posição da câmera
        if (!isUsing)
        {
            MoveBackCamera();
            Move(Player);
        }
        else
        {
            MoveCamera();
        }

        // Atualiza o tamanho da câmera gradualmente
        if (isMovingSize)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * sizeChangeSpeed);
            if (Mathf.Abs(cam.orthographicSize - targetSize) < 0.01f)
            {
                cam.orthographicSize = targetSize;
                isMovingSize = false;
            }
        }
    }

    public void Move(Transform target)
    {
        Vector3 pos = new Vector3(target.position.x, target.position.y, -10);
        float smoothVelocity = velocity * Time.deltaTime;
        Vector3 newPos = Vector3.Lerp(transform.position, pos, smoothVelocity);
        transform.position = newPos;
    }

    public void MoveCamera()
    {
        isUsing = true;
        if (cam.orthographicSize >= minSize)
        {
            targetSize = minSize; // Novo valor desejado para o tamanho da câmera
            isMovingSize = true;
        }
    }

    public void MoveBackCamera()
    {
        if (cam.orthographicSize <= maxSize)
        {
            targetSize = maxSize; // Novo valor desejado para o tamanho da câmera
            isMovingSize = true;
        }
        isUsing = false;
    }
}
