using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Oddience/Character Customization")]
public class CharacterCustomizationOptions : SingletonScriptableObject<CharacterCustomizationOptions>
{
    [SerializeField] GameObject femaleBikini;
    public GameObject Bikini => femaleBikini;
    [SerializeField] GameObject[] hats;
    public GameObject[] Hats => hats;
    [SerializeField] Material[] skinMaterials;
    public Material[] SkinMaterials => skinMaterials;
    [SerializeField] Material[] pantsMaterials;
    public Material[] PantsMaterials => pantsMaterials;

    [System.Serializable]
    public class CharacterCustomizationProfile
    {
        public bool female;
        public int hatIndex;
        public int skinIndex;
        public int pantsIndex;

        public CharacterCustomizationProfile(bool female = false, int hatIndex = 0, int skinIndex = 0, int pantsIndex = 0)
        {
            this.female = false;
            this.hatIndex = hatIndex;
            this.skinIndex = skinIndex;
            this.pantsIndex = pantsIndex;
        }
    }
}
