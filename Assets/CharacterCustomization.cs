using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    [SerializeField] Transform hatTransform;
    [SerializeField] Transform bikiniTransform;

    [SerializeField] Transform leftCheek;
    [SerializeField] Transform rightCheek;

    private float _maleCheekSize = 1.25f;

    [SerializeField] SkinnedMeshRenderer characterMesh;

    [SerializeField] int skinMaterialIndex;
    [SerializeField] int pantMaterialIndex;
    [SerializeField] int hatIndex;

    private GameObject _currentHat;
    private GameObject _currentBikini;

    [SerializeField] bool randomize;

    [SerializeField] bool customize;
    float hatChance = 0.33f;

    private void Start()
    {
        _currentBikini = Instantiate(CharacterCustomizationOptions.Instance.Bikini, Vector3.zero, Quaternion.Euler(Vector3.zero), bikiniTransform);
        _currentBikini.transform.localPosition = Vector3.zero;
        _currentBikini.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _currentBikini.SetActive(false);

        ChangeSex(false);

        if (randomize)
        {
            Randomize();
        }
        else if (customize)
        {
            ChangeHat(hatIndex);
            ChangeSkinMaterial(skinMaterialIndex);
            ChangePantMaterial(pantMaterialIndex);
        }
    }

    public void ChangeSex(bool female)
    {
        if (female)
        {
            _currentBikini.SetActive(true);
            leftCheek.transform.localScale = Vector3.one;
            rightCheek.transform.localScale = Vector3.one;
        }
        else
        {
            leftCheek.transform.localScale = Vector3.one * _maleCheekSize;
            rightCheek.transform.localScale = Vector3.one * _maleCheekSize;
        }
    }

    public void ChangeHat(int hatIndex)
    {
        if (_currentHat != null) Destroy(_currentHat);
        ChangeHat(Instantiate(CharacterCustomizationOptions.Instance.Hats[hatIndex]));
    }

    public void ChangeHat(GameObject newHat)
    {
        if (_currentHat != null) Destroy(_currentHat);
        _currentHat = newHat;
        _currentHat.transform.parent = hatTransform;
        _currentHat.transform.localPosition = Vector3.zero;
        _currentHat.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _currentHat.transform.localScale = Vector3.one;
    }

    public void ChangeHat()
    {
        if(_currentHat != null) Destroy( _currentHat);
    }

    public void ChangeSkinMaterial(int materialIndex)
    {
        List<Material> materials = new List<Material>();

        for (int i = 0; i < characterMesh.materials.Length; i++)
        {
            if (i == skinMaterialIndex) materials.Add(CharacterCustomizationOptions.Instance.SkinMaterials[materialIndex]);
            else materials.Add(characterMesh.materials[i]);
        }

        characterMesh.SetMaterials(materials);
    }

    public void ChangePantMaterial(int materialIndex)
    {
        List<Material> materials = new List<Material>();

        for (int i = 0; i < characterMesh.materials.Length; i++)
        {
            if (i == pantMaterialIndex) materials.Add(CharacterCustomizationOptions.Instance.PantsMaterials[materialIndex]);
            else materials.Add(characterMesh.materials[i]);
        }

        characterMesh.SetMaterials(materials);
    }

    public void Randomize()
    {
        bool hasHat = Random.Range(0f, 1f) < hatChance;
        if (hasHat) ChangeHat(Random.Range(0, CharacterCustomizationOptions.Instance.Hats.Length));
        else ChangeHat();
        bool female = Random.Range(0f, 1f) < 0.5f;
        ChangeSex(female);
        ChangePantMaterial(Random.Range(0, CharacterCustomizationOptions.Instance.PantsMaterials.Length));
        ChangeSkinMaterial(Random.Range(0, CharacterCustomizationOptions.Instance.SkinMaterials.Length));
    }
}
