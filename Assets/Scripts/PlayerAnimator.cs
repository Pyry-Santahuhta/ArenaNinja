using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;

    int velocityHash;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        velocityHash = Animator.StringToHash("Velocity");

    }
    /*
    const float locomationAnimationSmoothTime = .1f;
    NavMeshAgent agent;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();


    }
*/
    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);

        if (inputDir != Vector2.zero)
        {
            velocity = 0.5f;
            if (running)
            {
                velocity = 1f;
            }
        }
        else
        {
            velocity = 0;
        }
        animator.SetFloat(velocityHash, velocity);
    }
}
