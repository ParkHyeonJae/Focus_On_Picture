using System;
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

    private HashSet<string> _missionTags = null;
    public HashSet<string> missionTags
        => _missionTags = _missionTags ?? Utils.GetGenTagsToHashSet();

    private XRSign[] _xrSigns;
    private sbyte _maxSignSequence;
    private sbyte _curSignSequence;

    public void Awake()
    {
        missionSequencer.Init();

        _xrSigns = FindObjectsOfType<XRSign>();
        Array.Sort(_xrSigns, new XRSignComparer());
        _maxSignSequence = (sbyte)_xrSigns.Length;
        _curSignSequence = 0;

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

    public GameObject GetTargetSignObject
        => _xrSigns[_curSignSequence].gameObject;

    public Transform GetTargetSignTransform
        => GetTargetSignObject.transform;

    /// <summary>
    /// 전체 미션 개수
    /// </summary>
    public int AllMissionCount
        => missionSequencer.missionList.Count;

    /// <summary>
    /// 현재 미션 진행도
    /// </summary>
    public int CurMissionProgress
        => sequence;

    public void MoveNext()
    {
        var mission = GetCurrentMission;
        if (mission == null)
            return;


        if (mission.maxShotCount == ++willClearSequence)
        {
            Mission prevMission = missionSequencer.Pop();
            mission = GetCurrentMission;
            willClearSequence = 0;

            sequence++;

            MoveNextMission(prevMission, mission);
        }


        if (!(mission is null))
        {
            SetRangeText(mission.missionText, willClearSequence, mission.maxShotCount); 
            return;
        }
        SetText(string.Empty);
    }

    private void MoveNextMission(Mission prevMission, Mission nextMission)
    {
        if (prevMission.goToNextSign && _curSignSequence < _maxSignSequence - 1)
            ++_curSignSequence;


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
