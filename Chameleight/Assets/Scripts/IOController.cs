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

    public void WriteStatistics1(){
        List<(int,string)> scores = scoreController.GetScores();
        List<(int,string)> colors = scoreController.GetColors();
        List<(int,float)> speeds = throwStatisticController.GetSpeeds();
        List<(int,Vector3)> angles = throwStatisticController.GetAngles();
        List<(int,float)> reactions = timeController.GetReaction();
        List<(int,float)> decisions = timeController.GetDecision();
        List<(int,float)> throws = timeController.GetThrow();
        //Dummy IDs
        string id = StaticClass.playerId;
        int game_id = 0;
        string[] path1 = {"Database", id, "game1.csv"};
        try{
            string lastLine = System.IO.File.ReadLines(Path.Combine(path1)).Last();
            string lastgame_id = lastLine.Split(',')[1];
            game_id = Convert.ToInt32(lastgame_id);
            game_id += 1;
        }
        catch{
            // if conversion or load fails, there is no played game, we mantain game_id=0
        }
        int balls = scores.Count;
        float time = timeController.GetTotal();
        string date = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"); 
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

        NumberFormatInfo nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";
        using (StreamWriter sw = File.AppendText(Path.Combine(path1)))
        {
            for(int i = 0; i < scores.Count; i++){
                sw.WriteLine(id+","+game_id.ToString()+","+date+","+i.ToString()+","+time.ToString(nfi)+","+balls.ToString()+","+colors.ElementAt(i).Item2.ToString()+","
                +scores.ElementAt(i).Item2.ToString()+","+speeds.ElementAt(i).Item2.ToString(nfi)+","+angles.ElementAt(i).Item2.x.ToString(nfi)+","
                +angles.ElementAt(i).Item2.y.ToString(nfi)+","+angles.ElementAt(i).Item2.z.ToString(nfi)+","+reactions.ElementAt(i).Item2.ToString(nfi)+","
                +decisions.ElementAt(i).Item2.ToString(nfi)+","+throws.ElementAt(i).Item2.ToString(nfi));
            }
        }
    }

    public void WriteStatistics2(
        bool movement, float totalTime, List<string> _nLight, List<float> _nTimeToHit, List<float> _nCoordX, List<float> _nCoordY, List<float> _nPoints, List<int> _nBulletsToHit
        ){
        string id = StaticClass.playerId;
        int game_id = 0;
        string[] path2 = {"Database", id, "game2.csv"};
        try{
            string lastLine = System.IO.File.ReadLines(Path.Combine(path2)).Last();
            string lastgame_id = lastLine.Split(',')[1];
            game_id = Convert.ToInt32(lastgame_id);
        }
        catch{
            // if conversion or load fails, there is no played game, we mantain game_id=0
        }
        string date = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        NumberFormatInfo nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";
        using (StreamWriter writer = File.AppendText(Path.Combine(path2)))
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

    public void RegisterUser(){
        string[] path = {"Database", PlayerInfo.email};
        Directory.CreateDirectory(Path.Combine(path));
        using (StreamWriter sw = File.AppendText(Path.Combine(path)))
        {
            sw.WriteLine(PlayerInfo.email+","+PlayerInfo.player_name+","+PlayerInfo.birthday.ToString()+","+PlayerInfo.gender+","+PlayerInfo.laterality+","+PlayerInfo.sport+
            ","+PlayerInfo.level+","+PlayerInfo.competing_years.ToString()+","+PlayerInfo.height.ToString()+","+PlayerInfo.weight.ToString());
        }
    }

    public void createDemoFolder(){
        string id = PlayerInfo.email;
        string[] path = {"Database", id};
        if(!Directory.Exists(Path.Combine(path))){
            Directory.CreateDirectory(Path.Combine(path));
            CreateGameSaveData();
        }
    }
    public void CreateGameSaveData(){
        string id = PlayerInfo.email;
        string[] path1 = {"Database", id, "game1.csv"};
        string[] path2 = {"Database", id, "game2.csv"};
        using (StreamWriter writer = File.AppendText(Path.Combine(path2)))
        {
            writer.Write("id,gameId,date,movement,playTime,instance(hitCorrect),LightEnabled,TimeNeededToHit,HitCoordX,HitCoordY,Points,BulletsNeeded");
            writer.Write(System.Environment.NewLine);
        }
        using (StreamWriter sw = File.AppendText(Path.Combine(path1)))
        {
            sw.Write("id,gameId,date,instance(ball),time,nºballs,ballColor,score,speed,angleX,angleY,angleZ,reactionTime,decisionTime,throwTime");
            sw.Write(System.Environment.NewLine);
        }
    }
}