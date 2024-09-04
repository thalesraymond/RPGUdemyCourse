using UnityEngine;


public class ItemEffect : ScriptableObject
{
    public virtual void ExecuteEffect(Transform enemyPosition)
    {
        Debug.Log("effect executed");
    }
}
