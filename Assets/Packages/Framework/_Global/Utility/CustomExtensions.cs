using System;
using System.Collections.Generic;
using System.Globalization;

/// <summary>
/// A collection of helpful extension methods
/// </summary>
public static class CustomExtensions
{
    #region List<>

    /// <summary>
    /// Removes the last value in the list
    /// </summary>
    public static T RemoveLast<T>(this List<T> list)
    {
        if (list.Count == 0)
            throw new Exception("There are no elements in the list!\n");

        int last = list.Count - 1;

        T value = list[last];
        list.RemoveAt(last);

        return value;
    }

    /// <summary>
    /// Removes the first value in the list
    /// </summary>
    public static T RemoveFirst<T>(this List<T> list)
    {
        if (list.Count == 0)
            throw new Exception("There are no elements in the list!\n");

        T value = list[0];
        list.RemoveAt(0);

        return value;
    }

    /// <summary>
    /// Returns a list of elements that fit the search term
    /// </summary>
    /// <param name="search">Search term</param>
    /// <param name="exact">Only return exact fits</param>
    public static List<string> Search(this List<string> list, string search, bool exact)
    {
        List<string> newList = new List<string>();

        if (exact)
        {
            foreach (string val in list)
                if (string.Compare(val, search, true) == 0)
                    newList.Add(val);
        }
        else
        {
            foreach (string val in list)
            {
                if (val.ToLower(CultureInfo.CurrentCulture).Contains(
                    search.ToLower(CultureInfo.CurrentCulture)))
                {
                    newList.Add(val);
                }
            }
        }

        return newList;
    }

    #endregion

    #region LinkedList<>

    /// <summary>
    /// Removes the first value and appends it
    /// </summary>
    public static T RemoveFirstAndAppend<T>(this LinkedList<T> list)
    {
        if (list.Count == 0)
            throw new Exception("There are no elements in the list!\n");

        T value = list.First.Value;

        list.RemoveFirst();
        list.AddLast(value);

        return value;
    }

    #endregion
}