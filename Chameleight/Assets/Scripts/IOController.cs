using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;
using System.Globalization;
using System;

public class IOController : MonoBehaviour
{
    public TimeController timeController;
    public ScoreController scoreController;
    public ThrowStatisticController throwStatisticController;

    //Write the statistics of the first game in a csv file.
    public void WriteStatistics1(){
        //Get statistics.
        List<(int,string)> scores = scoreController.GetScores();
        List<(int,string)> colors = scoreController.GetColors();
        List<(int,float)> speeds = throwStatisticController.GetSpeeds();
        List<(int,Vector3)> angles = throwStatisticController.GetAngles();
        List<(int,float)> reactions = timeController.GetReaction();
        List<(int,float)> decisions = timeController.GetDecision();
        List<(int,float)> throws = timeController.GetThrow();
        //Player identifier.
        string id = StaticClass.playerId;
        //Ball speed multiplier.
        float factor = StaticClass.BallFactor;
        int game_id = 0;
        string[] path1 = {"Database", id, "game1.csv"};
        string path = Path.Combine(Application.persistentDataPath, Path.Combine(path1));
        try{
            string lastLine = System.IO.File.ReadLines(path).Last();
            string lastgame_id = lastLine.Split(',')[1];
            //Game identifier
            game_id = Convert.ToInt32(lastgame_id);
            game_id += 1;
        }
        catch{
            //If conversion or load fails, there is no played game, we mantain game_id=0.
        }
        //Number of balls.
        int balls = scores.Count;
        //Total playing time.
        float time = timeController.GetTotal();
        //Current date.
        string date = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        //Sort all the statistics according to the ball number.
        scores.Sort(delegate ((int, string) emp1, (int, string) emp2) 
        {
            return emp1.Item1.CompareTo(emp2.Item1);
        });
        colors.Sort(delegate ((int, string) emp1, (int, string) emp2) 
        {
            return emp1.Item1.CompareTo(emp2.Item1);
        });
        speeds.Sort(delegate ((int, float) emp1, (int, float) emp2) 
        {
            return emp1.Item1.CompareTo(emp2.Item1);
        });
        angles.Sort(delegate ((int, Vector3) emp1, (int, Vector3) emp2) 
        {
            return emp1.Item1.CompareTo(emp2.Item1);
        });
        reactions.Sort(delegate ((int, float) emp1, (int, float) emp2) 
        {
            return emp1.Item1.CompareTo(emp2.Item1);
        });
        decisions.Sort(delegate ((int, float) emp1, (int, float) emp2) 
        {
            return emp1.Item1.CompareTo(emp2.Item1);
        });
        throws.Sort(delegate ((int, float) emp1, (int, float) emp2) 
        {
            return emp1.Item1.CompareTo(emp2.Item1);
        });

        //Store the statistics in a csv file.
        NumberFormatInfo nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";
        using (StreamWriter sw = File.AppendText(Path.Combine(path)))
        {
            for(int i = 0; i < scores.Count; i++){
                sw.WriteLine(id+","+game_id.ToString()+","+date+","+time.ToString(nfi)+","+balls.ToString()+","+factor.ToString(nfi)+","+i.ToString()+","
                +colors.ElementAt(i).Item2.ToString()+","+scores.ElementAt(i).Item2.ToString()+","+speeds.ElementAt(i).Item2.ToString(nfi)+","
                +angles.ElementAt(i).Item2.x.ToString(nfi)+","+angles.ElementAt(i).Item2.y.ToString(nfi)+","+angles.ElementAt(i).Item2.z.ToString(nfi)+","
                +reactions.ElementAt(i).Item2.ToString(nfi)+","+decisions.ElementAt(i).Item2.ToString(nfi)+","+throws.ElementAt(i).Item2.ToString(nfi));
            }
        }
    }

    // Write shooting game statistics to user's csv
    public void WriteStatistics2(
        bool movement, float totalTime, List<string> _nLight, List<float> _nTimeToHit, List<float> _nCoordX, List<float> _nCoordY, List<float> _nPoints, List<int> _nBulletsToHit
        ){
        string id = StaticClass.playerId; // user ID
        int game_id = 0; // game ID (differentiate each play session)
        string[] path2 = {"Database", id, "game2.csv"};
        string path = Path.Combine(Application.persistentDataPath, Path.Combine(path2));
        try{
            // Update game_id + 1 from last session if player has already played
            string lastLine = System.IO.File.ReadLines(path).Last();
            string lastgame_id = lastLine.Split(',')[1];
            game_id = Convert.ToInt32(lastgame_id);
            game_id += 1;
        }
        catch{
            // if conversion or load fails, there is no played game, we mantain game_id=0
        }
        string date = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        NumberFormatInfo nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";
        // Write parameters following csv column order (see CreateGameSaveData)
        using (StreamWriter writer = File.AppendText(Path.Combine(path)))
        {
            for(int i = 0; i < _nLight.Count; i++){
              writer.Write(id);  
              writer.Write(",");
              writer.Write(game_id.ToString(nfi));  
              writer.Write(",");
              writer.Write(date);  
              writer.Write(",");
              writer.Write(movement);  
              writer.Write(",");
              writer.Write(totalTime);  
              writer.Write(",");
              writer.Write(i.ToString(nfi));  
              writer.Write(",");
              writer.Write(_nLight[i].ToString(nfi));
              writer.Write(",");
              writer.Write(_nTimeToHit[i].ToString(nfi));
              writer.Write(",");
              writer.Write(_nCoordX[i].ToString(nfi));
              writer.Write(",");
              writer.Write(_nCoordY[i].ToString(nfi));
              writer.Write(",");
              writer.Write(_nPoints[i].ToString(nfi));
              writer.Write(",");
              writer.Write(_nBulletsToHit[i].ToString(nfi));
              writer.Write(System.Environment.NewLine);
            }
        }
    }

    //Store the information of a new user in a csv file.
    public void RegisterUser(){
        string[] path = {"Database", PlayerInfo.email};
        string[] infopath = {"Database", PlayerInfo.email, "user_info.csv"};
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, Path.Combine(path)));
        using (StreamWriter sw = File.AppendText(Path.Combine(Application.persistentDataPath, Path.Combine(infopath))))
        {
            sw.WriteLine("email,name,birthday,gender,laterality,sport,level,competing_years,practice_hours,height,weight");
            sw.WriteLine(PlayerInfo.email+","+PlayerInfo.player_name+","+PlayerInfo.birthday.ToString()+","+PlayerInfo.gender+","+PlayerInfo.laterality+","+PlayerInfo.sport+
            ","+PlayerInfo.level+","+PlayerInfo.competing_years.ToString()+","+PlayerInfo.practice_hours.ToString()+","+PlayerInfo.height.ToString()+","+PlayerInfo.weight.ToString());
        }
    }

    // Create demo user folder (the first time the game is initialized)
    public void createDemoFolder(){
        string id = PlayerInfo.email;
        string[] path = {"Database", id};
        if(!Directory.Exists(Path.Combine(Application.persistentDataPath, Path.Combine(path)))){
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, Path.Combine(path)));
            CreateGameSaveData();
        }
    }

    // Creates csv game files and column names for recently registered user
    public void CreateGameSaveData(){
        string id = PlayerInfo.email;
        string[] path1 = {"Database", id, "game1.csv"};
        string[] path2 = {"Database", id, "game2.csv"};
        string path;
        path = Path.Combine(Application.persistentDataPath, Path.Combine(path2));
        using (StreamWriter writer = File.AppendText(path))
        {
            writer.Write("id,gameId,date,movement,playTime,instance(hitCorrect),LightEnabled,TimeNeededToHit,HitCoordX,HitCoordY,Points,BulletsNeeded");
            writer.Write(System.Environment.NewLine);
        }
        path = Path.Combine(Application.persistentDataPath, Path.Combine(path1));
        using (StreamWriter sw = File.AppendText(path))
        {
            sw.Write("id,gameId,date,time,nºballs,resFactor,instance(ball),ballColor,score,speed,angleX,angleY,angleZ,reactionTime,decisionTime,throwTime");
            sw.Write(System.Environment.NewLine);
        }
    }
}