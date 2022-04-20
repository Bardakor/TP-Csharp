using System;

public class Warmup
{
    public static bool Ispalindrome(string str)
    {
        //if empty string return false
        if (str == "")
        {
            return false;
        }
        //remove all non-alphanumeric characters
        str = str.Replace(" ", "");
        str = str.Replace(".", "");
        str = str.Replace(",", "");
        str = str.Replace("!", "");
        str = str.ToLower();
        //verify if str is a palindrome
        for (int i = 0; i < str.Length / 2; i++)
        {
            if (str[i] != str[str.Length - 1 - i])
            {
                return false;
            }
        }
        return true;
    }

    public static char RotChar (char c, int key)
    {
       //if c is not a capital letter
        if (c < 'A' || c > 'Z')
        {
            return c;
        }
        //if c is a capital letter
        else
        {
            //if key is positive
            if (key > 0)
            {
                //if c is Z
                if (c == 'Z')
                {
                    return 'A';
                }
                //if c is not Z
                else
                {
                    return (char)(c + key);
                }
            }
            //if key is negative
            else
            {
                //if c is A
                if (c == 'A')
                {
                    return 'Z';
                }
                //if c is not A
                else
                {
                    return (char)(c + key);
                }
            }
        }
    }

    public static string RotString(string str, int key)
    {
        //if empty string return empty string
        if (str == "")
        {
            return "";
        }
        //if key is positive
        if (key > 0)
        {
            //if key is greater than 26
            if (key > 26)
            {
                key = key % 26;
            }
            //if key is less than 26
            else
            {
                //create empty string
                string newStr = "";
                //for each character in str
                for (int i = 0; i < str.Length; i++)
                {
                    //add RotChar to newStr
                    newStr += RotChar(str[i], key);
                }
                return newStr;
            }
        }
        //if key is negative
        else
        {
            //if key is less than -26
            if (key < -26)
            {
                key = key % 26;
            }
            //if key is greater than -26
            else
            {
                //create empty string
                string newStr = "";
                //for each character in str
                for (int i = 0; i < str.Length; i++)
                {
                    //add RotChar to newStr
                    newStr += RotChar(str[i], key);
                }
                return newStr;
            }
        }
        return str;
    }

    public static int BinarySearch(int [] array, int elt)
    {
        //if array is empty return -1
        if (array.Length == 0)
        {
            return -1;
        }
        //if array is not empty
        else
        {
            //create left and right indexes
            int left = 0;
            int right = array.Length - 1;
            //while left is less than right
            while (left <= right)
            {
                //create mid index
                int mid = (left + right) / 2;
                //if elt is less than array[mid]
                if (elt < array[mid])
                {
                    //set right to mid - 1
                    right = mid - 1;
                }
                //if elt is greater than array[mid]
                else if (elt > array[mid])
                {
                    //set left to mid + 1
                    left = mid + 1;
                }
                //if elt is equal to array[mid]
                else
                {
                    return mid;
                }
            }
            //if elt is not in array
            return -1;
        }
    }


    
}
