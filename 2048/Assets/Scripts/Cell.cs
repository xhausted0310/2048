using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Cell : MonoBehaviour
{
    public int X { get; private set; }
    public int Y { get; private set; }
    
    public int Value { get; private set; }
    public int Points => isEmpty ? 0 : (int)Mathf.Pow(2, Value);

    public bool isEmpty => Value == 0;
    public bool HasMerged { get; private set; }

    public const int MaxValue = 11;

    [SerializeField] private Image image;
    [SerializeField] private Text points;

    private CellAnimation currentAnimation;
    
    [Inject] private ColorManager ColorManager;
    [Inject] private GameController GameController;
    [Inject] private CellAnimationController CellAnimationController;

    // [Inject]private ICell _cell;
    //
    // [Inject]
    // public void Initialize(ICell cell)
    // {
    //     this._cell = cell;
    // }
    
    
    
    public void SetValue(int x, int y, int value, bool updateUI = true)
    {
        X = x;
        Y = y;
        Value = value;

        if (updateUI)
        {
            UpdateCell();
        }
    }

    public void IncreaseValue()
    {
        Value++;
        HasMerged = true;
        GameController.AddPoints(Points);
    }

    public void ResetFlags()
    {
        HasMerged = false;
    }

    public void MergeWithCell(Cell otherCell)
    {
        CellAnimationController.SmoothTranslation(this, otherCell, true);
        otherCell.IncreaseValue();
        SetValue(X,Y,0);
    }

    public void MoveToCell(Cell target)
    {
        CellAnimationController.SmoothTranslation(this, target, false);
        target.SetValue(target.X, target.Y, Value, false);
        SetValue(X,Y,0);
    }
    public void UpdateCell()
    {
        points.text = isEmpty ? string.Empty : Points.ToString();
        points.color = Value <= 2 ? ColorManager.PointsDarkColor : 
            ColorManager.PointsLightColor;

        image.color = ColorManager.CellColors[Value];
    }

    public void SetAnimation(CellAnimation animation)
    {
        currentAnimation = animation;
    }

    public void CancelAnimation()
    {
        if (currentAnimation != null)
        {
            currentAnimation.Destroy();
        }
    }
    
}
