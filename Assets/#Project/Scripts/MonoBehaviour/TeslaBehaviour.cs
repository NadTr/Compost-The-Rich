using System.Collections;
// using System.Numerics;
using UnityEngine;

public class TeslaBehaviour : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 distanceToMusk;
    [SerializeField] BossBS2 musk;

    void OnEnable()
    {
        startPosition = transform.position;
        distanceToMusk = startPosition - transform.parent.position;

    }

    void OnDisable()
    {
        bool result = musk.GetGoRight();
        if (result == false)
        {
            transform.position = transform.parent.position - distanceToMusk;
        }
        else
        {
            transform.position = -transform.position + distanceToMusk;
        }
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
