using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Database : MonoBehaviour
{
    public static bool init = false;

    //Shop List
    public static List<List<int>> shopList = new List<List<int>>();

    public static List<List<int>> saleList = new List<List<int>>();

    //Shops
    public static List<int> wepShop = new List<int>();
    public static List<int> wepShopSale = new List<int>();

    public static List<int> magicShop = new List<int>();
    public static List<int> magicShopSale = new List<int>();

    public static List<int> alcShop = new List<int>();
    public static List<int> alcShopSale = new List<int>();

    public static List<int> dungShop = new List<int>();
    public static List<int> dungShopSale = new List<int>();

    //Buyback List
    public static List<Item> buybackList = new List<Item>();

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void initDB()
    {
        if (!init)
        {
            init = true;

            shopList.Add(wepShop);
            shopList.Add(magicShop);
            shopList.Add(alcShop);
            shopList.Add(dungShop);

            saleList.Add(wepShopSale);
            saleList.Add(magicShopSale);
            saleList.Add(alcShopSale);
            saleList.Add(dungShopSale);

            //Weapon Shop
            wepShop.Add(39);
            wepShop.Add(92);
            wepShop.Add(106);
            wepShop.Add(117);

            wepShop.Add(40);
            wepShop.Add(93);
            wepShop.Add(107);

            wepShop.Add(108);
            wepShop.Add(109);
            wepShop.Add(110);
            wepShop.Add(111);
            wepShop.Add(112);
            wepShop.Add(113);
            wepShop.Add(114);
            wepShop.Add(115);
            wepShop.Add(116);
            wepShop.Add(94);
            wepShop.Add(95);
            wepShop.Add(96);
            wepShop.Add(97);
            wepShop.Add(98);
            wepShop.Add(99);
            wepShop.Add(100);
            wepShop.Add(101);
            wepShop.Add(102);
            wepShop.Add(118);
            wepShop.Add(119);
            wepShop.Add(120);
            wepShop.Add(121);
            wepShop.Add(122);
            wepShop.Add(123);
            wepShop.Add(124);
            wepShop.Add(125);
            wepShop.Add(126);

            //Weapon Sale
            wepShopSale.Add(41);
            wepShopSale.Add(94);

            //Magic Shop
            magicShop.Add(11);
            magicShop.Add(12);
            magicShop.Add(13);
            magicShop.Add(14);
            magicShop.Add(15);
            magicShop.Add(16);
            magicShop.Add(31);
            magicShop.Add(32);
            magicShop.Add(33);
            magicShop.Add(34);
            magicShop.Add(35);
            magicShop.Add(36);

            //Magic Sell
            magicShopSale.Add(11);
            magicShopSale.Add(31);

            //Alc Shop
            alcShop.Add(6);
            alcShop.Add(7);
            alcShop.Add(37);

            //Alc Sale
            alcShopSale.Add(30);



        }
    }

    public static void ClearBuyback()
    {
        buybackList.Clear();
    }
}
