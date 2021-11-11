using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public Transform endTransform;

    public ParticleSystem dirtParticle;

    public Animator myAnimator;

    public AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb2D.AddForce(new Vector2(1 * 10f, 0));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb2D.AddForce(new Vector2(-1 * 10f, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 currentEndPosition = endTransform.position;
            StartCoroutine(CurveLerpToMouse(transform.position, endTransform.position, 1));
        }
        didCollideThisFrame = false;
    }

    IEnumerator LerpToMouse(Vector3 startPosition, Vector3 targetPosition, float lerpDuration)
    {
        //time elapsed
        float t = 0;

        //while time elapsed is lower than intended duration
        while (t < lerpDuration)
        {
            //interpolate per frame from start position to end position
            transform.position = Vector3.Lerp(startPosition, targetPosition, t / lerpDuration);
            //increase elapsed time
            t += Time.deltaTime;

            //when while loop is done, return
            yield return null;
        }
    }

    IEnumerator CurveLerpToMouse(Vector3 startPosition, Vector3 targetPosition, float lerpDuration)
    {
        float t = 0;

        while (t < lerpDuration)
        {
            //curve at which interpolation will be multiplied
            float factor = t / lerpDuration;
            //evaluate our curve
            // factor = curve.Evaluate(t);

            //apply an ease calculation - Smoothstep
            factor = factor * factor * (3f - 2f * factor);

            transform.position = Vector3.Lerp(startPosition, targetPosition, factor);
            t += Time.deltaTime;

            yield return null;
        }
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
