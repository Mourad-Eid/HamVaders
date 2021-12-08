using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mapTextUpdate : MonoBehaviour
{
    TextMeshProUGUI mapText;
    // Start is called before the first frame update
    void Start()
    {
        mapText = GetComponent<TextMeshProUGUI>();
        mapText.text = "Thanks for Joining The Beta Test!";
    }

    // Update is called once per frame
    public void VanguardText()
    {
        mapText.text = "New Heroes are Coming Soon!";
    }

    public void CardsText()
    {
        mapText.text = "New Cards are Coming Soon!";
    }

    public void ShopText()
    {
        mapText.text = "Shop is Coming Soon!";
    }
}
