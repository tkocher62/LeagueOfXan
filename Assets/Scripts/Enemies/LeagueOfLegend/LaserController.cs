using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private bool isActive = false;
    private bool isTracking = true;

    private bool isCooldown = false;

    private const float damage = 15f;

    private CoroutineHandle coroutine;

    private void Update()
    {
        if (isTracking)
        {
            Vector3 targ = PlayerController.singleton.gameObject.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 180));
        }
    }

    private void OnDestroy()
    {
        Timing.KillCoroutines(coroutine);

        LeagueOfLegendController.singleton.isUsingLaser = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(isActive);
        if (isActive && !isCooldown && collision.gameObject.tag == "Player")
        {
            PlayerController.singleton.Damage(damage);
            Timing.RunCoroutine(StartCooldown().CancelWith(gameObject));
        }
    }

    private IEnumerator<float> StartCooldown()
    {
        isCooldown = true;
        yield return Timing.WaitForSeconds(0.4f);
        isCooldown = false;
    }

    public void StopTracking() => isTracking = false;

    public void SetActive()
    {
        isActive = true;
        Debug.Log("set active");
    }
    public void SetDeactive()
    {
        isActive = false;
        Debug.Log("set deactive");
    }

    public void Destroy() => Destroy(gameObject);
}
