using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomMenuManager : MonoBehaviour
{
    public GameObject CompanyMenuContainerPanel;

    private bool isCompanyMenuOpen = false;
    private bool isUpgradesMenuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompanyButtonOnClick()
    {
        if(isCompanyMenuOpen == true) // Hide Menu
        {
            HideCompanyMenu();
            isCompanyMenuOpen = false;
        }
        else // Show Menu
        {
            ShowCompanyMenu();
            isCompanyMenuOpen = true;
        }
    }

    public void UpgradesButtonOnClick()
    {
        if (isUpgradesMenuOpen == true) // Hide Menu
        {
            HideUpgradesMenu();
            isUpgradesMenuOpen = false;
        }
        else // Show Menu
        {
            ShowUpgradesMenu();
            isUpgradesMenuOpen = true;
        }
    }

    void ShowCompanyMenu()
    {
        RectTransform menuRectTransform = CompanyMenuContainerPanel.GetComponent<RectTransform>();
        Vector3 menuPosition = menuRectTransform.position;
        menuPosition.y += menuRectTransform.rect.height + this.GetComponent<RectTransform>().rect.height;
        CompanyMenuContainerPanel.GetComponent<RectTransform>().SetPositionAndRotation(menuPosition, Quaternion.identity);
    }

    void HideCompanyMenu()
    {
        RectTransform menuRectTransform = CompanyMenuContainerPanel.GetComponent<RectTransform>();
        Vector3 menuPosition = menuRectTransform.position;
        menuPosition.y -= menuRectTransform.rect.height + this.GetComponent<RectTransform>().rect.height;
        CompanyMenuContainerPanel.GetComponent<RectTransform>().SetPositionAndRotation(menuPosition, Quaternion.identity);
    }

    void ShowUpgradesMenu()
    {

    }

    void HideUpgradesMenu()
    {

    }
}
