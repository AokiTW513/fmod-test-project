using UnityEngine;

public class SoundTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.testSFXSound, this.transform.position);
        }       
    }
}