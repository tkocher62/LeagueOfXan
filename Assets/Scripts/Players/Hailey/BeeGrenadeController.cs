using Assets.Scripts.Enemies;
using MEC;
using UnityEngine;

public class BeeGrenadeController : MonoBehaviour
{
    private GameObject explosionPrefab;
    private Rigidbody2D body;

    private float damageScale = 40f;
    private float maxDamage = 25f;

    private Vector3 lastFrameVelocity = Vector3.zero;
    private Vector2 defaultPower = new Vector2(1, 0);

    void Start()
    {
        explosionPrefab = Resources.Load<GameObject>("Prefabs/Effects/Explosion");

        body = gameObject.AddComponent<Rigidbody2D>();
        body.gravityScale = 0f;
        gameObject.transform.up = PlayerController.singleton.movement.normalized;
        body.drag = 5f;
        body.angularDrag = 2f;
        body.AddTorque(100f);

        RaycastHit2D raycast = Physics2D.Raycast(gameObject.transform.position, PlayerController.singleton.movement.normalized, 1f, 1 << 11);
        if (raycast.collider == null || raycast.collider.tag != "Screen Border")
        {
            body.AddForce((PlayerController.singleton.movement.normalized != Vector2.zero ? PlayerController.singleton.movement.normalized : defaultPower * (PlayerController.singleton.render.flipX ? -1 : 1)) * 2200f);
        }

        SfxController.singleton.beeBuzz.Play();

        Timing.CallDelayed(1f, () => Explode());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Screen Border")
        {
            body.velocity = Vector3.Reflect(lastFrameVelocity.normalized, collision.contacts[0].normal) * lastFrameVelocity.magnitude;
        }
    }

    void Explode()
    {
        Assets.Scripts.General.Utils.Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float dist = Vector2.Distance(gameObject.transform.position, obj.transform.position);
            if (dist < 3.5f)
            {
                Enemy controller = obj.GetComponent<Enemy>();
                if (controller != null)
                {
                    float damage = 1 / (transform.position - obj.transform.position).sqrMagnitude * damageScale;
                    controller.Damage(Mathf.Clamp(damage, 0f, maxDamage));
                }
            }
        }
        SfxController.singleton.explosion.Play();
        Destroy(gameObject);
    }
}
