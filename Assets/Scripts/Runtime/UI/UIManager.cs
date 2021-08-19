using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Mission Popup")]
    [SerializeField] private CanvasGroup _mission;
    [SerializeField] private Text _content;
    [SerializeField] private Text _counter;

    [Header("Interaction")]
    [SerializeField] private CanvasGroup _interaction;
    [SerializeField] private Image _fill;

    public IEnumerator FillInteraction()
    {
        float delay = 0.1f;
        float frameDuration = 0.3f;
        float duration = 1.7f;

        if (_interaction.alpha <= 1)
            _interaction.alpha += (1 * delay) / frameDuration;
        else if (_fill.fillAmount <= 1)
            _fill.fillAmount += (1 * delay) / duration;

        if (_fill.fillAmount >= 1)
            yield return null;
        else
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(FillInteraction());
        }
    }

    public void OffInteraction()
    {
        _interaction.alpha = 0;
        _fill.fillAmount = 0;
    }

    public void OnMissionPopup(bool bisActive, float duration = 1)
    {
        string content = MissionManager.Instance.GetCurrentMission.missionText;
        _content.text = content;

        StartCoroutine(OnActiveMission(bisActive, duration));
    }

    public IEnumerator OnActiveMission(bool bisActive, float duration)
    {
        float delay = 0.1f;

        _mission.alpha += (1 * delay) / duration * (bisActive ? 1 : -1);

        if (!(_mission.alpha >= 1 || _mission.alpha <= 0))
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(OnActiveMission(bisActive, duration));
        }
        else
            yield return null;
    }
}
