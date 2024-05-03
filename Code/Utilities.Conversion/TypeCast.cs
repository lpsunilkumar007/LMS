using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Serialization;

public static class TypeCast
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="val"></param>
    /// <param name="alt"></param>
    /// <returns></returns>
    public static T ToType<T>(this object val, T alt = default(T)) where T : struct, IConvertible
    {
        try
        {
            if (val == null) return alt;
            if (val is DBNull) return alt;
            return (T)Convert.ChangeType(val, typeof(T));
        }
        catch
        {
            return alt;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="val"></param>
    /// <param name="alt"></param>
    /// <returns></returns>
    public static T? ToTypeOrNull<T>(this object val, T? alt = null) where T : struct, IConvertible
    {
        try
        {
            if (val != null && !(val is DBNull))
            {
                return (T)Convert.ChangeType(val, typeof(T));
            }
            return null;
        }
        catch
        {
            return alt;
        }
    }
    /// <summary>
    /// used to get Enum From String
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T ToEnum<T>(this object value)
    {
        return (T)Enum.Parse(typeof(T), value.ToString(), true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToEnumString<T>(this object value)
    {
        value = (T)Enum.Parse(typeof(T), value.ToString(), true);
        var fieldInfo = value.GetType().GetField(value.ToString());
        var descriptionAttributes = fieldInfo.GetCustomAttributes(
            typeof(DisplayAttribute), false) as DisplayAttribute[];
        if (descriptionAttributes == null) return string.Empty;
        return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
    }    
    /// <summary>
    /// | item 1 | item 4 | item 6 | item 8 |
    ///| item 2 | item 5 | item 7 | item 9 |
    ///| item 3 |        |        |        |
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="parts"></param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> SplitListVertically<T>(this IEnumerable<T> source, int parts)
    {
        var list = new List<T>(source);
        int defaultSize = (int)((double)list.Count / (double)parts);
        int offset = list.Count % parts;
        int position = 0;

        for (int i = 0; i < parts; i++)
        {
            int size = defaultSize;
            if (i < offset)
                size++; // Just add one to the size (it's enough).

            yield return list.GetRange(position, size);

            // Set the new position after creating a part list, so that it always start with position zero on the first yield return above.
            position += size;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <param name="pobject"></param>
    public static void SerializeXml<T>(string filePath, object pobject)
    {
        StreamWriter objStreamWriter = new StreamWriter(filePath);
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        serializer.Serialize(objStreamWriter, pobject);
        objStreamWriter.Close();
        serializer = null;
        objStreamWriter.Dispose();
        objStreamWriter = null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="xmlStream"></param>
    /// <returns></returns>
    public static T DeserializeXml<T>(Stream xmlStream)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        return (T)(serializer.Deserialize(xmlStream));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static T DeserializeXml<T>(string path, T alt = default(T))
    {
        if (!File.Exists(path)) return alt;

        Stream fs = File.OpenRead(path);
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        try
        {
            return (T)(serializer.Deserialize(fs));
        }
        finally { fs.Close(); fs.Dispose(); }
    }
    
   

}