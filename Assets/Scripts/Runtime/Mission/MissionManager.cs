using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    private static MissionManager _instance;
    public static MissionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MissionManager>();

                if (_instance == null)
                    _instance = new GameObject($"@{nameof(MissionManager)}").AddComponent<MissionManager>();

            }
            return _instance;
        }
    }

    public TextMeshProUGUI missionText;
    public MissionSequencer missionSequencer;

    public byte sequence = 0;

    public byte willClearSequence;

    public void Awake()
    {
        missionSequencer.Init();

        SetRangeText(GetCurrentMission.missionText, willClearSequence, GetCurrentMission.maxShotCount);
    }

    public Mission GetCurrentMission
        => missionSequencer.Peek();

    public GameObject[] GetTargets
        => GameObject.FindGameObjectsWithTag(GetCurrentMission.targetTag);

    public GameObject GetTargetObject
        => GetTargets[willClearSequence];

    public Transform GetTargetTransform
        => GetTargetObject.transform;

    public void MoveNextMission()
    {
        var mission = GetCurrentMission;
        if (mission == null)
            return;

        ++willClearSequence;

        if (mission.maxShotCount == willClearSequence)
        {
            missionSequencer.Pop();
            mission = GetCurrentMission;
            willClearSequence = 0;
            sequence++;
        }

        if (mission == null) SetText(string.Empty);
        else SetRangeText(mission.missionText, willClearSequence, mission.maxShotCount);
    }

    private void SetRangeText(string text, byte cur, byte end)
    {
        if (missionText is null)
            return;
        missionText.text = $"{text} [{cur}/{end}]";
    }


    public void SetText(string text)
    {
        if (missionText is null)
            return;
        missionText.text = text;
    }
}
