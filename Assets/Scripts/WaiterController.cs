using UnityEngine;
using UnityEngine.Events;

public class WaiterController : MonoBehaviour
{
    public Animator animator;
    public UnityEvent StartThrow;

    private int _throwAnimationHash = Animator.StringToHash("Throw");
    
    public void Throw()
    {
        animator.SetTrigger(_throwAnimationHash);
    }
    
    public void OnAnimThrow()
    {
        StartThrow.Invoke();
    }
}