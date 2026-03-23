using System;
using System.Collections;
using UnityEngine;
using Workers;

public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private BaseConstructionView _view;
    [SerializeField] private BaseConstructionConfig _config;

    public event Action<Base, Worker> Built;

    public void Build(Worker worker, Vector3 position)
    {
        StartCoroutine(BuildProcess(worker, position));
    }

    private IEnumerator BuildProcess(Worker worker, Vector3 position)
    {
        Base newBase = Instantiate(_config.BasePrefab, position, Quaternion.identity);

        for (int i = 0; i < _config.StageCount; i++)
        {
            _view.ShowStage(i, newBase.transform);
            yield return new WaitForSeconds(_config.TimePerStage);
        }

        _view.ShowFinalBase(newBase.transform);
        _view.Clear();

        Built?.Invoke(newBase, worker);
    }
}
