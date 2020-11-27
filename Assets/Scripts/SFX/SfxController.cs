using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{
    public static SfxController singleton;

    public Sfx buttonClick;
    public Sfx sprayPaint;
    public Sfx throwItem;
    public Sfx laserShoot;
    public Sfx damageEntity;
    public Sfx explosion;
    public Sfx bigExplosion;
    public Sfx sniperShot;
    public Sfx beeBuzz;
    public Sfx potionPickup;
    public Sfx fireballExplosion;
    public Sfx fireball;
    public Sfx bigLaser;
    public Sfx fadeOut;
    public Sfx laserCharge;
    public Sfx slam;

    internal List<Sfx> sfx;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        sfx = new List<Sfx>()
        {
            buttonClick,
            sprayPaint,
            throwItem,
            laserShoot,
            damageEntity,
            explosion,
            bigExplosion,
            sniperShot,
            beeBuzz,
            potionPickup,
            fireballExplosion,
            fireball,
            bigLaser,
            fadeOut,
            laserCharge,
            slam
        };
    }
}
