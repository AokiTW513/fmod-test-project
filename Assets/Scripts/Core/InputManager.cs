using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public InputActionAsset inputActions;
    public string actionName = "Jump";

    public enum DeviceType
    {
        KeyboardMouse,
        Xbox,
        PlayStation,
        OtherGamepad,
        Unknown
    }

    public DeviceType currentDevice { get; private set; } = DeviceType.Unknown;
    public event Action<DeviceType> OnDeviceChanged;

    public InputAction anyInputAction;


    private void Start()
    {
        if (instance != null)
        {
            Debug.Log("Found another GameManager in this scene.");
            return;
        }

        instance = this;

        if (anyInputAction == null)
        {
            Debug.LogError("InputManager: anyInputAction 尚未設定！");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        PrintCurrentBinging();
    }

    private void OnEnable()
    {
        anyInputAction.Enable();
        anyInputAction.performed += OnAnyInputPerformed;
    }

    private void OnDisable()
    {
        anyInputAction.performed -= OnAnyInputPerformed;
        anyInputAction.Disable();
    }

    private void OnAnyInputPerformed(InputAction.CallbackContext context)
    {
        var device = context.control.device;

        var newDeviceType = GetDeviceType(device);

        if (newDeviceType != currentDevice)
        {
            currentDevice = newDeviceType;
            Debug.Log($"InputManager偵測新裝置: {currentDevice} ({device.displayName})");
            OnDeviceChanged?.Invoke(currentDevice);
        }
    }

    private DeviceType GetDeviceType(InputDevice device)
    {
        if (device is Gamepad gamepad)
        {
            // 可依廠牌判斷：用名稱判斷 Xbox / PS
            string name = device.displayName.ToLower();

            Debug.Log(name);

            if (name.Contains("xbox"))
                return DeviceType.Xbox;
            else if (name.Contains("playstation") || name.Contains("ps4") || name.Contains("ps5") || name.Contains("dualshock") || name.Contains("dualsense"))
                return DeviceType.PlayStation;
            else
                return DeviceType.OtherGamepad;
        }
        else if (device is Keyboard || device is Mouse)
        {
            return DeviceType.KeyboardMouse;
        }
        else
        {
            return DeviceType.Unknown;
        }
    }

    private void PrintCurrentBinging()
    {
        InputAction inputAction = inputActions.FindAction(actionName);
        if (inputAction == null)
        {
            Debug.LogError("Can't find action name");
            return;
        }

        // Debug.Log($"Control Device: {currentDevice}");

        foreach (var binding in inputAction.bindings)
        {
            if (binding.isComposite || binding.isPartOfComposite)
            {
                continue;
            }

            //Debug.Log($"Action '{inputAction.name}' Bind:{binding.path} Display Name:{InputControlPath.ToHumanReadableString(binding.path, InputControlPath.HumanReadableStringOptions.OmitDevice)}");
        }
    }
}