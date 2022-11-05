using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class CellAnimationController : MonoBehaviour
{
    [Inject] private CellAnimationFactory cellAnimationFactory;
    [Inject] private Field field;
    private void Awake()
    {
        DOTween.Init();
    }

    public void SmoothTranslation(Cell from, Cell to, bool isMerging)
    {
        var cellAnim = cellAnimationFactory.Create();
        cellAnim.Move(from, to, isMerging);
        cellAnim.transform.SetParent(field.transform);
    }

    public void SmoothAppear(Cell cell)
    {
        var cellAnim = cellAnimationFactory.Create();
        cellAnim.Appear(cell);
        cellAnim.transform.SetParent(field.transform);
    }
}
