using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerFx : MonoBehaviour
{
    void LateUpdate()
    {
        transform.position = Player.Instance.transform.position;
    }
}
