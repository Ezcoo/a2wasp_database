using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arma2Net.AddInProxy;
using LiteDB;

namespace A2WaspDatabase
{
    [AddIn("StorePlayerScore")] // Name of the function
    public class StorePlayerScore : AddIn
    {
        public override string Invoke(string args, int maxResultSize)
        {
            int guid = args[0];
            int score = args[1];
            using (var db = new LiteDatabase(@"C:\DB\playerScores.db"))
            {
                var players = db.GetCollection<Player>("players");

                if (players.Find(Query.EQ("id", guid)) == null)
                {
                    var player = new Player
                    {
                        id = guid,
                        score = score,
                        ticks = 1
                    };

                    players.Insert(player);

                    return (player.score / player.ticks).ToString(); ;
                }
                else
                {
                    Player existingPlayer = (Player) players.Find(Query.EQ("id", guid));

                    int newScore = existingPlayer.score + score;

                    existingPlayer.score = newScore;
                    existingPlayer.ticks++;

                    players.Update(existingPlayer);

                    int scorePerMin = existingPlayer.score / existingPlayer.ticks;

                    return scorePerMin.ToString();
                }

            }
        }
    }
}
