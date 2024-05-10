using UnityEngine;
using UnityEngine.UI;

namespace PrimeLime.Utility.AutoScroller
{
    /// <summary>
    /// Scrolls to the nearest target and correctly checks the anchor pos
    /// </summary>
    public class AutoScrollerAvatars : AutoScrollerVertical
    {
        protected override void NotifySubscribers(int piece)
        {
            var img = Content.GetChild(piece).GetComponent<Image>();
            piece = int.Parse(img.sprite.name);
            Debug.Log($"Selected image ID: {piece}");

            base.NotifySubscribers(piece);
        }
    }
}
