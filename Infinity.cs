using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public struct Infinity
{
    public static string[] TypeArr = new[]
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N",
        "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
    };
    
    public class InvalidParameterException : Exception { }

    private int TypeIndex
    {
        get { return Math.Abs(Index); }
    }
    
    public int Index { get; private set; }
    public double Number { get; private set; }

    #region Constucor
    public Infinity(int input) : this((long) input) { }
    
    public Infinity(long input)
    {
        this = default(Infinity);
        Index = GetIndex(input);
        var t = Math.Pow(1000.0, Index);
        Number = input / t;
    }
    
    public Infinity(string input) : this()
    {
        string n = input.Substring(0, input.Length - 1);
        string type = input.Substring(input.Length- 1, 1);
        Index = -1;
        Index = GetTypeArrFromString(type);
        Number = double.Parse(n);
        RecaculateIndex();
    }

    public Infinity(double input, string type) : this()
    {
        Index = -1;
        Index = GetTypeArrFromString(type);
        Number = input;
        RecaculateIndex();
    }

    public Infinity(double number, int index)
    {
        Number = number;
        Index = index;
    }

    public Infinity(Infinity origin)
    {
        Number = origin.Number;
        Index = origin.Index;
    }
    
    #endregion

    public override string ToString()
    {
        return string.Format("{0:F1}{1}", Number, TypeArr[TypeIndex]);
    }


    #region Implicit
    public static implicit operator Infinity(int input) {
        return new Infinity(input);
    }
    
    public static implicit operator Infinity(long input) {
        return new Infinity(input);
    }
    
    public static implicit operator Infinity(string input) {
        return new Infinity(input);
    }
    #endregion
    
    #region Calculate

    public static Infinity operator +(Infinity inf1, int n)
    {
        return (inf1 + new Infinity(n));
    }
        
    public static Infinity operator +(Infinity inf1, long n)
    {
        return (inf1 + new Infinity(n));
    }
    
    public static Infinity operator +(Infinity inf1, Infinity inf2)
    {
        Infinity result = inf1;
        var subIndex = result.Index - inf2.Index;
        var t = Math.Pow(1000.0, subIndex);
        result.Number += inf2.Number / t;
        
        return result;
    }
    
    
    public static Infinity operator -(Infinity inf1, Infinity inf2)
    {
        Infinity result = inf1;
        var subIndex = result.Index - inf2.Index;
        var t = Math.Pow(1000.0, subIndex);
        result.Number -= inf2.Number / t;
        return result;
    }
    
    public static Infinity operator *(Infinity inf1, int n)
    {
        return (inf1 * (double) n);
    }
    
    public static Infinity operator *(Infinity inf1, long n)
    {
        return (inf1 * (double) n);
    }
    
    public static Infinity operator *(Infinity inf1, float n)
    {
        return (inf1 * (double) n);
    }
    
    public static Infinity operator *(Infinity inf1, double n)
    {
        Infinity result = inf1;
        result.Number *= n;
        result.RecaculateIndex();
        return result;
    }

    public static Infinity operator *(Infinity inf1, Infinity inf2)
    {
        Infinity result = inf1;
        result.Number *= inf2.Number;
        result.Index += inf2.Index;
        result.RecaculateIndex();
        return result;
    }

    
    public static Infinity operator /(Infinity inf1, int n)
    {
        return (inf1 / (double) n);
    }
    
    public static Infinity operator /(Infinity inf1, long n)
    {
        return (inf1 / (double) n);
    }
    
    public static Infinity operator /(Infinity inf1, float n)
    {
        return (inf1 / (double) n);
    }
    
    public static Infinity operator /(Infinity inf1, double n)
    {
        Infinity result = inf1;
        result.Number /= n;
        result.RecaculateIndex();
        return result;
    }
    
    public static Infinity operator /(Infinity inf1, Infinity inf2)
    {
        Infinity result = inf1;
        result.Number /= inf2.Number;
        result.Index -= inf2.Index;
        result.RecaculateIndex();
        return result;
    }

    #endregion
    
    #region Compare
    public static bool operator==(Infinity inf1, Infinity inf2)
    {
        return inf1.Index == inf2.Index &&
               inf1.Number == inf2.Number;
    }

    public static bool operator !=(Infinity inf1, Infinity inf2)
    {
        return !(inf1 == inf2);
    }

    public static bool operator>(Infinity inf1, Infinity inf2)
    {
        if (inf1.Index == inf2.Index)
            return inf1.Number > inf2.Number;

        return inf1.Index > inf2.Index;

    }

    public static bool operator <(Infinity inf1, Infinity inf2)
    {
        if (inf1.Index == inf2.Index)
            return inf1.Number < inf2.Number;

        return inf1.Index < inf2.Index;
    }
    
    public bool Equals(Infinity other)
    {
        return Index == other.Index && Number.Equals(other.Number);
    }

    public override bool Equals(object obj)
    {
        return obj is Infinity other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Index * 397) ^ Number.GetHashCode();
        }
    }
    #endregion

    #region Util

    private void RecaculateIndex()
    {
        if (Math.Abs(Number) >= 1000.0)
        {
            Number /= 1000.0;
            Index++;
        }
        else if (Math.Abs(Number) < 1)
        {
            Number *= 1000.0;
            Index--;
        }
    }

    private int GetIndex(long input)
    {
        long temp = input;
        int index = 0;
        for (index = 0; temp >= 1000; index++)
        {
            temp /= 1000;
        }

        return index;
    }
    
    private int GetTypeArrFromString(string str)
    {
        int result = -1;
        for (int i = 0; i < TypeArr.Length; i++)
        {
            if (TypeArr[i] == str)
            {
                result = i;
                break;
            }
        }

        if (result == -1)
            throw new InvalidParameterException();
        
        return result;
    }
    #endregion
}
