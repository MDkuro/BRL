using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{

    public int startCoinQuantity;
    public int startPointQuantity;
    public int startLifeQuantity;
    public Text coinQuantity;
    public Text pointQuantity;
    public Text lifeQuantity;
    public int points;

    public static int CurrentCoinQuantity;
    public static int CurrentPointQuantity;
    public static int CurrentLifeQuantity;

    // Start is called before the first frame update
    void Start()
    {
        CurrentCoinQuantity = startCoinQuantity;
        CurrentPointQuantity = startPointQuantity;
        CurrentLifeQuantity = startLifeQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        coinQuantity.text = CurrentCoinQuantity.ToString();
        pointQuantity.text = CurrentPointQuantity.ToString();
        lifeQuantity.text = CurrentLifeQuantity.ToString();
    }
}
