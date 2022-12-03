using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnyStateAnimator : MonoBehaviour
{
    private Animator animator;

    private Dictionary<string, AnyStateAnimation> animations = new Dictionary<string, AnyStateAnimation>();

    private string currentAnimation = string.Empty;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Animate();
    }
    public void AddAnimationts(params AnyStateAnimation[] newAnimatons)
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
}
