#This script contains the classes and methods that are needed to perform an analysis of the data generated in the Chameleight game.

# Import libraries
import os
import seaborn as sns
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.patches as mpatches
import matplotlib.ticker as mticker
import pandas as pd
import plotly.express as px
import math

# Class that stores the information of a given player
class Player:
    def __init__(self, playerinfo):
        # name, birthday, gender, laterality, sport, level, compYears, practiceh, height, weight
        self.ID = playerinfo['email'].iloc[0]
        self.name = playerinfo['name'].iloc[0]
        self.birthday = playerinfo['birthday'].iloc[0]
        self.gender = playerinfo['gender'].iloc[0]
        self.laterality = playerinfo['laterality'].iloc[0]
        self.sport = playerinfo['sport'].iloc[0]
        self.level = playerinfo['level'].iloc[0]
        self.competing_years = playerinfo['competing_years'].iloc[0]
        self.practice_hours = playerinfo['practice_hours'].iloc[0]
        self.height = playerinfo['height'].iloc[0]
        self.weight = playerinfo['weight'].iloc[0]

# Class that stores the data generated in the Color Balls game
class Game1:
    def __init__(self, player,game1):
        # player info, game data
        self.player = player
        self.game = game1

    # Display number of balls for each of the possible scores
    def tableScores(self):
        # Count scores
        count = self.game.groupby("score")["instance"].count()
        # Insert missing scores
        for ind in ["CORRECT","INCORRECT","MISSED","OUT"]:
            if ind not in count.index:
                count = count.append(pd.Series([0],index=[ind]))
        count = count.sort_index()
        print("NUMBER OF BALLS PER SCORE")
        print(count)
        print()

    # Display mean throw speed for each of the possible scores
    def tableSpeeds(self):
        # Compute mean speed
        sp = self.game.groupby("score")["speed"].mean()
        # Insert missing scores
        for ind in ["CORRECT","INCORRECT","MISSED","OUT"]:
            if ind not in sp.index:
                sp = sp.append(pd.Series([None],index=[ind]))
        sp = sp.sort_index()
        print("MEAN SPEED PER SCORE")
        print(sp)
        print()

    # Plot percentage of reaction and decision times according to the throw score
    def plotTimes(self):
        # Get mean reaction and decision times
        times = self.game.groupby("score")["reactionTime","decisionTime"].mean()
        try:
            cor_r, cor_d = times["reactionTime"].loc["CORRECT"], times["decisionTime"].loc["CORRECT"]
        except:
            cor_r, cor_d = (0, 0)
        try:
            inc_r, inc_d = times["reactionTime"].loc["INCORRECT"], times["decisionTime"].loc["INCORRECT"]
        except:
            inc_r, inc_d = (0, 0)
        try:
            mis_r, mis_d = times["reactionTime"].loc["MISSED"], times["decisionTime"].loc["MISSED"]
        except:
            mis_r, mis_d = (0, 0)
        try:
            out_r, out_d = times["reactionTime"].loc["OUT"], times["decisionTime"].loc["OUT"]
        except:
            out_r, out_d = (0, 0)

        totals = [cor_r+cor_d,inc_r+inc_d,mis_r+mis_d,out_r+out_d]
        reacs = [cor_r,inc_r,mis_r,out_r]
        decs = [cor_d,inc_d,mis_d,out_d]
        reactions = []
        decisions = []
        scores = ("CORRECT","INCORRECT","MISSED","OUT")
        names = []
        # Compute percentage of reaction and decision times
        for i in range(len(totals)):
            if totals[i] != 0 and not math.isnan(totals[i]):
                reactions.append(100*reacs[i]/totals[i])
                decisions.append(100*decs[i]/totals[i])
                names.append(scores[i])
        r = [i for i in range(len(names))]

        # Plot the percentages as bar plots
        barWidth = 0.85
        plt.bar(r, reactions, color='#b5ffb9', edgecolor='white', width=barWidth,label="Reaction time")
        plt.bar(r, decisions, bottom=reactions, color='#f9bc86', edgecolor='white', width=barWidth,label="Decision time")

        # Set labels
        plt.xticks(r, names)
        plt.ylabel("Time percentage (%)")
        plt.xlabel("Score")

        # Set legend
        plt.legend(loc='upper left', bbox_to_anchor=(1,1), ncol=1)

        # Show graphic
        plt.show()

    # Plot the vectors that represent the throw directions
    def plotVectors(self):
        fig, (ax1, ax2) = plt.subplots(1, 2, figsize=(12,5))
        # Get the throw directions (x,y,z)
        V = np.array([[float(self.game["angleX"].loc[i]),float(self.game["angleY"].loc[i]),float(self.game["angleZ"].loc[i])] for i in range(len(self.game.index))])
        rem = []
        col = []

        # The vector color depends on the throw score
        for i in range(len(V)):
            if V[i,0] == 0.0 and V[i,1] == 0.0 and V[i,2] == 0.0:
                rem.append(i)
            else:
                if self.game["score"].iloc[i] == "CORRECT":
                    col.append("green")
                elif self.game["score"].iloc[i] == "INCORRECT":
                    col.append("red")
                elif self.game["score"].iloc[i] == "MISSED":
                    col.append("orange")
                else:
                    col.append("purple")

        V = np.delete(V,rem,axis=0)
        cor = mpatches.Patch(color='green', label='Correct')
        inc = mpatches.Patch(color='red', label='Incorrect')
        mis = mpatches.Patch(color='orange', label='Missed')
        out = mpatches.Patch(color='purple', label='Out')
        origin = np.array([[0]*len(V),[0]*len(V)])

        # Plot the vectors from the top view
        ax1.set_xlim((-1,1))
        ax1.set_ylim((-1,1))
        ax1.quiver(*origin, V[:,0], V[:,2], color = col, scale = 2.5)
        # Set labels, legend and title
        ax1.set_xlabel("X axis")
        ax1.set_ylabel("Z axis")
        ax1.set_title("Top view")
        ax1.legend(handles=[cor,inc,mis,out])

        # Plot the vectors from the lateral view
        ax2.set_xlim((-1,1))
        ax2.set_ylim((-1,1))
        ax2.quiver(*origin, V[:,2], V[:,1], color = col, scale = 2.5)
        # Set labels, legend and title
        ax2.set_xlabel("Z axis")
        ax2.set_ylabel("Y axis")
        ax2.set_title("Lateral view")
        plt.legend(handles=[cor,inc,mis,out])

        #Show graphic
        plt.show()

    # Plot comparation between users according to two variables: accuracy and mean time per ball
    def plotComparation(self):
        # Get other player info
        others = pd.DataFrame({"id":[],"gameId":[],"date":[],"time":[],"balls":[],"resFactor":[],"instance":[],"ballColor":[],"score":[],"speed":[],"angleX":[],"angleY":[],"angleZ":[],"reactionTime":[],"decisionTime":[],"throwTime":[]})
        othersPlayer = pd.DataFrame({"email":[],"name":[],"birthday":[],"gender":[],"laterality":[],"sport":[],"level":[],"competing_years":[],"practice_hours":[],"height":[],"weight":[]})
        for _, dirs, _ in os.walk("./Database/"):
            for dir in dirs:
                if dir != self.player.ID:
                    aux1 = pd.read_csv('./Database/'+dir+'/game1.csv', header=None,  skiprows=[0])
                    aux1.columns = ["id","gameId","date","time","balls","resFactor","instance","ballColor","score","speed","angleX","angleY","angleZ","reactionTime","decisionTime","throwTime"]
                    others = others.append(aux1,ignore_index=True)
                    aux2 = pd.read_csv('./Database/'+dir+'/user_info.csv', header=None,  skiprows=[0])
                    aux2.columns = ["email","name","birthday","gender","laterality","sport","level","competing_years","practice_hours","height","weight"]
                    othersPlayer = othersPlayer.append(aux2,ignore_index=True)
        # Compute accuracy and time for the current user
        t = (self.game["reactionTime"] + self.game["decisionTime"]).mean()
        v = 100*len(self.game[self.game["score"]=="CORRECT"])/len(self.game)
        # Compute color of the current user (athlete or non-athlete)
        c = "red"
        if self.player.sport != "Non-Athlete":
            c = "blue"
        # Compute accuracy and time for the rest of the users
        others["totalTime"] = others["reactionTime"] + others["decisionTime"]
        ts = list(others.groupby(['id'])["totalTime"].mean().sort_index(ascending=True))
        vs = [100*len(others[(others["id"] == id) & (others["score"] == "CORRECT")])/len(others[others["id"] == id]) for id in sorted(others["id"].unique())]
        # Compute color of the rest of the users (athlete or non-athlete)
        cs = ["blue" if othersPlayer[othersPlayer["email"] == id]["sport"].iloc[0]!="Non-Athlete" else "red" for id in sorted(others["id"].unique())]
        # Plot the rest of the users
        plt.scatter(ts,vs,c=cs)
        # Plot the current user. Mark it with a star
        plt.scatter(t,v,marker="*", c=c, s = 200)
        # Find pareto front among all the users
        pT, pV = findPareto(ts+[t],vs+[v],maxX=False,maxY=True)
        # Plot pareto front
        plt.plot(pT,pV)
        plt.fill_between(pT,pV,alpha=0.2)
        # Set labels and legend
        nat = mpatches.Patch(color='red', label='Non-Athlete')
        at = mpatches.Patch(color='blue', label='Athlete')
        plt.legend(handles=[nat,at])
        plt.xlabel("Time per ball (s)")
        plt.ylabel("Correct percentage (%)")

        # Show graphic
        plt.show()

# Class that stores the data generated in the Shooting Lights game
class Game2:
    def __init__(self,player,game2):
        self.player = player
        self.game = game2

    # Print mean points per target
    def meanPoints(self):
        print("MEAN POINTS PER TARGET: " + str(self.game["points"].mean()))
        print()

    # Plot distribution of number of bullets per target
    def plotBullets(self):
        # Plot the distribution as an histogram
        sns.histplot(self.game["bullets"],discrete=True,stat='probability')
        # Set labels and axis limits
        plt.xlabel("Bullets per target")
        plt.xlim((0.5,max(self.game["bullets"])+0.5))
        plt.gca().xaxis.set_major_locator(mticker.MultipleLocator(1))

        # Show graphic
        plt.show()

    # Plot coordinates of the darts that hit the target
    def plotPoints(self):
        fig, ax = plt.subplots()
        # Plot the circle that represents the target
        cir = plt.Circle((0, 0), 1.64775,fill=False)
        ax.set_aspect('equal', adjustable='datalim')
        ax.add_patch(cir)
        # Plot coordinates of the darts. The color represents the obtained amount of points
        cax = ax.scatter(self.game["hitCoordX"],self.game["hitCoordY"], c=self.game["points"], cmap='viridis_r')
        idmax = self.game["points"].idxmax()
        # Mark the best shoot with a star
        ax.scatter(self.game["hitCoordX"].iloc[idmax],self.game["hitCoordY"].iloc[idmax], c=self.game["points"].iloc[idmax], vmin=self.game["points"].min(), vmax=self.game["points"].max(), cmap='viridis_r', marker="*", s=200)
        # Set labels and cbar
        ax.set_xlabel("X axis")
        ax.set_ylabel("Y axis")
        cbar = plt.colorbar(cax)
        cbar.set_label('Points')

        # Show graphics
        plt.show()

    # Plot comparation between users according to two variables: score and time per target
    def plotComparation(self):
        # Get other player info
        others = pd.DataFrame({"id":[],"gameId":[],"date":[],"movement":[],"time":[],"instance":[],"lightEnabled":[],"hitTime":[],"hitCoordX":[],"hitCoordY":[],"points":[],"bullets":[]})
        othersPlayer = pd.DataFrame({"email":[],"name":[],"birthday":[],"gender":[],"laterality":[],"sport":[],"level":[],"competing_years":[],"practice_hours":[],"height":[],"weight":[]})
        for _, dirs, _ in os.walk("./Database/"):
            for dir in dirs:
                if dir != self.player.ID:
                    aux1 = pd.read_csv('./Database/'+dir+'/game2.csv', header=None,  skiprows=[0])
                    aux1.columns = ["id","gameId","date","movement","time","instance","lightEnabled","hitTime","hitCoordX","hitCoordY","points","bullets"]
                    others = others.append(aux1,ignore_index=True)
                    aux2 = pd.read_csv('./Database/'+dir+'/user_info.csv', header=None,  skiprows=[0])
                    aux2.columns = ["email","name","birthday","gender","laterality","sport","level","competing_years","practice_hours","height","weight"]
                    othersPlayer = othersPlayer.append(aux2,ignore_index=True)
        # Compute score and time for the current user
        t = self.game["hitTime"].mean()
        v = self.game["points"].mean()
        # Compute color of the current user (athlete or non-athlete)
        c = "red"
        if self.player.sport != "Non-Athlete":
            c = "blue"
        # Compute score and time for the rest of the users
        ts = list(others.groupby(['id'])["hitTime"].mean().sort_index(ascending=True))
        vs = list(others.groupby(['id'])["points"].mean().sort_index(ascending=True))
        # Compute color of the rest of the users (athlete or non-athlete)
        cs = ["blue" if othersPlayer[othersPlayer["email"] == id]["sport"].iloc[0]!="Non-Athlete" else "red" for id in sorted(others["id"].unique())]
        # Plot the rest of the users
        plt.scatter(ts,vs,c=cs)
        # Plot the current user. Mark it with a star
        plt.scatter(t,v,marker="*", c=c, s = 200)
        # Find pareto front among all the users
        pT, pV = findPareto(ts+[t],vs+[v],maxX=False,maxY=True)
        # Plot pareto front
        plt.plot(pT,pV)
        plt.fill_between(pT,pV,alpha=0.2)
        # Set labels and legend
        nat = mpatches.Patch(color='red', label='Non-Athlete')
        at = mpatches.Patch(color='blue', label='Athlete')
        plt.legend(handles=[nat,at])
        plt.xlabel("Time per target (s)")
        plt.ylabel("Points per target")

        # Show graphic
        plt.show()

# Function that finds the pareto front according to the given data points (Xs,Ys) and optimization directions (maxX, maxY)
def findPareto(Xs, Ys, maxX = True, maxY = True):
    # Sort the list in either ascending or descending order of X
        myList = sorted([[Xs[i], Ys[i]] for i in range(len(Xs))], reverse=maxX)
    # Start the Pareto frontier with the first value in the sorted list
        p_front = [myList[0]]
    # Loop through the sorted list
        for pair in myList[1:]:
            if maxY:
                if pair[1] >= p_front[-1][1]: # Look for higher values of Y…
                    p_front.append(pair) # … and add them to the Pareto frontier
            else:
                if pair[1] <= p_front[-1][1]: # Look for lower values of Y…
                    p_front.append(pair) # … and add them to the Pareto frontier
    # Turn resulting pairs back into a list of Xs and Ys
        p_frontX = [pair[0] for pair in p_front]
        p_frontY = [pair[1] for pair in p_front]
        if maxX == False:
            p_frontX = [0]+p_frontX+[max(Xs)]
        else:
            p_frontX = [max(Xs)]+p_frontX+[0]
        if maxY == False:
            p_frontY = [max(Ys)]+p_frontY+[0]
        else:
            p_frontY = [0]+p_frontY+[max(Ys)]
        return p_frontX, p_frontY

# Function that reads the info of the user with the input email (if it exists). Returns player info, game 1 data and game 2 data
def read_userInf(user):
    try:
        playerinfo = pd.read_csv('./Database/'+user+'/user_info.csv', header=None,  skiprows=[0])
        playerinfo.columns = ["email","name","birthday","gender","laterality","sport","level","competing_years","practice_hours","height","weight"]
        player = Player(playerinfo)
        game1info = pd.read_csv('./Database/'+user+'/game1.csv', header=None,  skiprows=[0])
        game1info.columns = ["id","gameId","date","time","balls","resFactor","instance","ballColor","score","speed","angleX","angleY","angleZ","reactionTime","decisionTime","throwTime"]
        game1 = Game1(player, game1info)
        game2info = pd.read_csv('./Database/'+user+'/game2.csv', header=None,  skiprows=[0])
        game2info.columns = ["id","gameId","date","movement","time","instance","lightEnabled","hitTime","hitCoordX","hitCoordY","points","bullets"]
        game2 = Game2(player, game2info)
        return(player,game1,game2)
    except:
        return None, None, None
