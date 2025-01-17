using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [SerializeField] private UsedMagazine       magPrefab;
    [SerializeField] private XRSocketInteractor magSocket;
    [SerializeField] private EmptyShell         shellPrefab;
    [SerializeField] private Transform          shellPosition;

    public int remainAmmo;
    public float fireDelay;


    private UsedMagazine _usedMagazine;
    private Animator     _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        magSocket.selectEntered.AddListener(OnMagazineInserted);
    }

    private void OnMagazineInserted(SelectEnterEventArgs args)
    {
        var magazine = args.interactableObject.transform.GetComponent<Magazine>();

        remainAmmo += magazine.ammoCount;

        Destroy(magazine.gameObject);
        
        _usedMagazine = Instantiate(magPrefab, transform);
        _usedMagazine.transform.localPosition = Vector3.zero;
        _usedMagazine.transform.localRotation = Quaternion.identity;
        
        magSocket.socketActive = false;

        _animator.SetBool("IsBackward", false);
    }

    private void OnMagazineEmpty()
    {
        if (_usedMagazine == null) return;

        _usedMagazine.OnEmpty();

        magSocket.socketActive = true;
    }

    public void Fire()
    {
        if (remainAmmo <= 0)
        {
            // 총알 부족 처리
        }
        else
        {
            if (_fireCoroutine != null)
                StopCoroutine(_fireCoroutine);
            _fireCoroutine = FireCoroutine();
            StartCoroutine(_fireCoroutine);
        }
    }

    IEnumerator _fireCoroutine;
    IEnumerator FireCoroutine()
    {
        _animator.SetBool("IsBackward", true);

        ExhaustShell();

        yield return new WaitForSeconds(fireDelay);

        remainAmmo--;

        if (remainAmmo >= 1)
            _animator.SetBool("IsBackward", false);
        else
            OnMagazineEmpty();
    }

    private void ExhaustShell()
    {
        Instantiate(shellPrefab, shellPosition.position, shellPosition.rotation);
    }
}