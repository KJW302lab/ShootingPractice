using UnityEngine;

public class HitTarget : MonoBehaviour
{
    public Vector3 HitPoint { get; set; }
    
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("HitTarget");
    }

    public void OnHit()
    {
        
    }
}