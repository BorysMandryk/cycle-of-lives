using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Animator _animator;

    public void Destroy()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("PlayerDeath");

        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        GameManager.Instance.SpawnPlayer();
        Destroy(gameObject);
    }
}