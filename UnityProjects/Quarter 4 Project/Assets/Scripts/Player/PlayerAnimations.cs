using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private GameObject player;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().RunActive == true)
        {
            anim.SetTrigger("Running");
        }
        if (player.GetComponent<PlayerController>().AttackActive == true)
        {
            anim.SetTrigger("AttackTrigger");
        }
        if (player.GetComponent<PlayerController>().JumpActive == true)
        {
            anim.SetTrigger("Jumped");
        }
        if (player.GetComponent<PlayerController>().DashActive == true)
        {
            anim.SetTrigger("Dashed");
        }
        if (player.GetComponent<PlayerController>().CrouchActive == true)
        {
            anim.SetTrigger("Crouching");
        }
        if (player.GetComponent<PlayerController>().isGrounded == true)
        {
            anim.SetTrigger("Hit ground");
        }
        if (player.GetComponent<PlayerController>().RunActive == false)
        {
            anim.SetTrigger("StoppedRunning");
        }
        if (player.GetComponent<PlayerController>().isGrounded == false)
        {
            anim.SetTrigger
        }
    }
}
