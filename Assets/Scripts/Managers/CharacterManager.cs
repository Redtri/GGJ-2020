using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public static CharacterManager instance;

    Dictionary<string, int> characters;

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        characters = new Dictionary<string, int> ();
    }

    public void AddChara(string c_NameSurc_Name)
    {
        if (!characters.ContainsKey(c_NameSurc_Name))
        {
            characters.Add(c_NameSurc_Name, 1);
        }
        else
        {
            characters[c_NameSurc_Name]++;
        }
    }
    

}
