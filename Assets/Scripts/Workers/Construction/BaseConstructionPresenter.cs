using System;
using System.Collections;
using UnityEngine;
using Workers;

public class BaseConstructionPresenter : MonoBehaviour
{
    [SerializeField] private BaseConstructionView _view;
    [SerializeField] private BaseConstructionConfig _config;
    
    private Coroutine _constructionCoroutine;
    private Worker _worker;

    public event Action<Base, Worker> OnConstructionComplete;

    public void SetWorker(Worker worker)
    {
        _worker = worker;
        _worker.BuildStarted += StartConstruction;
    }

    private void StartConstruction(Vector3 position)
    {
        if (_constructionCoroutine != null)
            StopCoroutine(_constructionCoroutine);

        Base newBase = Instantiate(_config.BasePrefab, position, Quaternion.identity);
        _constructionCoroutine = StartCoroutine(ConstructionProcess(newBase.transform));
    }

    private IEnumerator ConstructionProcess(Transform baseTransform)
    {
        for (int i = 0; i < _config.StageCount; i++)
        {
            _view.ShowStage(i, baseTransform);
            yield return new WaitForSeconds(_config.TimePerStage);
        }
        
        _constructionCoroutine = null;
        OnConstructionCompleted(baseTransform.GetComponent<Base>());
    }

    private void OnConstructionCompleted(Base baseObject)
    {
        _view.ShowFinalBase(baseObject.transform);
        _view.Clear();
        _worker.BuildStarted -= StartConstruction;
        OnConstructionComplete?.Invoke(baseObject, _worker);
        _worker = null;
    }
}
