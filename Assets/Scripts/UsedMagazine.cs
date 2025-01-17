using System.Collections;
using UnityEngine;

public class UsedMagazine : MonoBehaviour
{
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
        _rb.useGravity = false;
    }

    public void OnEmpty()
    {
        _rb.isKinematic = false;
        _rb.useGravity = true;

        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(5f);

        Destroy(_rb.gameObject);
    }
}