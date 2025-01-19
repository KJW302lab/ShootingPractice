using UnityEngine;

public class HitTarget : MonoBehaviour
{
    public Vector3 HitPoint { get; set; }

    private Animator _anim;
    private bool _isAppear = true;
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        gameObject.layer = LayerMask.NameToLayer("HitTarget");
    }

    public void Appear()
    {
        if (_isAppear)
            return;

        _isAppear = true;
        _anim.SetBool("IsAppear", _isAppear);
    }

    public void Disappear()
    {
        if (_isAppear == false)
            return;

        _isAppear = false;
        _anim.SetBool("IsAppear", _isAppear);
    }

    public void OnHit()
    {
        Disappear();
    }
}