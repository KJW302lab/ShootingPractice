using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [SerializeField] private UsedMagazine       magPrefab;
    [SerializeField] private XRSocketInteractor magSocket;
    [SerializeField] private EmptyShell         shellPrefab;
    [SerializeField] private Transform          shellPosition;

    private UsedMagazine _usedMagazine;
    private Animator     _animator;

    public float  fireDelay = 0.5f;

    private bool  _canFire;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        magSocket.selectEntered.AddListener(OnMagazineInserted);
        
        _canFire = true;
    }

    private void OnMagazineInserted(SelectEnterEventArgs args)
    {
        var magazine = args.interactableObject.transform.GetComponent<Magazine>();
        
        // 총알 추가

        Destroy(magazine.gameObject);
        
        _usedMagazine = Instantiate(magPrefab, transform);
        _usedMagazine.transform.localPosition = Vector3.zero;
        _usedMagazine.transform.localRotation = Quaternion.identity;
        
        magSocket.socketActive = false;
    }

    public void Fire()
    {
        if (_usedMagazine == null)
            return;

        if (_canFire == false)
            return;
        
        StartCoroutine(FireCoroutine());
    }
    
    private IEnumerator FireCoroutine()
    {
        _canFire = false;
        
        _animator.ResetTrigger("Shot");
        _animator.SetTrigger("Shot");
        
        yield return new WaitForSeconds(fireDelay);
        
        _canFire = true;
    }

    public void ExhaustShell()
    {
        Instantiate(shellPrefab, shellPosition.position, shellPosition.rotation);
    }
}