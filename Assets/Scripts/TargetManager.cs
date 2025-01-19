using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    #region Instancing
    private static TargetManager _instance;
    public static TargetManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<TargetManager>();

            return _instance;
        }
    }
    #endregion

    private List<HitTarget> _targetList;

    private void Awake()
    {
        _targetList = FindObjectsByType<HitTarget>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .ToList();

        foreach (var hitTarget in _targetList)
            hitTarget.Disappear();
    }

    public void StartPractice()
    {
        if (_targetAppearCoroutine != null)
        {
            StopCoroutine(_targetAppearCoroutine);
            foreach (var hitTarget in _targetList)
                hitTarget.Disappear();
        }

        _targetAppearCoroutine = TargetAppearCoroutine();
        StartCoroutine(_targetAppearCoroutine);
    }

    private IEnumerator _targetAppearCoroutine;
    IEnumerator TargetAppearCoroutine()
    {
        var copiedTargetList = new List<HitTarget>();
        copiedTargetList.AddRange(_targetList);
            
        var randomCount = Random.Range(1, _targetList.Count);

        for (int i = 0; i < randomCount; i++)
        {
            var randomIndex = Random.Range(0, copiedTargetList.Count);

            var target = copiedTargetList[randomIndex];
            copiedTargetList.Remove(target);
                
            target.Appear();

            yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));
        }

        yield return new WaitForSeconds(3f);
            
        foreach (var hitTarget in _targetList)
            hitTarget.Disappear();
    }
}