using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{    
    public static string GetData(List<Dictionary<string, object>> data, int id, string datatype)
    {
        string strdata = "";
        int datanum = 0;

        for (int i = 0; i < data.Count; i++)
        {
            if (string.Compare(data[i]["id"].ToString(), id.ToString(), true) == 0)
            {
                datanum = i;

                break;
            }         
        }

        strdata = data[datanum][datatype].ToString();

        if (string.Compare(datatype, "image", true) == 0)
        {
            char p = '.';

            string[] str = strdata.Split(p); //.jpg Â©¶óÁÖ±â

            strdata = str[0];
        }

        return strdata;
    }


}
