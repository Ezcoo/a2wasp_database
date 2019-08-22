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
                        score = score
                    };

                    players.Insert(player);

                    return player.score.ToString();
                }
                else
                {
                    Player existingPlayer = (Player) players.Find(Query.EQ("id", guid));

                    existingPlayer.score = score;

                    players.Update(existingPlayer);

                    return "-1";
                }

            }
        }
    }
}
