using UnityEngine;


public class ItemEffect : ScriptableObject
{
    [TextArea]
    public string EffectDescription;
    
    public virtual void ExecuteEffect(Transform enemyPosition)
    {
        Debug.Log("effect executed");
    }
}
