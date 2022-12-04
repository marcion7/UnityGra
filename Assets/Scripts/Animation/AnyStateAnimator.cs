using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void AnimationTriggerEvent(string animation);

public class AnyStateAnimator : MonoBehaviour
{
    private Animator animator;

    private Dictionary<string, AnyStateAnimation> animations = new Dictionary<string, AnyStateAnimation>();

    public AnimationTriggerEvent AnimationTriggerEvent { get; set; }

    private string currentAnimation = string.Empty;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Animate();
    }
    public void AddAnimations(params AnyStateAnimation[] newAnimatons)
    {
        for (int i = 0; i < newAnimatons.Length; i++)
        {
            this.animations.Add(newAnimatons[i].Name, newAnimatons[i]);
        }
    }

    public void TryPlayAnimation(string newAnimation) {
        if (currentAnimation == "")
        {
            animations[newAnimation].Active = true;
            currentAnimation = newAnimation;
        }
       else if (currentAnimation != newAnimation && !animations[newAnimation].HigherPrio.Contains(currentAnimation) || !animations[currentAnimation].Active)
       {
            animations[currentAnimation].Active = false;
            animations[newAnimation].Active = true;
            currentAnimation = newAnimation;
       }
    }

    public void SetWeapon(float weapon)
    {
        animator.SetFloat("Weapon", weapon);
    }

    private void Animate()
    {
        foreach (string key in animations.Keys)
        {
            animator.SetBool(key, animations[key].Active);
        }
    }
    public void OnAnimationDone(string animation)
    {
        animations[animation].Active = false;
    }

    public void OnAnimatorTrigger(string animation)
    {
        if(AnimationTriggerEvent != null)
        {
            AnimationTriggerEvent.Invoke(animation);
        }
    }
}
