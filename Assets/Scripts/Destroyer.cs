using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destructionPS;
    [SerializeField] private AudioClip _destructionSFX;

    private Animator _animator;
    private AudioSource _audioSource;

    public void Destroy()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("PlayerDeath");

        _destructionPS.transform.parent = null;
        _destructionPS.Play();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_destructionSFX);

        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        GameManager.Instance.SpawnPlayer();
        Destroy(gameObject);
    }
}