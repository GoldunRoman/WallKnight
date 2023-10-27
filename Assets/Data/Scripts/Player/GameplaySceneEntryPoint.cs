using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneEntryPoint : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement.Initialize();
    }
}
