using System.Collections.Generic;

public class Potion
{
    public int Base;
    public List<int> Items;
    public int Cooked;


    public Potion(int base_num, List<int> items)
    {
        this.Base = base_num;
        this.Items = items;
    }
}
