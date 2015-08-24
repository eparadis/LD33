using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HardcodedLevelStore {

    public string GetFirstLevel()
    {
        return RecallLevel(0);
    }

    public string GetNextLevel()
    {
        int lastLevelNum = PlayerPrefs.GetInt("lastLoadedLevel", -1);

        if( lastLevelNum == -1)
            return "";

        if( lastLevelNum == _levels.Count - 1)
            return "";

        lastLevelNum += 1;

        return RecallLevel(lastLevelNum);
    }

    string RecallLevel(int levelNumber)
    {
        PlayerPrefs.SetInt("lastLoadedLevel", levelNumber);
        return _levels[levelNumber]; 
    }

    List<string> _levels = new List<string>() 
    {
        @"L 3 0
L 0 -5
L 3 -4.5
S -3 0
L 6 -2
L -2.8 -2
P",

@"L 3 0
L 0 -5
L 3 -4.5
S -3 0
K -1 -1
L 6 -2
L -2.8 -2
P",

@"L 3 0
L 0 -5
L 3 -4.5
S -3 0
K -1 -1
L 6 -2
L -2.8 -2
K -4 0
P"

        
    };
}
