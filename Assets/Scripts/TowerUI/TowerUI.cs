using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{

    [SerializeField] private GameObject towerUI = null;
    [SerializeField] private GameObject upgradesPanel = null;
    [SerializeField] private GameObject upgradePrefab = null;

    private Tower _selectedTower = null;
    
    public Tower SelectedTower
    {
        set
        {
            _selectedTower = value;
            var upgradesTransform = upgradesPanel.transform;
            if (_selectedTower != null)
            {
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
        while (upgradesTransform.childCount > 1) // Destroy all upgrades buttons
        {
            Destroy(upgradesTransform.GetChild(1).gameObject);
            yield return null;
        }
        
        foreach (var level in _selectedTower.CurrLevel.GetNext())
        {
            UpgradeButton.Create(upgradePrefab, upgradesTransform, level, _selectedTower);
        }
    }
    
    public void CloseUI()
    {
        SelectedTower = null;
    }
}
