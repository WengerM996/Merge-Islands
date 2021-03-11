using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] private GameObject _mergeEffect;
    [SerializeField] private GameObject _unpackEffect;
    

    private void OnEnable()
    {
        ItemIntersection.Merged += OnMerged;
        Box.Unpacked += OnUnpacked;
    }

    private void OnDisable()
    {
        ItemIntersection.Merged -= OnMerged;
        Box.Unpacked -= OnUnpacked;
    }

    private void OnMerged(int index, Cell cell)
    { 
        Create(_mergeEffect, cell.transform.position);
    }

    private void OnUnpacked(int index, Vector3 position)
    {
        Create(_unpackEffect, position);
    }

    private void Create(GameObject effectTemplate, Vector3 position)
    {
        var effect = Instantiate(effectTemplate, position, Quaternion.identity);
        Destroy(effect, 2f);
    }
}
