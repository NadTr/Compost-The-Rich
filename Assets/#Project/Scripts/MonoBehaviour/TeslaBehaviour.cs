using System;
using System.Collections;
using System.Threading.Tasks;

// using System.Numerics;
using UnityEngine;

public class TeslaBehaviour : MonoBehaviour
{
    private Vector3 distanceToMusk;
    [SerializeField] BossBS2 musk;

    void OnEnable()
    {
        float factor = transform.parent.GetComponent<SpriteRenderer>().flipX ? -1 : 1;
        Vector2 localPos = transform.localPosition;

        distanceToMusk = localPos;

        localPos.x *= factor;
        transform.localPosition = localPos;
        // Debug.Log("startPosition = " + startPosition + "ou " + transform.position);
        // Debug.Log("transform.parent.position = " + transform.parent.position);
        // Debug.Log("distanceToMusk = " + distanceToMusk);
    }

    void OnDisable()
    {
        float factor = transform.parent.GetComponent<SpriteRenderer>().flipX ? -1 : 1;
        Vector2 localPos = distanceToMusk;
        transform.localPosition = localPos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            StartCoroutine(Grounded());
        }
    }

    private IEnumerator Grounded()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);

    }
}
