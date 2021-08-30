using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

                if (_instance == null)
                    _instance = new GameObject($"@{nameof(UIManager)}").AddComponent<UIManager>();

            }
            return _instance;
        }
    }

    [Header("Mission Popup")]
    [SerializeField] public CanvasGroup _mission;
    [SerializeField] public Text _content;
    [SerializeField] public Text _counter;

    [Header("Interaction")]
    [SerializeField] public CanvasGroup _interaction;
    [SerializeField] public Image _fill;

    [Header("MissionGauge")]
    [SerializeField] public TextMeshProUGUI _allMission;
    [SerializeField] public TextMeshProUGUI _curMission;

    [Header("Billboard")]
    [SerializeField] public Image _compass;
    [SerializeField] public TextMeshProUGUI _distance;
    [SerializeField] public RectTransform _gauge;
    [SerializeField] public Image _gaugeFill;

    public void Update()
    {
        #region Billboard
        _compass.rectTransform.LookAt(Camera.main.transform);
        _compass.rectTransform.Rotate(new Vector3(0, 180));
        _distance.rectTransform.LookAt(Camera.main.transform);
        _distance.rectTransform.Rotate(new Vector3(0, 180));
        _gauge.LookAt(Camera.main.transform);
        _gauge.Rotate(new Vector3(0, 180));
        #endregion

        int _allMissionValue = MissionManager.Instance.AllMissionCount;
        int _curMissionValue = MissionManager.Instance.CurMissionProgress;

        //거리 UI 세팅
        float distance = Vector3.Distance(Camera.main.transform.position, MissionManager.Instance.GetTargetSignTransform.position);
        _distance.text = distance.ToString("N0") + "M";

        //방향 UI 세팅
        Vector3 dir = MissionManager.Instance.GetTargetSignTransform.position - Camera.main.transform.position;
        float rotateDegree = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        _compass.transform.rotation = Quaternion.Euler(_compass.rectTransform.rotation.x, _compass.transform.rotation.y, -rotateDegree);

        //미션 게이지 UI 세팅
        _allMission.text = _allMissionValue.ToString();
        _curMission.text = _curMissionValue.ToString();
        _gaugeFill.fillAmount = Mathf.Lerp(0.35f, 1, (float)_curMissionValue / (float)_allMissionValue);
    }

    public void OnInteraction(System.Action onInteractCompletedCallback = null)
    {
        float frameDuration = 0.3f;
        float duration = 1.7f;

        _interaction.DOFade(1, frameDuration).OnComplete(() => _fill.DOFillAmount(1, duration).OnComplete(() => onInteractCompletedCallback?.Invoke()));
    }

    public void OffInteraction()
    {
        _interaction.DOKill();
        _fill.DOKill();
        _interaction.DOFade(0, 0.5f);
        _fill.DOFillAmount(0, 0.5f);
    }

    public void OnMissionPopup(bool bisActive, float duration = 1)
    {
        string content = MissionManager.Instance.GetCurrentMission?.missionText;
        _content.text = content;

        string counter = MissionManager.Instance.CurMissionProgress.ToString() + 
            "/" + MissionManager.Instance.AllMissionCount.ToString();
        _counter.text = counter;

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
