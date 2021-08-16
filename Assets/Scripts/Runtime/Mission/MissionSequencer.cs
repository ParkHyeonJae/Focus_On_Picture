using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MissionSequencer) + "@Default", menuName = nameof(MissionSequencer) + "/Create", order = 0)]
public class MissionSequencer : ScriptableObject
{
    public Mission this[byte sequence]
    {
        get
        {
            if (IsOutOfRange(sequence))
                Debug.LogException(new System.ArgumentOutOfRangeException());

            return missionList[sequence];
        }
    }


    public List<Mission> missionList;

    public Queue<Mission> missions = new Queue<Mission>();

    public Mission Peek()
        => missions.Count > 0 ? missions.Peek() : null;

    public Mission Pop()
        => missions.Count > 0 ? missions.Dequeue() : null;

    public bool IsOutOfRange(byte sequence)
        => (sequence < 0 || missionList.Count <= sequence);

    public void Init()
    {
        missions.Clear();

        foreach (var mission in missionList)
            missions.Enqueue(mission);
    }
}
