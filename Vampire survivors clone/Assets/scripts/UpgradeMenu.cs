using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Button[] btn = new Button[3];
    private GameObject menu;
    private int[] pickables = new int[3] {-1,-1,-1 };

    private int currency = 0;
    [SerializeField] private int levelCost = 40;
    [SerializeField] private float levelCostIncrease= 1.2f;
    private int pendingUpgrades;
    private int NextLevel()
    {
        int level = 0;
        foreach (int i in player.weaponLevels) 
        {
            level += i;
        }
        Debug.Log($"level: {player.weaponLevels[0]}, {levelCost * Mathf.Pow(levelCostIncrease, level)}");
        return (int) (levelCost*Mathf.Pow(levelCostIncrease, level));
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu = transform.GetChild(0).gameObject;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasReleasedThisFrame && menu.activeSelf == false)
        {
            Debug.Log("upgrade");
            NewUpgrade();
        }
    }
    private void NewUpgrade()
    {
        if (pendingUpgrades < 1)
        { return; }
        Time.timeScale = 0;
        pendingUpgrades--;
        pickables = new int[3] { -1, -1, -1 };
        menu.SetActive(true);
        for (int i = 0; i < pickables.Length; i++) 
        {
            int x = -1;
            while (pickables[i] == -1 && pickables.Contains(x)) 
            {
                x = Random.Range(0,player.weapons_.Length);
            }
            pickables[i] = x;
            btn[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = player.weapons_[x].Name +" to level "+ (player.weaponLevels[x]+1);
            btn[i].onClick.AddListener(() => selectWeapon(x));
        }
        
    }
    private void selectWeapon(int x)
    {
        player.weaponLevels[x]++;
        Debug.Log(player.weaponLevels[x]);
        menu.SetActive(false);
        Time.timeScale = 1;
        foreach (Button btn in btn)
        {
            btn.onClick.RemoveAllListeners();
        }
        NewUpgrade();
    }
    public void AddCurrency()
    {
        currency++;
        if (currency >= NextLevel()) 
        {
            pendingUpgrades++;
            NewUpgrade();
        }
        
    }
}
