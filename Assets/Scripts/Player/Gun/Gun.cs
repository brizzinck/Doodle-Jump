using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Camera _camera;
    [SerializeField] private SpriteRenderer _gunRender;
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletDir;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0 && _player.CurrentBoost == null)
        {
            if ((Input.mousePosition.y > _camera.pixelHeight - _camera.pixelHeight / 10) || _player.IsShoot) return;
            StartCoroutine(Shoot(Input.mousePosition));
        }
    }
    private IEnumerator Shoot(Vector3 positionShoot)
    {
        AudioController.Instance.PlaySound(_shootClip);
        _player.IsShoot = true;
        _player.SootAnim();
        _gunRender.color = new Color(1, 1, 1, 1);
        Vector3 diff = _camera.ScreenToWorldPoint(positionShoot) - transform.position;
        diff.Normalize();
        if (diff.x < 0) diff.y = Mathf.Abs(diff.y);
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rot_z -= 90;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(rot_z, -45, 45));
        Instantiate(_bullet, _bulletDir.position, transform.rotation);     
        yield return new WaitForSeconds(0.5f);
        _gunRender.color = new Color(1, 1, 1, 0);
        _player.IsShoot = false;
        _player.IdelAnim();
    }
}
