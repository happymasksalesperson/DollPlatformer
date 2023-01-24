using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpriteAnimationController : MonoBehaviour
{
    // chatGPT wrote this for me :)
    
    private Dictionary<int, Sprite> animationFrames = new Dictionary<int, Sprite>();

    // change names laters
    public Sprite animationFrame1;
    public Sprite animationFrame2;
    public Sprite animationFrame3;
    public Sprite animationFrame4;
    public Sprite animationFrame5;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        animationFrames.Add(0, animationFrame1);
        animationFrames.Add(1, animationFrame2);
        animationFrames.Add(2, animationFrame3);
        animationFrames.Add(3, animationFrame4);
        animationFrames.Add(4, animationFrame5);
    }

    public void ChangeAnimationFrame(int frame)
    {
        if (animationFrames.ContainsKey(frame))
        {
            spriteRenderer.sprite = animationFrames[frame];
        }
    }
    
    public void PlayAnimation(int startFrame, int endFrame, float animationSpeed)
    {
        StartCoroutine(Animate(startFrame, endFrame, animationSpeed));
    }

    IEnumerator Animate(int startFrame, int endFrame, float animationSpeed)
    {
        for (int i = startFrame; i <= endFrame; i++)
        {
            ChangeAnimationFrame(i);
            yield return new WaitForSeconds(animationSpeed);
        }
    }
}