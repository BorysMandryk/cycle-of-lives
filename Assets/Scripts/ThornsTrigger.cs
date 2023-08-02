using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Destroyer>().Destroy();
        //GameManager.Instance.DestroyPlayer();
    }
}
