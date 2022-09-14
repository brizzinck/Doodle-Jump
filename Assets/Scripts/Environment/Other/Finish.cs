using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private GameLevelsUIController _UIController;
    private FinishController _finish;
    public FinishController finish { get => _finish; set => _finish = value; }
    public GameLevelsUIController UIController { get => _UIController; set => _UIController = value; }
    public Finish(FinishController finish)
    {
        _finish = finish;
    }
    
    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out Player player))
        {
            VibrateController.Vibrate();
            if (AudioController.Instance.GetMute() == "False") _audioSource.PlayOneShot(_audioSource.clip);
            _finish.DoEneble(true);
            _UIController.Finish();
            Saver.SaveIntPrefs("CurrentLevel", Saver.GetIntPrefs("CurrentLevel") + 1);
            Time.timeScale = 0;
        }
    }
}
