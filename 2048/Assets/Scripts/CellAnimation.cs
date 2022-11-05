using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

public class CellAnimation : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text points;

    [Inject] private ColorManager colorManager;

    private float moveTime = 0.1f;
    private float appearTime = 0.1f;

    private Sequence sequence;

    public void Move(Cell from, [CanBeNull] Cell to, bool isMerging)
    {
        from.CancelAnimation();
        to.SetAnimation(this);

        _image.color = colorManager.CellColors[from.Value];
        points.text = from.Points.ToString();
        points.color = from.Value <= 2 ? colorManager.PointsDarkColor : colorManager.PointsLightColor;

        transform.position = from.transform.position;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(to.transform.position, moveTime).SetEase(Ease.InOutQuad));
        if (isMerging)
        {
            sequence.AppendCallback(() =>
            {
                _image.color = colorManager.CellColors[to.Value];
                points.text = to.Points.ToString();
                points.color = to.Value <= 2 ? colorManager.PointsDarkColor : colorManager.PointsLightColor;
            });

            sequence.Append(transform.DOScale(1.2f, appearTime));
            sequence.Append(transform.DOScale(1f, appearTime));
        }

        sequence.AppendCallback(() =>
        {
            to.UpdateCell();
            Destroy();
        });
    }

    public void Appear(Cell cell)
    {
        cell.CancelAnimation();
        cell.SetAnimation(this);

        _image.color = colorManager.CellColors[cell.Value];
        points.text = cell.Points.ToString();
        points.color = cell.Value <= 2 ? colorManager.PointsDarkColor : colorManager.PointsLightColor;

        transform.position = cell.transform.position;
        transform.localScale = Vector2.zero;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(1.2f, appearTime * 2));
        sequence.Append(transform.DOScale(1f, appearTime * 2));
        
        sequence.AppendCallback(() =>
        {
            cell.UpdateCell();
            Destroy();
        });
    }

    public void Destroy()
    {
        sequence.Kill();
        Destroy(gameObject);
    }
}
