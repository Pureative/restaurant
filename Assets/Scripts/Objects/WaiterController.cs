using System;
using UnityEngine;
using UnityEngine.Events;

public class WaiterController : MonoBehaviour
{
    public Animator animator;
    public UnityEvent StartThrow;
    public float moveSpeed = 10f;

    private int _throwAnimationHash = Animator.StringToHash("Throw");
    private Vector3 _selectPosition;
    
    private void OnEnable()
    {
        _selectPosition = transform.position;
    }
    
    public void Throw()
    {
        animator.SetTrigger(_throwAnimationHash);
    }
    
    public void OnAnimThrow()
    {
        StartThrow.Invoke();
    }

    public void MoveToSelectPosition(Vector3 position)
    {
        _selectPosition.z = position.z;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _selectPosition, Time.deltaTime * moveSpeed);
    }
}