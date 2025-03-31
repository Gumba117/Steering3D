using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CentralTowerType
{
    Ally,
    Enemy,
    Neutral
}
public class CentralTowerController : MonoBehaviour
{
    private int _enemiesCount;
    private int _allysCount;
    public TowerType towerType = TowerType.Neutral;
    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private float _conquerTime = 30f;
    [SerializeField] private float _activeTime = 60f;
    private float _enemyTowerTime;
    private float _allyTowerTime;

    private float _activeCentralTowerTime;

    private bool _isActive = false;

    [SerializeField] private TowerController[] _towers;

    public TextMeshProUGUI enemyCountTxt;
    public TextMeshProUGUI allyCountTxt;

    private void Update()
    {
        if (_isActive == false) 
        {
            ActivateCentralTower();
            return;
        }

        switch (towerType)
        {
            case TowerType.Ally:
                _meshRenderer.material.color = Color.blue;
                if (_enemiesCount > (_allysCount*2))
                {
                    _enemyTowerTime += Time.deltaTime;
                }
                
                break;
            case TowerType.Enemy:
                _meshRenderer.material.color = Color.red;
                if (_allysCount > (_enemiesCount*2))
                {
                    _allyTowerTime += Time.deltaTime;
                }
                break;
            case TowerType.Neutral:
                _meshRenderer.material.color = Color.white;
                if (_enemiesCount > (_allysCount * 2))
                {
                    _enemyTowerTime += Time.deltaTime;
                }
                else if (_allysCount > (_enemiesCount * 2))
                {
                    _allyTowerTime += Time.deltaTime;
                }
                break;
        }
        if (_enemyTowerTime >= _conquerTime)
        {
            _allyTowerTime = 0;
            _enemyTowerTime = 0;
            towerType = TowerType.Enemy;
        }
        else if (_allyTowerTime >= _conquerTime)
        {
            _allyTowerTime = 0;
            _enemyTowerTime = 0;
            towerType = TowerType.Ally;
        }

        enemyCountTxt.text = "Enemy Time: \n" + (_conquerTime-_enemyTowerTime) + "\nEnemys: \n" + _enemiesCount.ToString();
        allyCountTxt.text = "Ally Time: \n" + (_conquerTime-_allyTowerTime) + "\nAllys: \n" + _allysCount.ToString();

        if (_activeCentralTowerTime >= _activeTime && towerType == TowerType.Neutral)
        {
            _activeCentralTowerTime = 0;
            _isActive = false;
            foreach (TowerController tower in _towers)
            {
                tower.KillEverything();
            }
        }
        else if (_activeCentralTowerTime >= _activeTime && towerType == TowerType.Ally)
        {
            //Ganan los aliados
            Time.timeScale = 0;
        }
        else if (_activeCentralTowerTime >= _activeTime && towerType == TowerType.Enemy)
        {
            //Ganan los enemigos
            Time.timeScale = 0;
        }
        _activeCentralTowerTime += Time.deltaTime;
        _activeCentralTowerTime = Mathf.Clamp(_activeCentralTowerTime, 0, _activeTime);
        _enemyTowerTime = Mathf.Clamp(_enemyTowerTime, 0, _conquerTime);
        _allyTowerTime = Mathf.Clamp(_allyTowerTime, 0, _conquerTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && _isActive)
        {
            _enemiesCount++;
        }
        else if (other.CompareTag("Ally") && _isActive)
        {
            _allysCount++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")&& _enemiesCount>0 && _isActive)
        {
            _enemiesCount--;
        }
        else if (other.CompareTag("Ally") && _allysCount > 0 && _isActive)
        {
            _allysCount--;
        }
    }
    public void ActivateCentralTower()
    {
        int tAllys = 0;
        int tEnemys = 0;

        float tTime = 0;

        foreach (TowerController tower in _towers)
        {
            if (tower.towerType == TowerType.Ally)
            {
                tAllys++;
            }
            else if (tower.towerType == TowerType.Enemy)
            {
                tEnemys++;
            }
        }
        if (tAllys >= 3 || tEnemys >= 3)
        {
            tTime += Time.deltaTime;
        }
        else
        {
            tTime = 0;
        }
        tTime = Mathf.Clamp(tTime, 0, _conquerTime);
        if (tTime>=_conquerTime)
        {
            _isActive = true;
        }
    }
}

