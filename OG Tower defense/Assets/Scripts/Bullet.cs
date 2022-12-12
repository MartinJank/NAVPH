using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    private bool isColliding = false;
    private void Start() {
        Destroy(gameObject, 3f);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (!isColliding) {
            isColliding = true;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.takeDemage(damage);
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        isColliding = false;
    }

    private void Update()
    {
        transform.position += transform.right * 0.3f;
    }
}
