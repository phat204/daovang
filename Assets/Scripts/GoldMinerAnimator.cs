using UnityEngine;

public class GoldMinerAnimator : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ThaMoc() {
        animator.SetBool("tha_moc", true);
    }

    public void KeoMoc() {
        animator.SetBool("tha_moc", false);
        animator.SetBool("keo_moc", true);
    }

    public void Stop() {
        animator.SetBool("keo_moc", false);
    }

    public void NemThuocNo() {
        animator.SetTrigger("nem_thuoc_no");
    }
}
