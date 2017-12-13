using System;

class Launcher
{
    public static void Main()
    {
        var list = new LinkedList<int>();

        list.AddFirst(2);
        list.AddFirst(3);

        ShowCount(list);
        LogList(list);

        list.AddLast(4);
        ShowCount(list);
        LogList(list);

        list.AddLast(5);
        ShowCount(list);
        LogList(list);

        //list.RemoveFirst();
        //ShowCount(list);
        //LogList(list);

        //list.RemoveFirst();
        //ShowCount(list);
        //LogList(list);

        //list.RemoveFirst();
        //ShowCount(list);
        //LogList(list);

        //list.RemoveFirst();
        //ShowCount(list);
        //LogList(list);

        list.RemoveLast();
        ShowCount(list);
        LogList(list);

        list.RemoveLast();
        ShowCount(list);
        LogList(list);
        list.RemoveLast();
        ShowCount(list);
        LogList(list);
        list.RemoveLast();
        ShowCount(list);
        LogList(list);

    }

    public static void LogList(LinkedList<int> list)
    {
        Console.WriteLine("Values in the linked list:");
        foreach (var item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    public static void ShowCount(LinkedList<int> list)
    {
        Console.WriteLine("Count: " + list.Count);
    }
}
