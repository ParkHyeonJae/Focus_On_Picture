using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(Mission) + "@Object", menuName = nameof(Mission) + "/Create", order = 0)]
public class Mission : ScriptableObject
{
    [Header("미션 내용")]
    [Multiline]
    public string missionText;

    [Header("찍을 사진 개수")]
    [Range(1, 256)]
    public byte maxShotCount = 1;

    public string targetTag
        => tag.ToString();
    public GenEnums.Tags tag;

    public bool goToNextSign = false;

}
