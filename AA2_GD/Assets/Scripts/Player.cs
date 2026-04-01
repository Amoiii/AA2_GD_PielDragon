using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerHitbox _playerHitbox;
    private PlayerShooting _playerShooting;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHitbox = GetComponent<PlayerHitbox>();
        _playerShooting = GetComponent<PlayerShooting>();
    }

    public void SetPlayerState(bool state)
    {
        _playerMovement.isActive = state;
        _playerHitbox.isActive = state;
        _playerShooting.isActive = state;
    }
}
