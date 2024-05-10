using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeLime.Utility.AutoScroller
{
    /// <summary>
    /// Scrolls to the nearest target and correctly checks the anchor pos
    /// </summary>
    public class BaseAutoScroller : MonoBehaviour
    {
        [SerializeField] protected ScrollRect scroll;
        [SerializeField] protected RectTransform content;

        [Space]
        [SerializeField] protected Vector2 cellSize;

        [SerializeField] protected int amountPieces = 0;
        [SerializeField] protected float velocityThreshold = 15f;
        [SerializeField] protected float velocityThresholdMin = 5f;
        [SerializeField] protected float durationOfMove = 1f;

        protected bool calculateMove = true;
        protected bool canCheck = true;
        protected float velocityCached = 0;

        public int CurrentPiece { get; protected set; }
        public int AmountPieces { get { return amountPieces; } set { amountPieces = value; } }
        public RectTransform Content => content;

        public event Action<int> OnScrolledToItem;

        protected virtual void LateUpdate() { }

        protected virtual void StopAndMoveTo(int currentPiece) { }

        protected virtual void NotifySubscribers(int piece)
        {
            OnScrolledToItem?.Invoke(piece);
        }
    }
}
