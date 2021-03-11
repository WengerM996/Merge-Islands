using System;
using DG.Tweening;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    [SerializeField] private Vector3 _byPosition;
    [SerializeField] private float _duration;
    [SerializeField] private LevelSystem _levelSystem;

    private FieldBuilder _fieldBuilder;

    private void OnEnable()
    {
        _levelSystem.ReachedNextLevel += OnReachedNextLevel;
    }

    private void OnDisable()
    {
        _levelSystem.ReachedNextLevel -= OnReachedNextLevel;
    }

    private void Start()
    {
        _fieldBuilder = GetComponent<FieldBuilder>();
        LoadPosition();
    }

    private void LoadPosition()
    {
        var position = transform.position;
        
        if (PlayerPrefs.HasKey("posX"))
        {
            position.x = PlayerPrefs.GetFloat("posX");
        }
        
        if (PlayerPrefs.HasKey("posY"))
        {
            position.y = PlayerPrefs.GetFloat("posY");
        }
        
        if (PlayerPrefs.HasKey("posZ"))
        {
            position.z = PlayerPrefs.GetFloat("posZ");
        }

        transform.position = position;

        /*
        var countCells = CountUnlockedCells();
        
        if (countCells < 9) return;
        
        var multi = 1;
        var newPosition = transform.position;

        if (countCells >= 12 && countCells <= 15)
        {
            
            multi = 1;  //multi = 2;
        } else if (countCells > 15)
        {
            multi = 2;
            newPosition.x += _byPosition.x;
        }
        
        newPosition += new Vector3(0f, _byPosition.y * multi, _byPosition.z * multi);
        transform.position = newPosition;
        //transform.DOMove(newPosition, _duration);*/
    }

    private void OnReachedNextLevel()
    {
        var countCells = CountUnlockedCells();
        
        //if (countCells % 3 != 0) return;
        
        var newPosition = transform.position;

        if (countCells == 10)
        {
            newPosition.y += _byPosition.y;
        }
        
        if (countCells == 13)
        {
            newPosition.y += _byPosition.y;
            newPosition.z += _byPosition.z;
        }
        
        if (countCells == 16)
        {
            newPosition.x += _byPosition.x;
        }
        
        SetNewPosition(newPosition);

        //Check();

        /*var countCells = CountUnlockedCells();
        if (countCells >= 17) return;
        
        if (countCells == 16)
        {
            var newPosition = transform.position;
            newPosition.x += _byPosition.x;
            transform.DOMove(newPosition, _duration);
        } else if (countCells < 15 && countCells % 3 == 0)
        {
            var newPosition = transform.position;
            newPosition.y += _byPosition.y;
            newPosition.z += _byPosition.z;
            
        }*/
    }

    private void SetNewPosition(Vector3 newPosition)
    {
        transform.DOMove(newPosition, _duration);
        PlayerPrefs.SetFloat("posX", newPosition.x);
        PlayerPrefs.SetFloat("posY", newPosition.y);
        PlayerPrefs.SetFloat("posZ", newPosition.z);
    }

    private int CountUnlockedCells()
    {
        var counter = 0;

        foreach (var cell in _fieldBuilder.Cells)
        {
            if (cell.Unlock)
            {
                counter++;
            }
        }

        return counter;
    }
}
