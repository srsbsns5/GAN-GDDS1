using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Character")]
    public class Character : ScriptableObject
    {
        public string charName;
        public Sprite portrait;
    }
}