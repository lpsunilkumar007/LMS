using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class StringHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToFirstLetterCapitalize(this string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        if (string.IsNullOrWhiteSpace(value)) return value;
        var textInfo = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="optType"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GenerateId()
    {
        long i = 1;
        foreach (byte b in Guid.NewGuid().ToByteArray())
        {
            i *= b + 1;
        }
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string XmlSafeReplace(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }
        str = str.Replace("&", "&amp;");
        str = str.Replace("<", "&lt;");
        str = str.Replace(">", "&gt;");
        str = str.Replace("\"", "&quot;");
        str = str.Replace("'", "&#39;");

        return str;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="find"></param>
    /// <param name="replace"></param>
    /// <param name="matchWholeWord"></param>
    /// <returns></returns>
    public static string SafeReplace(this string input, string find, string replace, bool matchWholeWord = true)
    {
        if (replace == null) replace = "";
        if (find == null) find = "";
        string textToFind = matchWholeWord ? string.Format(@"\b{0}\b", find) : find;
        string outPut = Regex.Replace(input, textToFind, replace);
        //if (makeTextXmlSafeReplace)
        //{
        //    outPut = XmlSafeReplace(outPut);
        //}
        return outPut;
    }
    /// <summary>
    /// Array TO String (delimeted)
    /// </summary>
    /// <param name="commaSeperatedString"></param>
    /// <returns></returns>
    public static string ToDelimetedString(this List<string> ValueList, char delimeter = ' ', bool InsertBreak = false)
    {
        string _return = "";
        foreach (var item in ValueList)
        {
            _return += item.ToString() + delimeter;
            if (InsertBreak)
                _return += "<br />";
        }

        if (_return.Length > 0 && _return.LastIndexOf(',') == _return.Length - 1) // there is an extra delimeter at the end
        {
            _return = _return.Substring(0, _return.Length - 1);
        }
        return _return;
    }
    /// <summary>
    /// Array TO String (delimeted)
    /// </summary>
    /// <param name="commaSeperatedString"></param>
    /// <returns></returns>
    public static string IntArrayToString(this int?[] IntArray, char delimeter = ',')
    {
        string _return = "";
        foreach (var item in IntArray)
        {
            _return += item.ToString() + delimeter;
        }

        if (_return.Length > 0 && _return.LastIndexOf(',') == _return.Length - 1) // there is an extra delimeter at the end
        {
            _return = _return.Substring(0, _return.Length - 1);
        }
        return _return;
    }
    /// <summary>
    /// comma seperator
    /// </summary>
    /// <param name="commaSeperatedString"></param>
    /// <returns></returns>
    public static int?[] StringToNullIntArray(this string commaSeperatedString)
    {
        List<int?> myIntegers = new List<int?>();

        Array.ForEach(commaSeperatedString.Split(",".ToCharArray()), s =>
        {
            int currentInt;
            if (Int32.TryParse(s, out currentInt))
                myIntegers.Add(currentInt);
        });
        return myIntegers.ToArray();
    }
    /// <summary>
    /// comma seperator
    /// </summary>
    /// <param name="commaSeperatedString"></param>
    /// <returns></returns>
    public static int[] StringToIntArray(this string commaSeperatedString)
    {
        List<int> myIntegers = new List<int>();
        Array.ForEach(commaSeperatedString.Split(",".ToCharArray()), s =>
        {
            int currentInt;
            if (Int32.TryParse(s, out currentInt))
                myIntegers.Add(currentInt);
        });
        return myIntegers.ToArray();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="toCheck"></param>
    /// <param name="comp"></param>
    /// <returns></returns>
    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source.IndexOf(toCheck, comp) >= 0;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <param name="StartString"></param>
    /// <param name="EndString"></param>
    /// <returns></returns>
    public static string Substring(this string str, string StartString, string EndString)
    {
        if (str.Contains(StartString))
        {
            int iStart = str.IndexOf(StartString) + StartString.Length;
            int iEnd = str.IndexOf(EndString, iStart);
            return str.Substring(iStart, (iEnd - iStart));
        }
        return string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GenerateTestReferenceNumber()
    {
        long i = Guid.NewGuid().ToByteArray().Aggregate<byte, long>(1, (current, b) => current * ((int)b + 1));
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }
}



