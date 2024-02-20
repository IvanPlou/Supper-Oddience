using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceGuage : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float fillSpeed = 10f;
    float _fill;


    private void Start()
    {
        GameManager.Instance.Audience.OnPointsUpdate += OnAudienceUpdate;
    }


    // Update is called once per frame
    void Update()
    {
        image.fillAmount = Mathf.Lerp(image.fillAmount, _fill, fillSpeed * Time.deltaTime);
    }

    void OnAudienceUpdate(object sender, Audience.OnPointsUpdateEventArgs args)
    {
        _fill = args.points / args.goal;
    }


}
