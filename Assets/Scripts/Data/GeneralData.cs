using System;
using UnityEngine;

public class GeneralData : MonoBehaviour
{
    private static int _coins;
    private static int _level;
    private static int _experience;
    private static int _experienceLeft;
    private static bool _sounds;
    private static bool _music;

    public static bool Sounds
    {
        get
        {
            _sounds = true;
            
            if (PlayerPrefs.HasKey("Sounds"))
            {
                _sounds = Convert.ToBoolean(PlayerPrefs.GetInt("Sounds"));
            }

            return _sounds;
        } 
        set
        {
            _sounds = value;
            PlayerPrefs.SetInt("Sounds", Convert.ToInt32(_sounds));
        } 
    }

    public static bool Music
    {
        get
        {
            _music = true;
            
            if (PlayerPrefs.HasKey("Music"))
            {
                _music = Convert.ToBoolean(PlayerPrefs.GetInt("Music"));
            }

            return _music;
        } 
        set
        {
            _music = value;
            PlayerPrefs.SetInt("Music", Convert.ToInt32(_music));
        } 
    }

    public static int Coins
    {
        get
        {
            _coins = PlayerPrefs.GetInt("Coins", 0);
            return _coins;
        }
        set
        {
            _coins = value;
            PlayerPrefs.SetInt("Coins", _coins);
        }
    }

    public static int Level
    {
        get
        {
            _level = PlayerPrefs.GetInt("Level", 1);
            return _level;
        }
        set
        {
            _level = value;
            PlayerPrefs.SetInt("Level", _level);
        }
    }

    public static int Experience
    {
        get
        {
            _experience = PlayerPrefs.GetInt("Exp", 0);
            return _experience;
        }
        set
        {
            _experience = value;
            PlayerPrefs.SetInt("Exp", _experience);
        }
    }
    
    public static int ExperienceLeft
    {
        get
        {
            _experienceLeft = PlayerPrefs.GetInt("ExpLeft", 50);
            return _experienceLeft;
        }
        set
        {
            _experienceLeft = value;
            PlayerPrefs.SetInt("ExpLeft", _experienceLeft);
        }
    }
}
