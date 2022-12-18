using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterTime : MonoBehaviour
{
    [SerializeField] private float lifespan;
    [SerializeField] private GameObject rangeDamage;

    void Update()
    {
        Destroy(gameObject, lifespan);
    }
    // void Start()
    // {
    //     StartCoroutine(waiter());
    // }

    // IEnumerator waiter()
    // {
    //     //Wait for 4 seconds
    //     yield return new WaitForSeconds(lifespan);
    //     rangeDamage.SetActive(false);

    // }
}
