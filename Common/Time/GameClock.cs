using System;
using UnityEngine;

public class GameClock : BaseBehaviour
{
    private static GameClock _instance;
    private float elapsedSeconds;
    private float secondsInOneDay = 24 * 60 * 60;

    public int SecondsInOneDayInGame = 20 * 60;
    public int StartingHour = 6;
    private int ElapsedMilliseconds { get => (int)(elapsedSeconds * 1000); }

    public static GameClock Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameClock").GetComponent<GameClock>();
            }
            return _instance;
        }
    }

    public int ElapsedMsInGame
    {
        get => (int)(ElapsedMilliseconds * secondsInOneDay / SecondsInOneDayInGame);
    }

    public TimeSpan GameTimeElapsed { get => new TimeSpan(0, 0, 0, 0, ElapsedMsInGame); }
    public TimeSpan RealTimeElapsed { get => new TimeSpan(0, 0, 0, 0, ElapsedMilliseconds); }

    public float TimeAsPercentage
    {
        // 1 : total seconds in game day = x : elapsed seconds in game

        // TODO: THIS MIGHT BREAK ON LONG GAMES
        get
        {
            var percentage = ((decimal)elapsedSeconds / SecondsInOneDayInGame) % 1;
            return (float)Math.Round(percentage, 2);
        }
    }

    private void Start()
    {
        var startingSeconds = StartingHour * 60 * 60;

        elapsedSeconds = SecondsInOneDayInGame * StartingHour / 24;
    }

    private void Update()
    {
        elapsedSeconds += Time.deltaTime;

        elapsedSeconds %= SecondsInOneDayInGame;
    }
}