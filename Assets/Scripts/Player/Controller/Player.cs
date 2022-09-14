using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Sprite _jumpSprite;
    [SerializeField] private Sprite _shotSprite;
    [SerializeField] private Sprite _shotSpriteJump;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private Image _playerImage;
    [SerializeField] private Text _coins;
    private bool _isShoot = false;
    private Boost _currentBoost;
    private Rigidbody2D _playerRB;
    public Boost CurrentBoost { get => _currentBoost; set => _currentBoost = value; }
    public bool IsShoot { get => _isShoot; set => _isShoot = value; }
    public Rigidbody2D PlayerRigidbody2D { get => _playerRB; }
    public SpriteRenderer PlayerSpriteRenderer { get => _playerSpriteRenderer; }
    public Image PlayerImage { get => _playerImage; }
    public void UpdateCoins()
    {
        _coins.text = "coins: " + Saver.GetIntPrefs("Coins").ToString();
    }
    public IEnumerator FullJumpAnim(float jumpAnimTime = 0.2f)
    {
        JumpAnim();
        yield return new WaitForSeconds(jumpAnimTime);
        IdelAnim();
    }
    public void SetSprites(PlayerSkin playerSkin)
    {
        _idleSprite = playerSkin.IdelSprite;
        _jumpSprite = playerSkin.JumpSprite;
        _shotSprite = playerSkin.ShotSprite;
        _shotSpriteJump = playerSkin.ShotSpriteJump;
    }
    public void JumpAnim()
    {
        if (_playerSpriteRenderer != null && _isShoot == false) _playerSpriteRenderer.sprite = _jumpSprite;
        else if (_playerSpriteRenderer != null && _isShoot) _playerSpriteRenderer.sprite = _shotSpriteJump;
        else if (_playerImage != null && _isShoot == false) _playerImage.sprite = _jumpSprite;
        else if (_playerImage != null && _isShoot) _playerImage.sprite = _shotSpriteJump;
    }
    public void IdelAnim()
    {
        if (_playerSpriteRenderer != null && _isShoot == false) _playerSpriteRenderer.sprite = _idleSprite;
        else if (_playerSpriteRenderer != null && _isShoot) _playerSpriteRenderer.sprite = _shotSprite;
        else if (_playerImage != null && _isShoot == false) _playerImage.sprite = _idleSprite;
        else if (_playerImage != null && _isShoot) _playerImage.sprite = _shotSprite;
    }
    public void SootAnim()
    {
        if (_playerSpriteRenderer != null) _playerSpriteRenderer.sprite = _shotSprite;
        else if (_playerImage != null) _playerImage.sprite = _shotSprite;
    }
    public void SetLayer(string layer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
    }
    private IEnumerator Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        if (_coins != null) UpdateCoins();
        yield return null;
        IdelAnim();
    }
}
