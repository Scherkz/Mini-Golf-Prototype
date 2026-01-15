using UnityEngine;
using UnityEngine.UI;

public class BombBuilding : Building
{
    [SerializeField] private float destroyTimer;
    [SerializeField] private AudioClip explosionBombSfx;
    
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
