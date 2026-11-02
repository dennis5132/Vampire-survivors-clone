using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastingUpgrades : MonoBehaviour
{
    private DataManager manager;
    [SerializeField] private TextMeshProUGUI txt;

    [SerializeField] private Button XPBtn;
    [SerializeField] private Button HealthBtn;

    [SerializeField] private int healthCost=5;
    [SerializeField] private int XPCost=1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = gameObject.GetComponent<DataManager>();
        XPBtn.GetComponentInChildren<TextMeshProUGUI>().text = "XP bonus: " + manager.SendData().XPLevel + " (" + XPCost + " Points)";
        HealthBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Health bonus: " + manager.SendData().HealthLevel + " (" + healthCost + " Point)";
        txt.text = "Points: "+ manager.SendData().upgradeCurrency;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickXPBtn()
    {
        Data data = manager.SendData();
        if (data.upgradeCurrency >= XPCost)
        {
            data.XPLevel++;
            data.upgradeCurrency -= XPCost;
            XPBtn.GetComponentInChildren<TextMeshProUGUI>().text = "XP bonus: " + data.XPLevel + " (" + XPCost + " Points)";
            Debug.Log(manager.SendData().upgradeCurrency);
        }
            manager.Receive(data);
        txt.text = "Points: " + manager.SendData().upgradeCurrency;
    }
    public void ClickHealthBtn()
    {
        Data data = manager.SendData();
        if (data.upgradeCurrency >= healthCost)
        {
            data.HealthLevel++;
            data.upgradeCurrency -= healthCost;
            HealthBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Health bonus: " + data.HealthLevel + " (" + healthCost + " Point)";
            Debug.Log(manager.SendData().upgradeCurrency);
        }
        manager.Receive(data);
        txt.text = "Points: " + manager.SendData().upgradeCurrency;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
