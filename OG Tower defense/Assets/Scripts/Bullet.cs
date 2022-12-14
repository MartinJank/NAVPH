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
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null) {
                isColliding = true;
                enemy.takeDemage(damage);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.GetComponent<Tower>() == null) {
            isColliding = false;
        }
    }

    private void Update()
    {
        transform.position += transform.right * 0.3f;
    }
}
