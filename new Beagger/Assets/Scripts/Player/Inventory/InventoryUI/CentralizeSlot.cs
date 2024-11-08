using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralizeSlot : MonoBehaviour
{
    void Update()
    {
        transform.position += (transform.parent.position - transform.position) * 5 * Time.deltaTime;
      
    }
}
    