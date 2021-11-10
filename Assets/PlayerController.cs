using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public Transform endTransform;

    public ParticleSystem dirtParticle;

    public Animator myAnimator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Get inputs
        if (Input.GetKey(KeyCode.D))
        {
            rb2D.AddForce(new Vector2(1 * 50f, 0));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb2D.AddForce(new Vector2(1 * 10f, 0));
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            Vector3 currentEndPosition = endTransform.position;
            StartCoroutine(LerpToMouse(currentEndPosition, 1));
        }
        didCollideThisFrame = false;
    }

    IEnumerator LerpToMouse(Vector3 targetPosition, float lerpDuration)
    {
        float t = 0;
        Vector3 startPosition = transform.position;

        while (t < lerpDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, t / lerpDuration);
            t += Time.deltaTime;

            yield return null;
        }

        transform.position = targetPosition;
    }

    private bool didCollideThisFrame;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (didCollideThisFrame) return;
            if (rb2D.velocity.y > 2)
            {
                dirtParticle.transform.position = new Vector3(transform.position.x, -3, transform.position.z);
                dirtParticle.Play();

                myAnimator.Play("Bounce");
            }
            didCollideThisFrame = true;
        }
    }
}
