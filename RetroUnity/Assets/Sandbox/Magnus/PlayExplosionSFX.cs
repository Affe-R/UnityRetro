using UnityEngine;

public class PlayExplosionSFX : MonoBehaviour
{

    public AudioClip clip;  // assigned in the inspector

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, 0));
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, 3.0f);      // Destroyes explosion obj after 3 seconds.
    }

}
