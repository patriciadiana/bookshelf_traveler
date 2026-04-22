using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public void OnBackButton()
    {
        mainMenu.SetActive(true); 
        gameObject.SetActive(false);
    }
}