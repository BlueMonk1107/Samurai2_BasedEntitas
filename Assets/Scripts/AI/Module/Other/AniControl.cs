using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AniControl : MonoBehaviour
{

    private Animation _ani;

    public void Init(Vector3 position)
    {
        transform.position = position;
        _ani = GetComponent<Animation>();
        gameObject.SetActive(false);
    }

    public AnimationClip GetClip()
    {
        return _ani.clip;
    }

    public async void Play()
    {
        gameObject.SetActive(true);

        _ani.Play(GetClip().name);

        await Task.Delay(TimeSpan.FromSeconds(GetClip().length));

        Destroy(gameObject);
    }
}
