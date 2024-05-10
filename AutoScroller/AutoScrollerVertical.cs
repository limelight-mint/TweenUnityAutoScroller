using UnityEngine;
using DG.Tweening;

namespace PrimeLime.Utility.AutoScroller
{
    /// <summary>
    /// Scrolls to the nearest target and correctly checks the anchor pos
    /// </summary>
    public class AutoScrollerVertical : BaseAutoScroller
    {
        protected override void LateUpdate()
        {
            canCheck = Mathf.Abs(scroll.velocity.y) > velocityThresholdMin;

            if(!canCheck) return;
            calculateMove = velocityCached < velocityThreshold && velocityCached > Mathf.Abs(scroll.velocity.y);

            if(calculateMove) 
            {
                StopAndMoveTo(CurrentPiece);
                return;
            }

            float valueToCheck = content.anchoredPosition.y;
            float cell = cellSize.y;

            CurrentPiece = (int)Mathf.Round(valueToCheck / cell);
            velocityCached = Mathf.Abs(scroll.velocity.y);
        }

        protected override void StopAndMoveTo(int currentPiece)
        {
            calculateMove = false;
            canCheck = false;

            scroll.velocity = new Vector2(0,0);
            content.DOAnchorPosY(cellSize.y * CurrentPiece, durationOfMove).OnComplete(() =>
            {
                canCheck = true;
                calculateMove = true;
                NotifySubscribers(CurrentPiece);
            });
        }
    }
}
