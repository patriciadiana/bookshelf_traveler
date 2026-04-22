using System.Collections;
using UnityEngine;

public class ShowAlarm : MonoBehaviour
{
    public GameObject alarm;

    private void Start()
    {
        WaitToShowAlarm();

        alarm.SetActive(true);
    }

    IEnumerator WaitToShowAlarm()
    {
        yield return new WaitForSeconds(10);
    }
}
