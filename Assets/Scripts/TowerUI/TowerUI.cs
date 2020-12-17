using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{

    [SerializeField] private GameObject towerUI = null;
    [SerializeField] private GameObject upgradesPanel = null;
    [SerializeField] private TMP_Text sellText = null;
    [SerializeField] private GameObject upgradePrefab = null;
    
    private Tower _selectedTower = null;

    public Tower SelectedTower
    {
        get => _selectedTower;
        set
        {
            if (_selectedTower) _selectedTower.RangeShown(false);
            _selectedTower = value;
            var upgradesTransform = upgradesPanel.transform;
            if (_selectedTower != null)
            {
                _selectedTower.RangeShown(true);
                StartCoroutine(RefreshUI(upgradesTransform));
                towerUI.SetActive(true);
            }
            else
            {
                towerUI.SetActive(false);
            }
        }
    }
    
    private void Start()
    {
        Game.TowerUI = this;
        towerUI.SetActive(false);
    }

    private IEnumerator RefreshUI(Transform upgradesTransform)
    {
        while (upgradesTransform.childCount > 0) // Destroy all upgrades buttons
        {
            Destroy(upgradesTransform.GetChild(0).gameObject);
            yield return null;
        }

        sellText.text = "Sell: $ " + SelectedTower.CurrLevel.sellPrice;
        
        foreach (var level in SelectedTower.CurrLevel.GetNext())
        {
            UpgradeButton.Create(upgradePrefab, upgradesTransform, level, _selectedTower);
        }
    }

    public void SellTower()
    {
        Game.Player.Money += SelectedTower.CurrLevel.sellPrice;
        Destroy(SelectedTower.gameObject);
        SelectedTower = null;
    }
    
    public void CloseUI()
    {
        SelectedTower = null;
    }
}
