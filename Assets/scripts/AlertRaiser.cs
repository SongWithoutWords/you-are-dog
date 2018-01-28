using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionDispatcher), typeof(AudioSource))]
public class AlertRaiser : MonoBehaviour
{
    [System.Serializable]
    public struct AlertThreshold
    {
        public float collisionThreshold;
        public float alertLevelIncrease;
        public AudioClip audioClip;
    }

    public List<AlertThreshold> thresholds;

    void Start()
    {
        var cd = gameObject.GetComponent<CollisionDispatcher>();
        if (cd != null)
        {
            cd.OnCollisionEnter += OnCollision;
        }

        // Sort by descending collision threshold, so we pick most extreme first
        thresholds.Sort((a, b) =>
        {
            return
                a.collisionThreshold > b.collisionThreshold ? -1 :
                a.collisionThreshold < b.collisionThreshold ? +1 :
                0;
        });
    }

    void OnCollision(Collision2D collision, float acceleration)
    {
        foreach (var threshold in thresholds)
        {
            if (threshold.collisionThreshold < acceleration)
            {
                {
                    var audioSource = GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.PlayOneShot(threshold.audioClip, 1);
                    }
                }
                {
                    var restaurant = FindObjectOfType<RestaurantState>();
                    if (restaurant != null && collision.gameObject.GetComponent<PlayerInput>() != null)
                    {
                        restaurant.AddAlert(threshold.alertLevelIncrease);
                    }
                }
                break;
            }
        }
    }
}
