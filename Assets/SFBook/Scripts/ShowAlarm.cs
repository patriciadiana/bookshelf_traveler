using System.Collections;
using UnityEngine;

public class ShowAlarm : MonoBehaviour
{
    public GameObject alarm;

    private void Start()
    {
        alarm.SetActive(false);
        StartCoroutine(WaitToShowAlarm());
    }

    IEnumerator WaitToShowAlarm()
    {
        yield return new WaitForSeconds(4);

        alarm.SetActive(true);

        SoundManager.Instance.PlayLoopSound(SoundType.SF_ALARM);
    }
}
