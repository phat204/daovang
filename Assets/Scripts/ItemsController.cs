using UnityEngine;

public class ItemsController : MonoBehaviour
{
    private bool isMoveFollow;
    public float pullSpeed;
    public int score, explosive;
    public bool isBag;
    public GameObject parent;
    private HookController hookController;
    public GameObject explosionEffect;
    void Start()
    {
        isMoveFollow = false;
        if (isBag)
        {
            score = Random.Range(200, 501);
        }
        hookController = FindObjectOfType<HookController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == parent)
        {
            AudioManager.Instance.PlayKeoSound();
            isMoveFollow = true;
            hookController.isDropping = false;
            hookController.isPulling = true;
            hookController.pullSpeed = pullSpeed;
        }
    }

    void FixedUpdate()
    {
        MoveFollow(parent);
    }

    private void MoveFollow (GameObject target) {
        if (isMoveFollow)
        {
            transform.position = new Vector3(target.transform.position.x,
            target.transform.position.y - gameObject.GetComponent<Collider2D>().bounds.extents.y,
            target.transform.position.z);

            if (hookController.isThrowExplosive)
            {
                AudioManager.Instance.PlayNoMinSound();
                GameObject no = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(no, 0.3f);
                Destroy(gameObject);
                GameManager.Instance.explosive -= 1;
                hookController.pullSpeed = hookController.initialPullSpeed;
            }

            if (hookController.isSwaying)
            {
                AudioManager.Instance.PlayCongTienSound();
                Destroy(gameObject);
                GameManager.Instance.score += score;
                GameManager.Instance.explosive += explosive;
            }
        }
    }
}
