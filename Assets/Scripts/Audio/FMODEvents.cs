using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference testSFXSound2 { get; private set; }

    [field: Header("SFX")]
    [field: SerializeField] public EventReference testSFXSound { get; private set; }
    [field: SerializeField] public EventReference testDistance { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("BGM")]
    [field: SerializeField] public EventReference gameBGM { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found another FMODEvents in this scene.");
        }
        instance = this;
    }
}