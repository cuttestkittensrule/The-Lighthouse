using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Yarn.Unity;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private State state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static async void FadeIn(GameObject obj) {
        SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
        double fadeAmount = 0.1;
        const int fadeDelay = 500;
        while (sprite.color.a > 0) {
            await Task.Delay(fadeDelay);
            var color = sprite.color;
            if (color.a > fadeAmount) {
                fadeAmount = color.a;
            }
            sprite.color = new Color(color.r, color.g, color.b,  color.a - (float)fadeAmount);

        }
    }
    [YarnCommand("FadeIn")]
    public static void yrnFadeIn() {
        GameObject obj = GameObject.FindGameObjectWithTag("Cover");
        Thread t = new(new ThreadStart(() => { FadeIn(obj); }));
        t.Start();
    }


    [Serializable]
    private class State {
        [SerializeField]
        [ReadOnly]
        private bool canMove = true;
        public bool CanMove { get; private set; }
    }
}
