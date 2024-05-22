using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody = null;
    [SerializeField] AudioSource hitSound = null;
    bool audioPlayed;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            rigidbody.constraints = RigidbodyConstraints.None;
            if(hitSound != null & !audioPlayed)
            {
                audioPlayed = true;
                hitSound.Play();
            }
        }
    }
}
