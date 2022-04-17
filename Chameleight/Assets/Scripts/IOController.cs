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

    public void Write(){
        List<(int,string)> scores = scoreController.GetScores();
        List<(int,float)> speeds = throwStatisticController.GetSpeeds();
        List<(int,Vector3)> angles = throwStatisticController.GetAngles();
        List<(int,float)> reactions = timeController.GetReaction();
        List<(int,float)> decisions = timeController.GetDecision();
        List<(int,float)> throws = timeController.GetThrow();
        scores.Sort(delegate ((int, string) emp1, (int, string) emp2) 
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
        using (StreamWriter sw = File.AppendText("../Database/temp_"+DateTime.UtcNow.ToString("MM-dd-yyyy")+".csv"))
        {
            for(int i = 0; i < scores.Count; i++){
                sw.WriteLine(i.ToString()+","+scores.ElementAt(i).Item2.ToString()+","+speeds.ElementAt(i).Item2.ToString(nfi)
                +","+angles.ElementAt(i).Item2.x.ToString(nfi)+","+angles.ElementAt(i).Item2.y.ToString(nfi)+","+angles.ElementAt(i).Item2.z.ToString(nfi)
                +","+reactions.ElementAt(i).Item2.ToString(nfi)+","+decisions.ElementAt(i).Item2.ToString(nfi)+","+throws.ElementAt(i).Item2.ToString(nfi));
            }
        }
    }
}
