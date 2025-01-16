using System.Collections;
using UnityEngine;

public class EmptyShell : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rb.AddForce(transform.forward * 150f);

        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(3f);
        
        Destroy(gameObject);
    }
}