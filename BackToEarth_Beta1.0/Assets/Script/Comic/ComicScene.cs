using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicScene : MonoBehaviour {

    public float playTime;
    [HideInInspector]
    public TweenAlpha tween;

    void Awake()
    {
        tween = this.GetComponent<TweenAlpha>();
    }
}
