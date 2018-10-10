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
    private Color inactivedColor, activatedColor;

    private bool isActicated = false; //all bools start false/ inactive
    private void Update()
    {
        UpdateRotation();

    }

    private void UpdateColor()
    {
        Color color = inactivedColor;
        if (isActicated)
            color = activatedColor;



    }
    private void UpdateScale()
    {
        float scale = inactivatedScale;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit the checkpoint");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.SetCurrentCheckpoint(this); 
        }
    }
}
