using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* TO DO  : add shop type 
            change max price depending of shop type
            add merchant reaction to change price
            add object rarity 
            change price depend on rarity
*/
public class BargainScript: MonoBehaviour {

    private int max;
    private int startPrice;
    private int sellingPrice;
    // int rand = 0;
    private int  playerMoney = 0;

    private int chance = 3;

    private bool keyboardOn = true;

    [SerializeField] GameObject bargainControl;
    [SerializeField] GameObject backControl;

    [SerializeField] TextMeshProUGUI textBargain;
    [SerializeField] TextMeshProUGUI textPrice;
    [SerializeField] TextMeshProUGUI textStartPrice;
    [SerializeField] TextMeshProUGUI textMerchant;
    [SerializeField] TextMeshProUGUI textPlayerMoney;
    string sellText;
    

    // Use this for initialization
    void Start ()
    {
        StartBargain();
	}
	
    void StartBargain()
    {
        startPrice = Random.Range(1, 12);
        max = Random.Range(startPrice + 1, startPrice * 2);
        sellingPrice = startPrice;
        textStartPrice.text = "Start Price : " + startPrice.ToString();
        textPrice.text =  "Sell Proposition : " + sellingPrice.ToString();
        textPlayerMoney.text =  "Coins : " + playerMoney.ToString();

        Debug.Log("Max Price " + max);
    }

	// Update is called once per frame
	void Update ()
    {
        BargainUpdate();
    }

    public void BargainUp(){
        sellingPrice++;
        textPrice.text =  "Sell Proposition : " + sellingPrice.ToString();
    }
        public void BargainDown(){
            if(sellingPrice > startPrice){
                sellingPrice--;
            }
        textPrice.text =  "Sell Proposition : " + sellingPrice.ToString();
    }

    public void BargainPlus(){
        sellingPrice += 5;
        textPrice.text =  "Sell Proposition : " + sellingPrice.ToString();
    }

    public void BargainMinus(){
        int temp = sellingPrice;
        temp -= 5;
        if( temp < startPrice){
            temp = startPrice;
            sellingPrice = temp;
        }

        if(sellingPrice > startPrice){
            sellingPrice -= 5;
        }
            
        textPrice.text =  "Sell Proposition : " + sellingPrice.ToString();
    }
    public void BargainSell(){
        if( sellingPrice <= max ){
            sellText = SuccessBargain(sellingPrice);
            playerMoney += sellingPrice;
            FinishBargain();

        }else{
            chance--;
            sellText = "It's too high !\nChange Left : " + chance;
            if(chance == 0){
                sellText = FailBargain(); 
                FinishBargain();
            }
        }

        textMerchant.text = sellText;
    }
    public void BargainUpdate(){

        if (keyboardOn){
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // sellingPrice++;
                BargainUp();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // sellingPrice--;
                BargainDown();
            }else if(Input.GetKeyDown(KeyCode.RightArrow)){
                BargainPlus();
            }else if(Input.GetKeyDown(KeyCode.LeftArrow)){
                BargainMinus();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                BargainSell();
            }
        }
    }

    private void FinishBargain(){
                bargainControl.SetActive(false);
                backControl.SetActive(true);
                keyboardOn = false;
                textPlayerMoney.text = "Coins : " + playerMoney.ToString();
    }
    private string SuccessBargain(int sellPrice){
        string sucess;
        int textChoice = Random.Range(1,4);
        switch (textChoice){
            case 1:
                sucess = "A fair price !\n";
                break;
            case 2:
                sucess = "It was nice doing business with you !\n";
                break;
            case 3:
                sucess = "Here's your money\n";
                break;
            default : 
                sucess = "Success !";
                break;
        }
        sucess += "You sold your item for " + sellPrice + " coins.";
        return sucess;
    }

    private string FailBargain(){
        string fail;
        int textChoice = Random.Range(1,4);
        switch (textChoice){
            case 1:
                fail = "Never come here again !\n";
                break;
            case 2:
                fail = "I will never buy at this price !\n";
                break;
            case 3:
                fail = "It's a rip off ! (I should hire him...)\n";
                break;
            default : 
                fail = "Fail !";
                break;
        }

        fail += "You fail to sell your item. ";
        return fail;

    }
    
}
