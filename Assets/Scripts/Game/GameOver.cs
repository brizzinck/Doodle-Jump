using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] CameraController _cameraController;
    [SerializeField] private CanvasGroup _gameOver;
    [SerializeField] private AudioClip[] _gameOverAudio;
    [SerializeField] private BaseGameUIController _gameUIController;
    private BoxCollider2D _playerCollider;
    private Rigidbody2D _playerRigidbody;
    public IEnumerator Continue(int cost)
    {
        int costContinue = Mathf.Clamp(cost, 10, 99);
        if (Saver.GetIntPrefs("Coins") >= costContinue)
        {
            _gameUIController.ControllPausebutton(true);
            AudioController.Instance.PlaySound(_gameOverAudio[1]);
            Saver.SaveIntPrefs("Coins", Saver.GetIntPrefs("Coins") - costContinue);
            Animations.DoMove(_gameOver.transform, new Vector3(0, -2100, 0));
            _player.gameObject.SetActive(true);
            _player.UpdateCoins();
            _playerRigidbody.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -1);
            yield return new WaitForSeconds(0.5f);
            _playerCollider.enabled = true;
            _player.SetLayer("Player");
        }       
    }
    public IEnumerator Fall()
    {
        AudioController.Instance.PlaySound(_gameOverAudio[0]);
        VibrateController.Vibrate();
        _gameUIController.DisplayInfoGameOver();
        _gameUIController.ControllPausebutton(false);
        _playerCollider.enabled = false;
        _cameraController.PlayerFall = true;
        yield return new WaitForSeconds(0.7f);
        Animations.DoMove(_gameOver.transform, new Vector3(0, 0, 0));
        _cameraController.PlayerFall = false;
        yield return new WaitForSeconds(0.2f);
        _player.gameObject.SetActive(false);
    }
    private void Start()
    {
        _playerCollider = _player.GetComponent<BoxCollider2D>();
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
    }
}
