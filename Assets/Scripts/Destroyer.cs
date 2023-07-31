using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destructionPS;
    private Animator _animator;

    public void Destroy()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("PlayerDeath");

        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        _destructionPS.transform.parent = null;
        _destructionPS.Play();

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        GameManager.Instance.SpawnPlayer();
        Destroy(gameObject);
    }
}