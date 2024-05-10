using UnityEngine;
using DG.Tweening;

namespace PrimeLime.Utility.AutoScroller
{
    /// <summary>
    /// Scrolls to the nearest target and correctly checks the anchor pos
    /// </summary>
    public class AutoScrollerHorizontal : BaseAutoScroller
    {
        protected override void LateUpdate()
        {
            if(amountPieces <= 0) return;
            canCheck = Mathf.Abs(scroll.velocity.x) > velocityThresholdMin;

            if(!canCheck) return;
            calculateMove = velocityCached < velocityThreshold && velocityCached > Mathf.Abs(scroll.velocity.x);

            if(calculateMove) 
            {
                StopAndMoveTo(CurrentPiece);
                return;
            }

            float valueToCheck = content.anchoredPosition.x;
            float cell = cellSize.x;

            CurrentPiece = (int)Mathf.Round(valueToCheck / cell);
            velocityCached = Mathf.Abs(scroll.velocity.x);
        }

        protected override void StopAndMoveTo(int currentPiece)
        {
            calculateMove = false;
            canCheck = false;

            scroll.velocity = new Vector2(0,0);
            content.DOAnchorPosX(cellSize.y * CurrentPiece, durationOfMove).OnComplete(() =>
            {
                canCheck = true;
                calculateMove = true;
                NotifySubscribers(CurrentPiece);
            });
        }
    }
}
