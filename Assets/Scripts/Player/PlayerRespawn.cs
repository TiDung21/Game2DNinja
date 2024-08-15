using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform curCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();  
        uiManager = FindObjectOfType<UIManager>();

    }

    private void CheckRespawn()
    {
        if(curCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }
        transform.position = curCheckpoint.position;
        playerHealth.RespawnPlayer();
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(curCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            curCheckpoint = collision.transform;

            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
