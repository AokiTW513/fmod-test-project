using FMOD.Studio;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction move;
    private float speed = 4f;
    private EventInstance testSFXSound2;
    private GameObject canInteractableObject;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions["Move"];
    }

    private void Start()
    {
        testSFXSound2 = AudioManager.instance.CreateInstance(FMODEvents.instance.testSFXSound2);
        transform.position = SaveManager.instance.currentGameData.player.playerPosition;
    }

    private void Update()
    {
        if (GameManager.instance.isPause) return;
        PauseButton();
        Movement();
        UpdateSound();
        ToggleMusic();
    }

    private void PauseButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.TogglePause();
        }
    }

    private void Movement()
    {
        transform.position += Vector3.right * move.ReadValue<Vector2>().x * speed * Time.deltaTime;
    }

    private void UpdateSound()
    {
        if (move.ReadValue<Vector2>().x != 0)
        {
            PLAYBACK_STATE playBackState;
            testSFXSound2.getPlaybackState(out playBackState);
            if (playBackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                testSFXSound2.start();
            }
        }
        else
        {
            testSFXSound2.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void ToggleMusic()
    {
        if (Input.GetKeyDown(KeyCode.F) && canInteractableObject != null)
        {
            //if can interactable gameobject is radio then turn on/off it
            if (canInteractableObject.gameObject.tag == "Radio")
            {
                canInteractableObject.GetComponent<Radio>().OnSwitch();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Radio")
        {
            canInteractableObject = collision.gameObject;
            canInteractableObject.GetComponent<Radio>().ShowInteractionPromptUI(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Radio")
        {
            canInteractableObject.GetComponent<Radio>().ShowInteractionPromptUI(false);
            canInteractableObject = null;
        }
    }
}