# This is the script for processig the data obtained from the Chameleight RVA Game

# Press Mayús+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.

# import libraries
import seaborn as sns
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.patches as mpatches
import matplotlib.pyplot as plt
import pandas as pd
import plotly.express as px

class Player:
    def __init__(player, playerinfo, game1, game2):
        # name, birthday, gender, laterality, sport, level, compYears, practiceh, height, weight
        player.name = playerinfo['name']
        player.birthday = playerinfo['birthday']
        player.gender = playerinfo['gender']
        player.laterality = playerinfo['laterality']
        player.sport = playerinfo['sport']
        player.level = playerinfo['level']
        player.competing_years = playerinfo['competing_years']
        player.practice_hours = playerinfo['practice_hours']
        player.height = playerinfo['height']
        player.weight = playerinfo['weight']
        player.game1Info = game1
        player.game2Info = game2

class Game1:
    def __init__(gameData, game1):
        gameData.info = game1

    def percentStackedBarChart(self):
        # set the figure size
        plt.figure(figsize=(14, 14))

                # from raw value to percentage
        total = gameData.info.groupby('score')['nºballs'].sum().reset_index()
        blue = tips[tips.smoker == 'Blue Material (Instance)'].groupby('score')['nºballs'].sum().reset_index()
        blue['nºballs'] = [i / j * 100 for i, j in zip(blue['nºballs'], total['nºballs'])]
        total['nºballs'] = [i / j * 100 for i, j in zip(total['nºballs'], total['nºballs'])]

        # bar chart 1 -> top bars (group of 'Balls=Red')
        bar1 = sns.barplot(x="day", y="total_bill", data=total, color='sandybrown')


        # bar chart 2 -> bottom bars (group of 'Balls=blue')
        bar2 = sns.barplot(x="day", y="total_bill", data=blue, color='lightblue')

        # add legend
        top_bar = mpatches.Patch(color='sandybrown', label='Balls = Red')
        bottom_bar = mpatches.Patch(color='lightblue', label='Balls = Blue')
        plt.legend(handles=[top_bar, bottom_bar])

        # show the graph
        plt.show()

        # falta una barra por cada juego con el porcentaje de missed correct out
        # eje x juego, eje y nºballs

        # From raw value to percentage
        r = gameData.info['gameId'].unique()
        totals = [i + j + k for i, j, k in zip(gameData.info.loc[gameData.info['score'].isin('CORRECT')], gameData.info.loc[gameData.info['score'].isin('MISSED')], gameData.info.loc[gameData.info['score'].isin('OUT')])]
        correctBars = [i / j * 100 for i, j in zip(gameData.info.loc[gameData.info['score'].isin('CORRECT')], totals)]
        missingBars = [i / j * 100 for i, j in zip(gameData.info.loc[gameData.info['score'].isin('MISSING')], totals)]
        outBars = [i / j * 100 for i, j in zip(gameData.info.loc[gameData.info['score'].isin('OUT')], totals)]

        # plot
        barWidth = 0.85
        #names = ('Correct', 'B', 'C', 'D', 'E')
        # Create green Bars
        plt.bar(r, correctBars, color='#b5ffb9', edgecolor='white', width=barWidth)
        # Create orange Bars
        plt.bar(r, missingBars, bottom=correctBars, color='#f9bc86', edgecolor='white', width=barWidth)
        # Create blue Bars
        plt.bar(r, outBars, bottom=[i + j for i, j in zip(correctBars, missingBars)], color='#a3acff', edgecolor='white',
                width=barWidth)

        # Create green Bars
        plt.bar(r, correctBars, color='#b5ffb9', edgecolor='white', width=barWidth, label="group A")
        # Create orange Bars
        plt.bar(r, missingBars, bottom=correctBars, color='#f9bc86', edgecolor='white', width=barWidth, label="group B")
        # Create blue Bars
        plt.bar(r, outBars, bottom=[i + j for i, j in zip(correctBars, missingBars)], color='#a3acff', edgecolor='white',
                width=barWidth, label="group C")

        # Custom x axis
        #plt.xticks(r, names)
        #plt.xlabel("group")

        # Add a legend
        plt.legend(loc='upper left', bbox_to_anchor=(1, 1), ncol=1)

        # Show graphic
        plt.show()

class Game2:
    def __init__(gameData, game2):
        gameData.info = game2

    def shotingPlot(self):
        df = gameData.info
        # give a list to the marker argument
        sns.lmplot(x='HitCoordX', y='HitCoordY', data=df, fit_reg=False, hue='LightEnabled', legend=False,
                   markers=["o", "x", "1"])
        # raw a circle to mesh the points
        # circle = plt.Circle((0, 0), 2)
        # ax.add_patch(circle1)

        # Move the legend to an empty part of the plot
        plt.legend(loc='lower right')


        plt.show()


def read_userInf():
    playerinfo = pd.read_csv('RVA_data/RVA_Data/Database/igonemorais@gmail.com/user_info.csv')
    game1 = pd.read_csv('RVA_data/RVA_Data/Database/igonemorais@gmail.com/game1.csv')
    game2 = pd.read_csv('RVA_data/RVA_Data/Database/igonemorais@gmail.com/game2.csv')

def player_nonplayer_Stats():


def print_hi(name):
    # Use a breakpoint in the code line below to debug your script.
    print(f'Hi, {name}')  # Press Ctrl+F8 to toggle the breakpoint.



