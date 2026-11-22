using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    public float damageValue = 1;
    
    [Header("TargetDetection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;
    [SerializeField] private LayerMask targetLayer;

    public void PerformAttack()
    {
        var targetColliders = this.GetDetectedColliders();
        var attacker = this.GetComponent<Transform>();
        foreach (var targetCollider in targetColliders)  
        {
            var entityHealth = targetCollider.GetComponent<EntityHealth>();
            entityHealth?.TakeDamage(this.damageValue, attacker);
        }
    }
    
    private Collider2D[] GetDetectedColliders()
    {
        // Leaving as is for now since attack check is not happening every frame.
        // ReSharper disable once Unity.PreferNonAllocApi
        return Physics2D.OverlapCircleAll(this.targetCheck.position, this.targetCheckRadius, this.targetLayer);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.targetCheck.position, this.targetCheckRadius);
    }
}