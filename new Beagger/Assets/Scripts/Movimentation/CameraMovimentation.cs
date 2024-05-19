using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovimentation : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float velocity;

    private void Move()
    {
        Vector3 pos = new Vector3(Player.position.x, Player.position.y, -10);
        float smoothVelocity = velocity * Time.deltaTime;
        Vector3 newPos = Vector3.Lerp(transform.position, pos, smoothVelocity);
        transform.position = newPos;


    }
    private void Update()
    {
        Move();
    }
}
