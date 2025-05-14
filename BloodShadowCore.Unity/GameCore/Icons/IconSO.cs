using System;
using System.Collections.Generic;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.Icons
{
    [CreateAssetMenu(fileName = "IconSO", menuName = "IconSO")]
    public class IconSO : ScriptableObject
    {
        [field: SerializeField] public string Device { get; private set; }
        [field: SerializeField] public List<IconPair> IconPairs { get; private set; }

        [Serializable]
        public struct IconPair
        {
            [field: SerializeField] public string Name { get; private set; }
            [field: SerializeField] public Sprite Icon { get; private set; }

            public static implicit operator BloodShadow.GameCore.Icons.IconData.IconPair(IconPair pair)
                => new BloodShadow.GameCore.Icons.IconData.IconPair(pair.Name, pair.Icon.texture.EncodeToPNG());
        }
    }
}
