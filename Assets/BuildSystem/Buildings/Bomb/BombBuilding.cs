using UnityEngine;

public class BombBuilding : Building
{
    [SerializeField] private float destroyTimer;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip explosionBombSfx;

    private void Awake()
    {
        if (sfxSource == null)
            sfxSource = GetComponent<AudioSource>();
    }
    
    public override void Init()
    {
        base.Init();
        
        if (explosionBombSfx != null)
            AudioSource.PlayClipAtPoint(explosionBombSfx, transform.position, 1f);
        
        Invoke(nameof(DestroySelf), destroyTimer);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
