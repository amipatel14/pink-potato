using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float inactivatedRotationSpeed = 100, activatedRotationSpeed = 300;

    [SerializeField]
    private float inactivatedScale = 1, activatedScale = 1.5f;

    private bool isActivated = false;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private void Update()
    {
        UpdateRotation();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetIsActivated(bool value)
    {
        isActivated = value;
        UpdateScale();
    }

    private void UpdateScale()
    {
        float scale = inactivatedScale;

        if (isActivated)
            scale = activatedScale;

        transform.localScale = Vector3.one * scale;
    }

    private void UpdateRotation()
    {
        float rotationSpeed = inactivatedRotationSpeed;
        if (isActivated)
            rotationSpeed = activatedRotationSpeed;

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
            Debug.Log("Player hit the checkpoint");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.SetCurrentCheckpoint(this);
            audioSource.Play();

        }
    }
}

