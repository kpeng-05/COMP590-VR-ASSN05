using System;
using UnityEngine;

public class BladeCollapse : MonoBehaviour
{
    public float speed = 0.1f;
    bool isOn;
    float minScale = 0;
    float maxScale;
    float lerp;
    float scale;
    float xScale;
    float yScale;   
    public GameObject blade;

    public AudioClip lightsaberOn;
    public AudioClip lightsaberOff;
    public AudioSource activationAudio;
    public AudioSource idleAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xScale = transform.localScale.x;
        yScale = transform.localScale.z;
        isOn = true;
        maxScale = transform.localScale.y;
        scale = maxScale;
        lerp = maxScale / speed;
        idleAudio.volume = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lerp = isOn ? -Math.Abs(lerp) : Math.Abs(lerp);
            if (!isOn)
            {
                activationAudio.PlayOneShot(lightsaberOn);
            }
            else
            {
                activationAudio.PlayOneShot(lightsaberOff);
            }
        }

        if (isOn && !activationAudio.isPlaying && !idleAudio.isPlaying)
        {
            idleAudio.Play();
        }
        else if (!isOn)
        {
            idleAudio.Stop();
        }

        scale += lerp * Time.deltaTime;
        scale = Math.Clamp(scale, minScale, maxScale);
        transform.localScale = new Vector3(xScale, scale, yScale);
        isOn = scale > 0;
        if (isOn && !blade.activeSelf)
        {
            blade.SetActive(true);
        }
        if (!isOn && blade.activeSelf)
        {
            blade.SetActive(false);
        }
    }
}
