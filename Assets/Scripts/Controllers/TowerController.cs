using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum TowerType
{
    Ally,
    Enemy,
    Neutral
}
public class TowerController : MonoBehaviour
{
    public int enemiesCount;
    private int _allysCount;
    public TowerType towerType = TowerType.Neutral;
    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private float _conquerTime;
    private float _enemyTowerTime;
    private float _allyTowerTime;

    public TextMeshProUGUI enemyCountTxt;
    public TextMeshProUGUI allyCountTxt;

    private List<GameObject> _entities = new List<GameObject>();

    private void Update()
    {

        switch (towerType)
        {
            case TowerType.Ally:
                _meshRenderer.material.color = Color.blue;
                if (enemiesCount > (_allysCount+_allysCount/2))
                {
                    _enemyTowerTime += Time.deltaTime;
                }
                
                break;
            case TowerType.Enemy:
                _meshRenderer.material.color = Color.red;
                if (_allysCount > (enemiesCount + enemiesCount / 2))
                {
                    _allyTowerTime += Time.deltaTime;
                }
                break;
            case TowerType.Neutral:
                _meshRenderer.material.color = Color.white;
                if (enemiesCount > _allysCount)
                {
                    _enemyTowerTime += Time.deltaTime;
                }
                else if (enemiesCount < _allysCount)
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

        enemyCountTxt.text = "Enemy Time: \n" + (_conquerTime-_enemyTowerTime) + "\nEnemys: \n" + enemiesCount.ToString();
        allyCountTxt.text = "Ally Time: \n" + (_conquerTime-_allyTowerTime) + "\nAllys: \n" + _allysCount.ToString();

        _enemyTowerTime = Mathf.Clamp(_enemyTowerTime, 0, _conquerTime);
        _allyTowerTime = Mathf.Clamp(_allyTowerTime, 0, _conquerTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesCount++;
            if (other.GetComponent<EnemyController>().enemyType == EnemyController.EnemyType.Normal)
            {
                other.GetComponent<EnemyController>().GoToTower(gameObject.transform);
            }

            other.GetComponent<EnemyController>().onTower=true;
            _entities.Add(other.gameObject);

        }
        else if (other.CompareTag("Ally"))
        {
            _allysCount++;
            _entities.Add(other.gameObject);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")&& enemiesCount > 0)
        {
            enemiesCount--;
            _entities.Remove(other.gameObject);


            other.GetComponent<EnemyController>().onTower = false;

        }
        else if (other.CompareTag("Ally") && _allysCount > 0)
        {
            _allysCount--;
            _entities.Remove(other.gameObject);
        }

    }

    public void KillEverything()
    {
        _entities.ForEach(entity => Destroy(entity));
        towerType = TowerType.Neutral;
    }
}

