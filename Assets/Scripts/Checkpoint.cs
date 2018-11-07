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

    [SerializeField]
<<<<<<< HEAD
    private Color inactivedColor, activatedColor;

    private bool isActicated = false; //all bools start false/ inactive
    private void Update()
    {
        UpdateRotation();

=======
    private Color inactivatedColor, activatedColor;

    private bool isActivated = false;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        UpdateColor();
    }

    private void Update()
    {
        UpdateRotation();
    }

    public void SetIsActivated(bool value)
    {
        isActivated = value;
        UpdateScale();
        UpdateColor();
>>>>>>> 0efe9304b8e1eade769e26362fc846dc8f4a3af0
    }

    private void UpdateColor()
    {
<<<<<<< HEAD
        Color color = inactivedColor;
        if (isActicated)
            color = activatedColor;



=======
        Color color = inactivatedColor;
        if (isActivated)
            color = activatedColor;

        spriteRenderer.color = color;

        
>>>>>>> 0efe9304b8e1eade769e26362fc846dc8f4a3af0
    }
    private void UpdateScale()
    {
        float scale = inactivatedScale;
<<<<<<< HEAD
        if (isActicated)
            scale = activatedScale;

        transform.localScale = Vector3.one * scale;
    }

    private void UpdateRotation()
    {
        float rotationSpeed = inactivatedRotationSpeed;
        if (isActicated)
            rotationSpeed = activatedRotationSpeed;

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void SetIsActivated(bool value)
    {
        isActicated = value;
        UpdateScale();
    }

=======
        if (isActivated)
            scale = activatedScale;

        transform.localScale = Vector3.one *scale;
    }
    private void UpdateRotation()
    {
        float rotationSpeed = inactivatedRotationSpeed;
        if (isActivated)
            rotationSpeed = activatedRotationSpeed;

       transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
>>>>>>> 0efe9304b8e1eade769e26362fc846dc8f4a3af0
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
