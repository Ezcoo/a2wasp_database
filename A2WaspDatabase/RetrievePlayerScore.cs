using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arma2Net.AddInProxy;
using LiteDB;

namespace A2WaspDatabase
{
    [AddIn("RetrievePlayerScore")] // Name of the function
    public class RetrievePlayerScore : AddIn
    {
        public override string Invoke(string args, int maxResultSize)
        {
            int guid = args[0];
            using (var db = new LiteDatabase(@"C:\DB\playerScores.db"))
            {
                var players = db.GetCollection<Player>("players");

                if (players.Find(Query.EQ("id", guid)) == null)
                {
                    var player = new Player
                    {
                        id = guid,
                        score = 0,
                        ticks = 1
                    };

                    players.Insert(player);

                    return (player.score / player.ticks).ToString();
                }
                else
                {
                    Player existingPlayer = (Player) players.FindById(guid);

                    return (existingPlayer.score / existingPlayer.ticks).ToString();
                }

            }
        }
    }

}